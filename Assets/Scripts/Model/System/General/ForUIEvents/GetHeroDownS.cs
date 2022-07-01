namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenHeroClick()
        {
            _e.SelectedCellIdx = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (_e.CurrentPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                var curPlayer = _e.CurrentPlayerIT;

                var myHeroT = _e.PlayerInfoE(curPlayer).GodInfoC.UnitT;

                if (_e.PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
                {
                    if (!_e.PlayerInfoE(_e.CurrentPlayerIT).GodInfoC.HaveCooldown)
                    {
                        _e.SelectedUnitC.UnitT = myHeroT;
                        _e.SelectedUnitC.LevelT = LevelTypes.First;


                        _e.CellClickT = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                _s.SetMistake(MistakeTypes.NeedWaitQueue, 0);
                _e.SoundAction(ClipTypes.WritePensil).Invoke();
            }

            _e.NeedUpdateView = true;
        }
    }
}