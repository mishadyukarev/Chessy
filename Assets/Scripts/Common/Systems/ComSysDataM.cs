using Leopotam.Ecs;

namespace Scripts.Common
{
    public sealed class ComSysDataM : SystemAbstManager
    {
        private PhotonSceneSys _photSceneSys;
        public LaunchAdComSys LaunchAdSys { get; private set; }

        public ComSysDataM(EcsWorld comWorld, EcsSystems allComSys) : base(comWorld, allComSys)
        {
            _photSceneSys = SpawnInitComSys.Main_GO.AddComponent<PhotonSceneSys>();
            LaunchAdSys = new LaunchAdComSys();

            InitOnlySystems
                .Add(_photSceneSys)
                .Add(LaunchAdSys);
        }
    }
}
