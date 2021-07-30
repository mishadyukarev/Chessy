using Leopotam.Ecs;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemGeneralReduction : IEcsInitSystem, IEcsRunSystem
    {
        protected EntGameGeneralElseDataManager _eGM;
        protected EntitiesGameGeneralUIViewManager _eGGUIM;
        protected SystemsGameGeneralManager _sGM;

        protected SystemGeneralReduction()
        {

        }

        public virtual void Init()
        {
            _eGM = Instance.ECSmanager.EntGameGeneralElseDataManager;
            _eGGUIM = Instance.ECSmanager.EntGameGeneralUIViewManager;
            _sGM = Instance.ECSmanager.SysGameGeneralManager;
        }

        public virtual void Run()
        {

        }
    }
}