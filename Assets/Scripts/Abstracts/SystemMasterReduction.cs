using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemMasterReduction : SystemGeneralReduction
    {
        protected EntGameMasterManager _eMM;

        public override void Init()
        {
            base.Init();

            _eMM = Instance.ECSmanager.EntGameMasterManager;
        }
    }
}