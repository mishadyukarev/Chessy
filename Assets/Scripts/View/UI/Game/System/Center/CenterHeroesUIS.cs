using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterHeroesUIS : SystemUIAbstract
    {
        bool _needActiveZone;
        readonly EntitiesViewUIGame _eUI;

        internal CenterHeroesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = entsUI;
        }

        internal override void Sync()
        {
            _needActiveZone = false;

            var isActiveKingZone = _eUI.CenterEs.KingE.Paren.IsActiveSelf;
            var curPlayerI = e.CurPlayerITC.PlayerT;

            if (!isActiveKingZone && e.PlayerInfoE(curPlayerI).GodInfoE.UnitTC.Is(UnitTypes.None) && e.CellClickTC.CellClickT != CellClickTypes.SetUnit)
            {
                if (!e.LessonTC.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActiveZone = true;
                }
            }


            _eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(_needActiveZone);


            if (_needActiveZone)
            {
                var nextPlayerT = e.CurPlayerIT.NextPlayer();
                var haveElfemaleEnemy = e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale);
                var haveSnowyEnemy = e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy);
                _eUI.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.SetActiveParent(!haveElfemaleEnemy);
                _eUI.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.SetActiveParent(!haveSnowyEnemy);
            }
        }
    }
}