namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterHeroesUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var isActiveKingZone = eUI.CenterEs.KingE.Paren.IsActiveSelf;
            var curPlayerI = e.CurPlayerITC.Player;

            if (!isActiveKingZone && e.PlayerInfoE(curPlayerI).MyHeroTC.Is(UnitTypes.None))
            {
                //var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else
            {
                eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}