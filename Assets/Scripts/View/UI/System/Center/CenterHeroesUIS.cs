using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;

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

            if (!isActiveKingZone && godInfoCs[(byte)aboutGameC.CurrentPlayerIType].UnitT.Is(UnitTypes.None) && aboutGameC.CellClickType != CellClickTypes.SetUnit)
            {
                if (!aboutGameC.LessonType.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActiveZone = true;
                }
            }


            _eUI.CenterEs.HeroE(UnitTypes.Elfemale).Parent.TrySetActive(_needActiveZone);


            if (_needActiveZone)
            {
                var nextPlayerT = aboutGameC.CurrentPlayerIType.NextPlayer();
                var haveElfemaleEnemy = godInfoCs[(byte)nextPlayerT].UnitT.Is(UnitTypes.Elfemale);
                var haveSnowyEnemy = godInfoCs[(byte)nextPlayerT].UnitT.Is(UnitTypes.Snowy);
                _eUI.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.SetActiveParent(!haveElfemaleEnemy);
                _eUI.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.SetActiveParent(!haveSnowyEnemy);
            }
        }
    }
}