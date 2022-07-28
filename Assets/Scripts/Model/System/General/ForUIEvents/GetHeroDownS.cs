namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenHeroClick()
        {
            IndexesCellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            var curPlayer = AboutGameC.CurrentPlayerIT;

            var myHeroT = PlayerInfoE(curPlayer).GodInfoC.UnitType;

            if (PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
            {
                if (!PlayerInfoE(AboutGameC.CurrentPlayerIT).GodInfoC.HaveCooldown)
                {
                    _selectedUnitC.UnitT = myHeroT;
                    _selectedUnitC.LevelT = LevelTypes.First;


                    AboutGameC.CellClickT = CellClickTypes.SetUnit;
                }
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}