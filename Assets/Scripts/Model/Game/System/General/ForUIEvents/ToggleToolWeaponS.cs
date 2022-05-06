using Chessy.Common.Enum;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class ToggleToolWeaponS : SystemModel
    {
        public ToggleToolWeaponS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Click(in ToolWeaponTypes twT)
        {
            eMG.CellsC.Selected = 0;

            eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();


            if (eMG.CurPlayerITC.Is(eMG.WhoseMovePlayerTC.PlayerT))
            {
                if (eMG.LessonTC.Is(LessonTypes.ClickPick))
                {
                    if (twT == ToolWeaponTypes.Pick)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }

                if (eMG.PlayerInfoE(eMG.WhoseMovePlayerTC.PlayerT).PawnInfoE.PawnsInGame > 0)
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
                        if (eMG.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            if (twT == ToolWeaponTypes.Shield || twT == ToolWeaponTypes.BowCrossbow)
                            {
                                if (eMG.SelectedE.ToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                            }
                            else if (twT != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                        }
                        else
                        {
                            levT = eMG.SelectedE.ToolWeaponC.LevelT;
                        }
                    }
                    else if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.Sword)
                    {
                        levT = LevelTypes.Second;
                    }

                    eMG.SelectedE.ToolWeaponC.ToolWeaponT = twT;
                    eMG.SelectedE.ToolWeaponC.LevelT = levT;


                    eMG.CellClickTC.CellClickT = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    eMG.MistakeTC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    eMG.MistakeTimerC.Timer = 0;
                    eMG.SoundAction(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                eMG.MistakeTC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMG.MistakeTimerC.Timer = 0;
                eMG.SoundAction(ClipTypes.WritePensil).Invoke();
            }


            eMG.NeedUpdateView = true;
        }
    }
}