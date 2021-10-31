using Leopotam.Ecs;
using System.Collections.Generic;

namespace Scripts.Game
{
    public sealed class MastDataSysContainer
    {
        private static Dictionary<MastDataSysTypes, EcsSystems> _systems;
        private static Dictionary<RpcMasterTypes, EcsSystems> _rpcSysts;

        public MastDataSysContainer(Dictionary<MastDataSysTypes, EcsSystems> systems, Dictionary<RpcMasterTypes, EcsSystems> rpcSysts)
        {
            _systems = systems;
            _rpcSysts = rpcSysts;
        }

        public static void Run(MastDataSysTypes mastDataSys) => _systems[mastDataSys].Run();
        public static void Run(RpcMasterTypes rpc)
        {
            if (_rpcSysts.ContainsKey(rpc)) _rpcSysts[rpc].Run();
            else throw new System.Exception();
        }
    }
}


