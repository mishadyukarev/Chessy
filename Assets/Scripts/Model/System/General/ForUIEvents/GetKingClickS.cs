namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void GetClickEffect()
        {
            indexesCellsC.Selected = 0;

            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            if (PlayerInfoE(aboutGameC.CurrentPlayerIT).PlayerInfoC.HaveKingInInventor)
            {
                selectedUnitC.UnitT = UnitTypes.King;
                selectedUnitC.LevelT = LevelTypes.First;

                aboutGameC.CellClickT = CellClickTypes.SetUnit;
            }

            updateAllViewC.NeedUpdateView = true;
        }
    }
}