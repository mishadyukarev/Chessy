using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class CenterKingUIS : SystemUIAbstract
    {
        bool _needActive;
        readonly EntitiesViewUI _eUI;

        internal CenterKingUIS(in EntitiesViewUI eUI, in EntitiesModel ents) : base(ents)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            _needActive = false;

            if (_e.PlayerInfoE(_aboutGameC.CurrentPlayerIType).PlayerInfoC.HaveKingInInventorP && _aboutGameC.CellClickType != CellClickTypes.SetUnit)
            {
                if (!_aboutGameC.LessonType.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActive = true;
                }
            }

            _eUI.CenterEs.KingE.Paren.TrySetActive(_needActive);
        }
    }
}
