using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct ViewDataSC
    {
        static Dictionary<ViewDataSTypes, Action> _actions;

        public ViewDataSC(List<object> list)
        {
            var idx = 0;

            _actions = (Dictionary<ViewDataSTypes, Action>)list[idx++];
        }

        public static void Run(ViewDataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type].Invoke();
        }
    }
}