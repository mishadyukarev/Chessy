namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetClickEffect()
        {
            IndexesCellsC.Selected = 0;

            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (PlayerInfoE(AboutGameC.CurrentPlayerIT).PlayerInfoC.HaveKingInInventor)
            {
                _selectedUnitC.UnitT = UnitTypes.King;
                _selectedUnitC.LevelT = LevelTypes.First;

                AboutGameC.CellClickT = CellClickTypes.SetUnit;
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}