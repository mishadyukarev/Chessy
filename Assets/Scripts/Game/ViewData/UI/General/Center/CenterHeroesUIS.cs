namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterHeroesUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var isActiveKingZone = UIEs.CenterEs.KingE.Paren.IsActiveSelf;
            var curPlayerI = E.CurPlayerITC.Player;

            if (!isActiveKingZone && !UIEs.CenterEs.UpgradeE.Parent.IsActiveSelf
                && E.PlayerE(curPlayerI).HaveCenterHero)
            {
                //var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else
            {
                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}