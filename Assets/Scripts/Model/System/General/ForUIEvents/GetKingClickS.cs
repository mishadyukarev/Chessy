namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetClickEffect()
        {
            _e.SelectedCellIdx = 0;

            _e.SoundAction(ClipTypes.Click).Invoke();

            if (_e.PlayerInfoE(_aboutGameC.CurrentPlayerIT).PlayerInfoC.HaveKingInInventor)
            {
                _e.SelectedUnitC.UnitT = UnitTypes.King;
                _e.SelectedUnitC.LevelT = LevelTypes.First;

                _e.CellClickT = CellClickTypes.SetUnit;
            }

            _e.NeedUpdateView = true;
        }
    }
}