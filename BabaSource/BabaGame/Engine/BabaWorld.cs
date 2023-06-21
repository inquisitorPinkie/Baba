﻿using Core.Content;
using Core.Engine;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaGame.Engine;

public class BabaWorld
{

	public Dictionary<short, BabaMap> MapDatas;
	public Dictionary<short, RegionData> Regions;
	public Dictionary<short, MapSimulator> Simulators;
	public short[] GlobalWordMapIds;
    private string name;
    public readonly Dictionary<ObjectTypeId, int> Inventory;

	public BabaWorld(WorldData data)
	{
        name = data.Name;
		MapDatas = data.Maps.ToDictionary(map => map.MapId, map => (BabaMap)map);
		Regions = data.Regions.ToDictionary(r => r.RegionId);
        GlobalWordMapIds = data.GlobalWordMapIds;
		Simulators = data.Maps.ToDictionary(map => map.MapId, map => new MapSimulator(this, map.MapId));
        Inventory = data.Inventory;

		foreach (var sim in Simulators.Values)
		{
			sim.GetNeighbors();
        }
	}

    public WorldData ToWorldData()
    {
        var data = new WorldData
        {
            Maps = MapDatas.Values.Select(x => x.ToMapData()).ToList(),
            Regions = Regions.Values.ToList(),
            GlobalWordMapIds = GlobalWordMapIds,
            Name = name,
            Inventory = Inventory,
        };
        return data;
    }


    public short[] mapsWithYou(ObjectTypeId you)
    {
        parseMapRules(Simulators.Values.Select(s => s.MapId).ToArray());
        var mapIds = new List<short>();
        foreach (var sim in Simulators.Values)
        {
            var objs = sim.findObjectsThatAre(you).ToList();
            if (objs.Any())
            {
                mapIds.Add(sim.MapId);
            }
        }
        return mapIds.ToArray();
    }

	private void parseMapRules(short[] mapIds)
	{
		var dict = new Dictionary<short, RuleDict>();

        var globalRules = _parseAndApplyToAll(new RuleDict(), GlobalWordMapIds);

        var regionRules = Regions.ToDictionary(s => s.Key, s => _parseAndApplyToAll(globalRules, s.Value.WordLayerIds));

        var alreadyProcessed = GlobalWordMapIds.Concat(Regions.SelectMany(x => x.Value.WordLayerIds)).ToList();

		foreach (var id in mapIds)
		{
            if (alreadyProcessed.Contains(id)) continue;

			var map = Simulators[id];
			var rules = map.region != null ? regionRules[map.region.RegionId] : globalRules;
            dict[id] = map.parseRules(rules);
		}
	}

    RuleDict _parseAndApplyToAll(RuleDict startingRules, short[] ids)
    {
        var rules = startingRules;
        foreach (var id in ids)
        {
            rules = Simulators[id].parseRules(rules);
        }
        foreach (var id in ids)
        {
            Simulators[id].setAllRules(rules);
        }
        return rules;
    }

    public void Step(short currentMap, short[] mapIds, Direction direction, ObjectTypeId playerNumber)
    {
        var sims = mapIds.Select(id => Simulators[id]).ToList();

        // this should be recalculated at all points where it's possible to change, like in transform() or interactionblock()
        var yous = mapIds.ToDictionary(id => id, id => Simulators[id].findObjectsThatAre(playerNumber).ToArray());

        parseMapRules(mapIds);

        var didAnyMove = false;
        foreach (var map in sims)
        {
            if (map.doMovement(map.MapId == currentMap ? direction : Direction.None, yous[map.MapId])) 
                didAnyMove = true;
        }

        if (!didAnyMove) return;

        parseMapRules(mapIds);
        foreach (var map in sims) map.transform();
        parseMapRules(mapIds);
        foreach (var map in sims) map.moveblock();
        parseMapRules(mapIds);
        foreach (var map in sims) map.fallblock();
        parseMapRules(mapIds);
        foreach (var map in sims) map.statusblock();
        parseMapRules(mapIds);
        foreach (var map in sims) map.interactionblock();
        parseMapRules(mapIds);
        foreach (var map in sims) map.collisionCheck(yous[map.MapId]);
        parseMapRules(mapIds);
        foreach (var map in sims)
        {
            map.removeDuplicatesInSamePosition();
            map.doJoinables();
        }
    }

    public bool TestInventory(Dictionary<ObjectTypeId, int> needs)
    {
        foreach (var (reagent, count) in needs)
        {
            if (!Inventory.TryGetValue(reagent, out var has) || has < count)
                return false;
        }
        return true;
    }

    public bool ConsumeFromInventory(Dictionary<ObjectTypeId, int> needs)
    {
        if (!TestInventory(needs)) return false;

        foreach (var (reagent, count) in needs)
        {
            Inventory[reagent] -= count;
        }
        return true;
    }

    public bool AddToInventory(ObjectTypeId id, int amount)
    {
        Inventory[id] = Inventory.TryGetValue(id, out var current) ? current + amount : amount;
        return true;
    }
}
