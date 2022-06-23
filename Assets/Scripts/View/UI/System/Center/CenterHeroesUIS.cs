﻿using Chessy.Model.Enum;
using Chessy.Model.Extensions;
using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class CenterHeroesUIS : SystemUIAbstract
    {
        bool _needActiveZone;
        readonly EntitiesViewUI _eUI;

        internal CenterHeroesUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
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