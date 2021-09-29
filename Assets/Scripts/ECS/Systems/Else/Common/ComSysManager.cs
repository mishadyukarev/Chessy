using Assets.Scripts.ECS.Systems.Else.Common.Else;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class ComSysManager : SystemAbstManager
    {
        private PhotonSceneSys _photSceneSys;
        internal LaunchAdComSys LaunchAdSys { get; private set; }

        internal ComSysManager(EcsWorld comWorld, EcsSystems allComSys) : base(comWorld, allComSys)
        {
            _photSceneSys = Main.Instance.gameObject.AddComponent<PhotonSceneSys>();
            LaunchAdSys = new LaunchAdComSys();

            InitOnlySystems
                .Add(_photSceneSys)
                .Add(LaunchAdSys);
        }
    }
}
