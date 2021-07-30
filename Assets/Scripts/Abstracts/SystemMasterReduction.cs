using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemMasterReduction : SystemGeneralReduction
    {
        protected EntitiesGameMasterManager _eMM;
        protected SystemsGameMasterManager _sMM;

        public override void Init()
        {
            base.Init();

            _eMM = Instance.ECSmanager.EntGameMasterManager;
            _sMM = Instance.ECSmanager.SysGameMasterManager;
        }
    }
}