using Leopotam.Ecs;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemGeneralReduction : IEcsInitSystem, IEcsRunSystem
    {
        protected EntitiesGameGeneralManager _eGM;
        protected SystemsGameGeneralManager _sGM;

        protected SystemGeneralReduction()
        {

        }

        public virtual void Init()
        {
            _eGM = Instance.ECSmanager.EntitiesGameGeneralManager;
            _sGM = Instance.ECSmanager.SystemsGameGeneralManager;
        }

        public virtual void Run()
        {

        }
    }
}