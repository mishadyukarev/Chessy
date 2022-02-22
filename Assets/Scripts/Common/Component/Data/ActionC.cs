﻿
using System;

namespace Game.Game
{
    public struct ActionC
    {
        public Action Action;

        public ActionC(in Action action) => Action = action;

        public void Invoke() => Action?.Invoke();
    }
}