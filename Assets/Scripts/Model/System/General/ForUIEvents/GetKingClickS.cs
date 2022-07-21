namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetClickEffect()
        {
            _cellsC.Selected = 0;

            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (PlayerInfoE(_aboutGameC.CurrentPlayerIT).PlayerInfoC.HaveKingInInventor)
            {
                _selectedUnitC.UnitT = UnitTypes.King;
                _selectedUnitC.LevelT = LevelTypes.First;

                _aboutGameC.CellClickT = CellClickTypes.SetUnit;
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}