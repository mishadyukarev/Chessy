using Chessy.Model.Enum;
using Photon.Pun;

namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ToggleToolWeapon(in ToolsWeaponsWarriorTypes twT)
        {
            _e.SoundAction(ClipTypes.Click).Invoke();

            _e.SelectedCellIdx = 0;

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
                        if (_e.CellClickT.Is(CellClickTypes.GiveTakeTW))
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


                    _e.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    _e.MistakeT = MistakeTypes.NeedPawnsInGame;
                    _e.MistakeTimer = 0;
                    _e.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                _e.MistakeT = MistakeTypes.NeedWaitQueue;
                _e.MistakeTimer = 0;
                _e.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            _e.NeedUpdateView = true;
        }

        public void Melt()
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryMeltInMelterBuildingM) });
        }
    }
}