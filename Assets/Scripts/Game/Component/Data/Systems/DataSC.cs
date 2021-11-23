using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct DataSC
    {
        private static Dictionary<DataSystTypes, Action> _actions;

        public DataSC(List<object> list)
        {
            var idx = 0;

            _actions = (Dictionary<DataSystTypes, Action>)list[idx++];
        }

        public static void Run(DataSystTypes type)
        {
            if (!_actions.ContainsKey(type)) throw new Exception();

            _actions[type].Invoke();
        }
    }
}

