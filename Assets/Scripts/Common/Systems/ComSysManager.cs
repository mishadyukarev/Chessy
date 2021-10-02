using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.Systems.Else.Common.Else;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class ComSysManager : SystemAbstManager
    {
        private PhotonSceneSys _photSceneSys;
        public LaunchAdComSys LaunchAdSys { get; private set; }

        public ComSysManager(EcsWorld comWorld, EcsSystems allComSys) : base(comWorld, allComSys)
        {
            _photSceneSys = SpawnInitComSys.Main_GO.AddComponent<PhotonSceneSys>();
            LaunchAdSys = new LaunchAdComSys();

            InitOnlySystems
                .Add(_photSceneSys)
                .Add(LaunchAdSys);
        }
    }
}
