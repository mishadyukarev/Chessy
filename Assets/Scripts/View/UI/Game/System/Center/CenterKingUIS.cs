using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.System;

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
