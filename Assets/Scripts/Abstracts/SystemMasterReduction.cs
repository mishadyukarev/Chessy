using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal abstract class SystemMasterReduction : SystemGeneralReduction
    {
        protected GameMasterSystemManager _eMM;

        public override void Init()
        {
            base.Init();

            _eMM = Instance.ECSmanager.GameMasterSystemManager;
        }
    }
}