using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct ViewDataSC
    {
        static Dictionary<ViewDataSTypes, Action> _actions;

        public ViewDataSC(Dictionary<ViewDataSTypes, Action> actions)
        {
            _actions = actions;
        }

        public static void Run(ViewDataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type].Invoke();
        }
    }
}