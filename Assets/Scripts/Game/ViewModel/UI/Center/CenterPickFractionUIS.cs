namespace Chessy.Game
{
    sealed class CenterPickFractionUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterPickFractionUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var curPlayer = E.CurPlayerITC.Player;

            var isActivatedZone = !E.PlayerE(curPlayer).HaveFraction && !E.UnitInfo(curPlayer, LevelTypes.First, UnitTypes.King).HaveInInventor;

            UIE.CenterEs.UpgradeE.Parent.SetActive(isActivatedZone);

            if (isActivatedZone)
            {
                
            }
        }
    }
}