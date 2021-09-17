using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class ComSysManager : SystemAbstManager
    {
        private PhotonSceneSys _photSceneSys;

        internal ComSysManager(EcsWorld comWorld, EcsSystems allComSys) : base(comWorld, allComSys)
        {
            _photSceneSys = Main.Instance.gameObject.AddComponent<PhotonSceneSys>();

            InitOnlySystems
                .Add(_photSceneSys);
        }
    }
}
