using Chessy.Common.Enum;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void ToggleToolWeapon(in ToolWeaponTypes twT)
        {
            _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();

            if (_eMG.LessonTC.Is(LessonTypes.ThatsYourDamage, LessonTypes.ThatsYourEffects, LessonTypes.ClickDefend)) return;

            _eMG.CellsC.Selected = 0;

            if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerTC.PlayerT))
            {
                //if (_eMG.LessonTC.Is(LessonTypes.ClickPick))
                //{
                //    if (twT == ToolWeaponTypes.Pick)
                //    {
                //        _eMG.LessonTC.SetNextLesson();
                //    }
                //}

                if (_eMG.PlayerInfoE(_eMG.WhoseMovePlayerTC.PlayerT).PawnInfoC.AmountInGame > 0)
                {
                    //if (tw == ToolWeaponTypes.Pick)
                    //{
                    //    TryOnHint(VideoClipTypes.Pick);
                    //}
                    //else
                    //{
                    //    TryOnHint(VideoClipTypes.UpgToolWeapon);
                    //}


                    var levT = LevelTypes.First;

                    if (twT == ToolWeaponTypes.Shield || twT == ToolWeaponTypes.BowCrossbow)
                    {
                        if (_eMG.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            if (twT == ToolWeaponTypes.Shield || twT == ToolWeaponTypes.BowCrossbow)
                            {
                                if (_eMG.SelectedE.ToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                            }
                            else if (twT != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                        }
                        else
                        {
                            levT = _eMG.SelectedE.ToolWeaponC.LevelT;
                        }
                    }
                    else if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.Sword)
                    {
                        levT = LevelTypes.Second;
                    }

                    _eMG.SelectedE.ToolWeaponC.ToolWeaponT = twT;
                    _eMG.SelectedE.ToolWeaponC.LevelT = levT;


                    _eMG.CellClickTC.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    _eMG.MistakeTC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    _eMG.MistakeTimerC.Timer = 0;
                    _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                _eMG.MistakeTC.MistakeT = MistakeTypes.NeedWaitQueue;
                _eMG.MistakeTimerC.Timer = 0;
                _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            _eMG.NeedUpdateView = true;
        }

        public void Melt()
        {
            _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TryMeltInMelterBuildingM) });
        }
    }
}