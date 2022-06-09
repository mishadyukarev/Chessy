using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract
    {
        bool _needActive;
        readonly EntitiesViewUIGame _eUI;

        internal CenterKingUIS(in EntitiesViewUIGame eUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = eUI;
        }

        internal override void Sync()
        {
            _needActive = false;

            if (e.PlayerInfoE(e.CurPlayerIT).KingInfoE.HaveInInventor && e.CellClickTC.CellClickT != CellClickTypes.SetUnit)
            {
                if (!e.LessonTC.Is(LessonTypes.YouNeedDestroyKing, LessonTypes.ThatIsYourSpawn))
                {
                    _needActive = true;
                }
            }

            _eUI.CenterEs.KingE.Paren.SetActive(_needActive);
        }
    }
}
