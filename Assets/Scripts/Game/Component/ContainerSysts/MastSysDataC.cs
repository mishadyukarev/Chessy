using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct MastSysDataC
    {
        private static Dictionary<MastDataSysTypes, Action> _systems;
        private static Dictionary<RpcMasterTypes, Action> _rpcSysts;

        public MastSysDataC(Dictionary<MastDataSysTypes, Action> systems, Dictionary<RpcMasterTypes, Action> rpcSysts)
        {
            _systems = systems;
            _rpcSysts = rpcSysts;
        }

        public static void InvokeRun(MastDataSysTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public static void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
    }
}


