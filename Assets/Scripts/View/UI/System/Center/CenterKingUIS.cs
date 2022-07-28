using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;
namespace Chessy.Model
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

            if (PlayerInfoE(AboutGameC.CurrentPlayerIType).PlayerInfoC.HaveKingInInventorP && AboutGameC.CellClickType != CellClickTypes.SetUnit)
            {
                if (!AboutGameC.LessonType.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActive = true;
                }
            }

            _eUI.CenterEs.KingE.Paren.TrySetActive(_needActive);
        }
    }
}
