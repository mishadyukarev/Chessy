using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct DataMastSC
    {
        static Action _runUpdSysts;
        static Dictionary<MastDataSysTypes, Action> _systems;
        static Dictionary<RpcMasterTypes, Action> _rpcSysts;
        static Dictionary<UniqueAbilTypes, Action> _uniqAbil;

        public DataMastSC(List<object> list)
        {
            var i = 0;
            _runUpdSysts = (Action)list[i++];
            _systems = (Dictionary<MastDataSysTypes, Action>)list[i++];
            _rpcSysts = (Dictionary<RpcMasterTypes, Action>)list[i++];
            _uniqAbil = (Dictionary<UniqueAbilTypes, Action>)list[i++];
        }

        public static void RunUpdate() => _runUpdSysts?.Invoke();
        public static void InvokeRun(MastDataSysTypes mastDataSys) => _systems[mastDataSys].Invoke();
        public static void InvokeRun(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Invoke();
            else throw new System.Exception();
        }
        public static void InvokeRun(UniqueAbilTypes uniqAbil) => _uniqAbil[uniqAbil].Invoke();
    }
}


