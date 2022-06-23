using Chessy.Model.Enum;
using Chessy.Model.Model.Entity;

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

            if (_e.PlayerInfoE(_e.CurPlayerIT).KingInfoE.HaveInInventor && _e.CellClickT != CellClickTypes.SetUnit)
            {
                if (!_e.LessonT.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActive = true;
                }
            }

            _eUI.CenterEs.KingE.Paren.SetActive(_needActive);
        }
    }
}
