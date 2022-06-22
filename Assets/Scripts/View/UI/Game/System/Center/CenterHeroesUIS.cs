using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.System;

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

            if (!isActiveKingZone && _e.PlayerInfoE(_e.CurPlayerIT).GodInfoE.UnitT.Is(UnitTypes.None) && _e.CellClickT != CellClickTypes.SetUnit)
            {
                if (!_e.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActiveZone = true;
                }
            }


            _eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(_needActiveZone);


            if (_needActiveZone)
            {
                var nextPlayerT = _e.CurPlayerIT.NextPlayer();
                var haveElfemaleEnemy = _e.PlayerInfoE(nextPlayerT).GodInfoE.UnitT.Is(UnitTypes.Elfemale);
                var haveSnowyEnemy = _e.PlayerInfoE(nextPlayerT).GodInfoE.UnitT.Is(UnitTypes.Snowy);
                _eUI.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.SetActiveParent(!haveElfemaleEnemy);
                _eUI.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.SetActiveParent(!haveSnowyEnemy);
            }
        }
    }
}