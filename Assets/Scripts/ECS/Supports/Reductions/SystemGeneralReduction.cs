using Leopotam.Ecs;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemGeneralReduction : IEcsInitSystem, IEcsRunSystem
    {
        protected EntitiesGameGeneralManager _eGM;
        protected SystemsGameGeneralManager _sGM;

        protected StartGameValuesConfig _startValuesGameConfig;
        protected CellManager _cellM;
        protected EconomyManager _econM;

        protected SystemGeneralReduction()
        {
            _startValuesGameConfig = Instance.StartValuesGameConfig;
        }

        public virtual void Init()
        {
            _eGM = Instance.ECSmanager.EntitiesGameGeneralManager;
            _sGM = Instance.ECSmanager.SystemsGameGeneralManager;
            _cellM = Instance.ECSmanager.CellManager;
            _econM = Instance.ECSmanager.EconomyManager;
        }

        public virtual void Run()
        {

        }
    }
}