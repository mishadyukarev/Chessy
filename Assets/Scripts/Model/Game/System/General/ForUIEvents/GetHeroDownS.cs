using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenHeroClick()
        {
            _eMG.CellsC.Selected = 0;
            //TryOnHint(VideoClipTypes.CreatingHero);

            if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerT))
            {
                _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

                var curPlayer = _eMG.CurPlayerIT;

                var myHeroT = _eMG.PlayerInfoE(curPlayer).GodInfoE.UnitT;

                if (_eMG.PlayerInfoE(curPlayer).GodInfoE.HaveHeroInInventor)
                {
                    if (!_eMG.PlayerInfoE(_eMG.CurPlayerITC.PlayerT).GodInfoE.CooldownC.HaveCooldown)
                    {
                        _eMG.SelectedUnitE.UnitTC.UnitT = myHeroT;
                        _eMG.SelectedUnitE.LevelTC.LevelT = LevelTypes.First;


                        _eMG.CellClickTC.CellClickT = CellClickTypes.SetUnit;
                    }
                }
            }
            else
            {
                _sMG.SetMistake(MistakeTypes.NeedWaitQueue, 0);
                _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
            }

            _eMG.NeedUpdateView = true;
        }
    }
}