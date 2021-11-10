using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct DataMastC
    {
        private static Dictionary<MastDataSysTypes, Action> _systems;
        private static Dictionary<RpcMasterTypes, Action> _rpcSysts;
        private static Dictionary<UniqAbilTypes, Action> _uniqAbil;

        public DataMastC(List<object> list)
        {
            var i = 0;
            _systems = (Dictionary<MastDataSysTypes, Action>)list[i++];
            _rpcSysts = (Dictionary<RpcMasterTypes, Action>)list[i++];
            _uniqAbil = (Dictionary<UniqAbilTypes, Action>)list[i++];
        }

        public static void InvokeRun(MastDataSysTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public static void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
        public static void InvokeRun(UniqAbilTypes uniqAbil) => _uniqAbil[uniqAbil].Invoke();
    }
}


