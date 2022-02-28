namespace Chessy.Game
{
    sealed class CenterPickFractionUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterPickFractionUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayer = E.CurPlayerITC.Player;

            var isActivatedZone = E.PlayerE(curPlayer).HaveCenterUpgrade && !E.UnitInfo(curPlayer, LevelTypes.First, UnitTypes.King).HaveInInventor;

            UIEs.CenterEs.UpgradeE.Parent.SetActive(isActivatedZone);

            if (isActivatedZone)
            {
                
            }
        }
    }
}