using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame _eUI;

        internal CenterHeroesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = entsUI;
        }

        public void Run()
        {
            var isActiveKingZone = _eUI.CenterEs.KingE.Paren.IsActiveSelf;
            var curPlayerI = e.CurPlayerITC.PlayerT;

            if (!isActiveKingZone && e.PlayerInfoE(curPlayerI).GodInfoE.UnitTC.Is(UnitTypes.None))
            {
                //var myHeroT = E.PlayerE(curPlayerI).AvailableHeroTC.Unit;

                _eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else
            {
                _eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent
                    .SetActive(false);
            }
        }
    }
}