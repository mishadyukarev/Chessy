namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenHeroClick()
        {
            _e.SelectedCellIdx = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.SoundAction(ClipTypes.Click).Invoke();

                var curPlayer = _e.CurPlayerIT;

                var myHeroT = _e.PlayerInfoE(curPlayer).GodInfoC.UnitT;

                if (_e.PlayerInfoE(curPlayer).GodInfoC.HaveGodInInventor)
                {
                    if (!_e.PlayerInfoE(_e.CurPlayerIT).GodInfoC.HaveCooldown)
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