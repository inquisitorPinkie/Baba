﻿using BabaGame.Events;
using BabaGame.Screens;
using Core.Bootstrap;
using Core.Engine;
using Core.Screens;
using Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace BabaGame;

public class BabaGame : GameSetup
{
    public BabaGame() : base(new BabaGameEntryPoint())
    {
        MAX_WIDTH = 1080;
        MAX_HEIGHT = 720;
    }

    private class BabaGameEntryPoint : GameEntryPoint
    {
        public override void Initialize()
        {
            base.Initialize();

            SaveFile? saveFile = null;
            WorldData? selectedWorld = null;

            void select(WorldData wd)
            {
                selectedWorld = wd;
            }

            var saveFiles = LoadGameSaveFiles.LoadAllCompiledMaps();

            WorldSelectScreen? worldSelectScreen = null;
            SaveFileSelectScreen? saveFileSelectScreen = null;
            MainMenuScreen? mainMenuScreen = null;
            MainGamePlayScreen? mainGamePlayScreen = null;

            var stack = new ScreenStack();

            var stateMachine = new StateMachine<BabaGameState, KeyPress>("babaGame", BabaGameState.None)
                .State(
                    BabaGameState.PickingSaveFile,
                    @event => saveFileSelectScreen!.Handle(@event),
                    def => def
                        .AddOnEnter(() =>
                        {
                            saveFile ??= saveFiles.Values.First();
                            saveFileSelectScreen = new(saveFile, select, () =>
                            {
                                var newSave = WorldData.Deserialize(saveFile.InitialContent.Serialize());
                                newSave.Name = $"Save {(char)(saveFile.SaveFiles.Count + 'a')}";
                                LoadGameSaveFiles.SaveCompiledMap(newSave, saveFile.Name, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                                select(newSave);
                            });
                            stack.Add(saveFileSelectScreen);
                        })
                        .AddOnLeave(() => stack.Pop())
                )
                .State(
                    BabaGameState.PickingWorld,
                    @event => worldSelectScreen!.Handle(@event),
                    def => def
                        .AddOnEnter(() =>
                        {
                            worldSelectScreen = new(saveFiles.Values.ToList(), saveFile, s => {
                                saveFile = s;
                            });
                            stack.Add(worldSelectScreen);
                        })
                        .AddOnLeave(() => stack.Pop())
                        .SetShortCircuit(() => saveFiles.Count == 1 ? BabaGameState.PickingSaveFile : BabaGameState.None)
                )
                .State(
                    BabaGameState.PlayingGame,
                    @event => mainGamePlayScreen!.Handle(@event),
                    def => def
                        .AddOnEnter(() =>
                        {
                            mainGamePlayScreen = new(stack);
                            stack.Add(mainGamePlayScreen);
                            mainGamePlayScreen.init();
                        })
                        .AddOnLeave(() => stack.Pop())
                )
                .State(
                    BabaGameState.MainMenu,
                    ev => mainMenuScreen!.Handle(ev),
                    def => def
                        .AddOnEnter(() =>
                        {
                            mainMenuScreen = new();
                            stack.Add(mainMenuScreen);
                        })
                        .AddOnLeave(() => stack.Pop())
                )
                .State(BabaGameState.Exit, e => BabaGameState.None, def => def.AddOnEnter(() => Exit()));

            stateMachine.Initialize(BabaGameState.MainMenu);

            onKeyPress(ev => stateMachine.SendAction(ev));
            AddChild(stack);
        }
    }
}

internal enum BabaGameState
{
    None,
    Exit,

    MainMenu,
    SettingsMenu,

    PickingWorld,
    PickingSaveFile,
    PlayingGame,


}