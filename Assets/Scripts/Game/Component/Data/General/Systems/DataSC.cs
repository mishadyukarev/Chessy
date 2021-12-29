using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct DataSC
    {
        static Dictionary<DataSTypes, Action> _actions;

        public DataSC(in List<object> list)
        {
            var idx = 0;

            _actions = (Dictionary<DataSTypes, Action>)list[idx++];
        }

        public static void Run(in DataSTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type].Invoke();
        }
    }
}

