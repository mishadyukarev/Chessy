namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void GetClickEffect()
        {
            _e.CellsC.Selected = 0;

            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                if (_e.PlayerInfoE(_e.CurPlayerIT).KingInfoE.HaveInInventor)
                {
                    _e.SelectedUnitE.UnitT = UnitTypes.King;
                    _e.SelectedUnitE.LevelT = LevelTypes.First;

                    _e.CellClickT = CellClickTypes.SetUnit;
                }
            }
            else _s.Mistake(MistakeTypes.NeedWaitQueue);

            _e.NeedUpdateView = true;
        }
    }
}