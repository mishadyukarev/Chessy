using Photon.Pun;

namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ToggleToolWeapon(in ToolsWeaponsWarriorTypes twT)
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();

            IndexesCellsC.Selected = 0;

            if (AboutGameC.CurrentPlayerIT.Is(AboutGameC.CurrentPlayerIT))
            {
                //if (_eMG.LessonTC.Is(LessonTypes.ClickPick))
                //{
                //    if (twT == ToolWeaponTypes.Pick)
                //    {
                //        _eMG.LessonTC.SetNextLesson();
                //    }
                //}

                if (PlayerInfoE(AboutGameC.CurrentPlayerIT).PawnInfoC.AmountInGame > 0)
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
                        if (AboutGameC.CellClickT.Is(CellClickTypes.GiveTakeTW))
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


                    AboutGameC.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    _mistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    _mistakeC.Timer = 0;
                    _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                _mistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                _mistakeC.Timer = 0;
                _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            _updateAllViewC.NeedUpdateView = true;
        }

        public void Melt()
        {
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryMeltInMelterBuildingM) });
        }
    }
}