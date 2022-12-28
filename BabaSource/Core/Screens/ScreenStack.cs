﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Screens
{
    public class ScreenStack
    {
        private readonly Stack<BaseScreen> stack = new();
        private readonly GameObject container;

        public ScreenStack(GameObject container)
        {
            this.container = container;
        }

        public void Add(BaseScreen screen)
        {
            container.RemoveAllChildren();
            stack.Push(screen);
            var visibleScreens = new List<BaseScreen>();
            foreach (var item in stack)
            {
                visibleScreens.Insert(0, item);
                if (!item.Transparent) break;
            }
            foreach (var item in visibleScreens)
            {
                item.HideCommands();
                container.AddChild(item);
            }
            screen.ShowCommands();
        }

        /// <summary>
        /// Makes sure that this screen is on the top of the stack
        /// </summary>
        /// <param name="screen"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void EnsureTop(BaseScreen? screen)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("Screen cannot be null");
            }
            PopTo(screen);
            Pop();
            Add(screen);
        }

        public void Pop()
        {
            var popped = stack.TryPop(out var poppedItem);
            if (popped)
                container.RemoveChild(poppedItem);
            if (stack.TryPeek(out var top))
                top.ShowCommands();
        }

        public void PopTo(BaseScreen? screen)
        {
            while (stack.TryPeek(out var top) && top != screen) { 
                Pop(); 
            }
            screen?.ShowCommands();
        }
    }
}
