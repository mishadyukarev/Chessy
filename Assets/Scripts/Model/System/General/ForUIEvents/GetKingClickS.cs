﻿namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetClickEffect()
        {
            _e.SelectedCellIdx = 0;

            if (_e.CurrentPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                if (_e.PlayerInfoE(_e.CurrentPlayerIT).PlayerInfoC.HaveKingInInventor)
                {
                    _e.SelectedUnitC.UnitT = UnitTypes.King;
                    _e.SelectedUnitC.LevelT = LevelTypes.First;

                    _e.CellClickT = CellClickTypes.SetUnit;
                }
            }
            else _s.Mistake(MistakeTypes.NeedWaitQueue);

            _e.NeedUpdateView = true;
        }
    }
}