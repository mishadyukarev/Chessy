using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenHeroClick()
        {
            _e.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
            {
                _e.Com.SoundActionC(ClipCommonTypes.Click).Invoke();

                var curPlayer = _e.CurPlayerIT;

                var myHeroT = _e.PlayerInfoE(curPlayer).GodInfoE.UnitT;

                if (_e.PlayerInfoE(curPlayer).GodInfoE.HaveHeroInInventor)
                {
                    if (!_e.PlayerInfoE(_e.CurPlayerIT).GodInfoE.CooldownC.HaveCooldown())
                    {
                        _e.SelectedUnitE.UnitT = myHeroT;
                        _e.SelectedUnitE.LevelT = LevelTypes.First;


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