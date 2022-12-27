﻿using Content;
using Content.UI;
using Core.Bootstrap;
using Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Editor
{
    public class EditorGame : GameSetup
    {
        public EditorGame() : base(new EditorGameEntryPoint())
        {
            MAX_WIDTH = 1080;
            MAX_HEIGHT = 720;
        }

        protected override void Initialize()
        {
            ContentLoader.LoadContent(GraphicsDevice);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            // TODO: Add your update logic here
        }

        private class EditorGameEntryPoint : GameEntryPoint
        {
            public override void Initialize()
            {
                AddChild(new Text("[red]hello-[blue]there/[100,255,100]?\nBaba [arrow] [white][baba:17][green] says:[200, 200, 200] \"hi\"\n\t[gray][wall:1][wall:5][wall:5][wall:5][wall:4]"), true);

                var buffer = new List<char>();
                var pre = "Type here:\n";
                var t = new Text(pre);
                t.Graphics.y = 100;

                CoreEventChannels.KeyEvent.Subscribe(ev => {
                    if (ev.Up) return;

                    if (ev.ChangedKey == Keys.Back)
                    {
                        if (buffer.Count > 0)
                        {
                            buffer.RemoveAt(buffer.Count - 1);
                            t.SetText(pre + string.Join("", buffer));
                        }
                    }
                    if (ev.ChangedKey == Keys.Enter)
                    {
                        buffer.Add('\n');
                        t.SetText(pre + string.Join("", buffer));
                    }
                });

                CoreEventChannels.TextInput.Subscribe(ev =>
                {
                    if (ev.Character == 8) return;
                    if (ev.Character == 10) return;
                    if (ev.Character == 13) return;
                    buffer.Add(ev.Character);
                    t.SetText(pre + string.Join("", buffer));
                });

                AddChild(t, true);
            }
        }
    }
}
