namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetClickEffect()
        {
            _e.SelectedCellIdx = 0;

            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                if (_e.PlayerInfoE(_e.CurPlayerIT).PlayerInfoC.HaveKingInInventor)
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