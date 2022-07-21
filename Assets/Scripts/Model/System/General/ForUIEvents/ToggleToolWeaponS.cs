using Chessy.Model.Enum;
using Photon.Pun;

namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ToggleToolWeapon(in ToolsWeaponsWarriorTypes twT)
        {
            _e.SoundAction(ClipTypes.Click).Invoke();

            _cellsC.Selected = 0;

            if (_aboutGameC.CurrentPlayerIT.Is(_aboutGameC.CurrentPlayerIT))
            {
                //if (_eMG.LessonTC.Is(LessonTypes.ClickPick))
                //{
                //    if (twT == ToolWeaponTypes.Pick)
                //    {
                //        _eMG.LessonTC.SetNextLesson();
                //    }
                //}

                if (_e.PlayerInfoE(_aboutGameC.CurrentPlayerIT).PawnInfoC.AmountInGame > 0)
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

                    if (twT == ToolsWeaponsWarriorTypes.Shield || twT == ToolsWeaponsWarriorTypes.BowCrossbow)
                    {
                        if (_aboutGameC.CellClickT.Is(CellClickTypes.GiveTakeTW))
                        {
                            if (twT == ToolsWeaponsWarriorTypes.Shield || twT == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                if (_selectedToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                            }
                            else if (twT != ToolsWeaponsWarriorTypes.BowCrossbow) levT = LevelTypes.Second;
                        }
                        else
                        {
                            levT = _selectedToolWeaponC.LevelT;
                        }
                    }
                    else if (twT == ToolsWeaponsWarriorTypes.Axe || twT == ToolsWeaponsWarriorTypes.Sword)
                    {
                        levT = LevelTypes.Second;
                    }

                    _selectedToolWeaponC.ToolWeaponT = twT;
                    _selectedToolWeaponC.LevelT = levT;


                    _aboutGameC.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    _mistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    _mistakeC.Timer = 0;
                    _e.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                _mistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                _mistakeC.Timer = 0;
                _e.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            _updateAllViewC.NeedUpdateView = true;
        }

        public void Melt()
        {
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryMeltInMelterBuildingM) });
        }
    }
}