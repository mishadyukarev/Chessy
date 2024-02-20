using Photon.Pun;

namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ToggleToolWeapon(in ToolsWeaponsWarriorTypes twT)
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            indexesCellsC.Selected = 0;

            if (aboutGameC.CurrentPlayerIT.Is(aboutGameC.CurrentPlayerIT))
            {
                //if (_eMG.LessonTC.Is(LessonTypes.ClickPick))
                //{
                //    if (twT == ToolWeaponTypes.Pick)
                //    {
                //        _eMG.LessonTC.SetNextLesson();
                //    }
                //}

                if (pawnPeopleInfoCs[(byte)aboutGameC.CurrentPlayerIT].AmountInGame > 0)
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
                        if (aboutGameC.CellClickT.Is(CellClickTypes.GiveTakeTW))
                        {
                            if (twT == ToolsWeaponsWarriorTypes.Shield || twT == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                if (selectedToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                            }
                            else if (twT != ToolsWeaponsWarriorTypes.BowCrossbow) levT = LevelTypes.Second;
                        }
                        else
                        {
                            levT = selectedToolWeaponC.LevelT;
                        }
                    }
                    else if (twT == ToolsWeaponsWarriorTypes.Axe || twT == ToolsWeaponsWarriorTypes.Sword)
                    {
                        levT = LevelTypes.Second;
                    }

                    selectedToolWeaponC.ToolWeaponT = twT;
                    selectedToolWeaponC.LevelT = levT;


                    aboutGameC.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    mistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    mistakeC.Timer = 0;
                    dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                mistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                mistakeC.Timer = 0;
                dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            updateAllViewC.NeedUpdateView = true;
        }

        public void Melt()
        {
            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryMeltInMelterBuildingM) });
        }
    }
}