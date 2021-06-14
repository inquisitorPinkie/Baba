﻿using Baba;
using BabaGame.src.Engine;
using BabaGame.src.Events;
using BabaGame.src.Objects;
using BabaGame.src.Resources;
using Core;
using Core.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabaGame.src
{
    public class World : GameObject
    {
        private WorldStructure data;

        private DateTime lastInput;

        private bool allowInput = false;

        private Dictionary<(int, int), MapDisplay> MapDisplays;

        private List<Keys> AllKeysPressed;

        public World(string world)
        {
            MapDisplays = new Dictionary<(int, int), MapDisplay>();

            lastInput = DateTime.Today;
            data = new WorldStructure(world);
            EventChannels.MapChange.Subscribe(setMap);
            EventChannels.KeyPress.Subscribe(onKeyPress);
            EventChannels.CharacterControl.Subscribe(onCharacterControl);

            var (px, ph) = WorldVariables.GetSizeInPixels(WorldVariables.MapWidth + 2, WorldVariables.MapHeight + 2);
            var scale = BabaGame.Game?.SetScreenSize(px, ph);
            WorldVariables.Scale = (scale ?? 1f) * WorldVariables.BaseScale;

            AllKeysPressed = new List<Keys>();
        }

        protected override void OnDestroy()
        {
            EventChannels.MapChange.Unsubscribe(setMap);
            EventChannels.KeyPress.Unsubscribe(onKeyPress);
            EventChannels.CharacterControl.Unsubscribe(onCharacterControl);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            Graphics.xscale = WorldVariables.Scale;
            Graphics.yscale = WorldVariables.Scale;

            base.OnUpdate(gameTime);

            if (allowInput && AllKeysPressed?.Count > 0)
            {
                if (DateTime.Now - lastInput >= TimeSpan.FromSeconds(WorldVariables.InputDelaySeconds + WorldVariables.MoveAnimationSeconds))
                {
                    lastInput = DateTime.Now;

                    if (AllKeysPressed.Contains(KeyMap.Up))
                    {
                        data.TakeAction("up");
                    }
                    else if (AllKeysPressed.Contains(KeyMap.Down))
                    {
                        data.TakeAction("down");
                    }
                    else if (AllKeysPressed.Contains(KeyMap.Left))
                    {
                        data.TakeAction("left");
                    }
                    else if (AllKeysPressed.Contains(KeyMap.Right))
                    {
                        data.TakeAction("right");
                    }
                    else if (AllKeysPressed.Contains(KeyMap.Wait))
                    {
                        data.TakeAction("wait");
                    }
                }

            }

        }

        void onCharacterControl(CharacterControl characterControl)
        {
            allowInput = characterControl.Enable;
        }

        void setMap(MapChange ev)
        {
            if (ev.Direction != null)
            {
                EventChannels.CharacterControl.SendAsyncMessage(new CharacterControl { Enable = true });
            }

            var (px, py) = WorldVariables.GetSizeInPixels(1, 1);
            Graphics.x = px;
            Graphics.y = py;

            Graphics.children.Clear();
            children.Clear();

            data.SetMap(ev);

            foreach (var (dx, dy) in loadMapDeltas)
            {
                if (data.GetOrLoadMap(ev.X + dx, ev.Y + dy) is MapData md)
                {
                    if (!MapDisplays.ContainsKey((ev.X + dx, ev.Y + dy)))
                    {
                        var display = new MapDisplay();
                        MapDisplays[(ev.X + dx, ev.Y + dy)] = display;

                        foreach (var obj in md.AllObjects)
                        {
                            display.AddChild(obj, addGraphics: true);
                        }

                        AddChild(display, addGraphics: true);
                        display.Graphics.x = dx * WorldVariables.MapWidth * WorldVariables.TileWidth;
                        display.Graphics.y = dy * WorldVariables.MapHeight * WorldVariables.TileHeight;
                    }
                }

            }

        }

        void onKeyPress(KeyEvent keyEvent)
        {
            if (data == null) return;
            if (!keyEvent.Up && AllKeysPressed.Contains(keyEvent.ChangedKey) == false)
            {
                AllKeysPressed.Add(keyEvent.ChangedKey);
            }
            else if (keyEvent.Up && AllKeysPressed.Contains(keyEvent.ChangedKey))
            {
                AllKeysPressed.Remove(keyEvent.ChangedKey);
            }
        }

        private static (int x, int y)[] loadMapDeltas = new[]
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1),
            (0, 0),
        };

    }
}
