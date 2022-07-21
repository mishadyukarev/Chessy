namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenHeroClick()
        {
            _cellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            _e.SoundAction(ClipTypes.Click).Invoke();

            var curPlayer = _aboutGameC.CurrentPlayerIT;

            var myHeroT = _e.PlayerInfoE(curPlayer).GodInfoC.UnitType;

            if (_e.PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
            {
                if (!_e.PlayerInfoE(_aboutGameC.CurrentPlayerIT).GodInfoC.HaveCooldown)
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