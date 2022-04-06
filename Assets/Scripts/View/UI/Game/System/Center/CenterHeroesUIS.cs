using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame _eUI;

        internal CenterHeroesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = entsUI;
        }

        internal override void Sync()
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


            var nextPlayerT = e.CurPlayerIT.NextPlayer();
            var haveElfemaleEnemy = e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale);
            var haveSnowyEnemy = e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy);
            _eUI.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.SetActiveParent(!haveElfemaleEnemy);
            _eUI.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.SetActiveParent(!haveSnowyEnemy);
        }
    }
}