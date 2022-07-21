namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenHeroClick()
        {
            _cellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            var curPlayer = _aboutGameC.CurrentPlayerIT;

            var myHeroT = PlayerInfoE(curPlayer).GodInfoC.UnitType;

            if (PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
            {
                if (!PlayerInfoE(_aboutGameC.CurrentPlayerIT).GodInfoC.HaveCooldown)
                {
                    _selectedUnitC.UnitT = myHeroT;
                    _selectedUnitC.LevelT = LevelTypes.First;


                    _aboutGameC.CellClickT = CellClickTypes.SetUnit;
                }
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}