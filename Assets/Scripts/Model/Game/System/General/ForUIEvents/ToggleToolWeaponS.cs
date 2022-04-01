using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;

namespace Chessy.Game.Model.System
{
    public sealed class ToggleToolWeaponS : SystemModelGameAbs
    {
        public ToggleToolWeaponS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        public void Click(in ToolWeaponTypes twT)
        {
            eMG.CellsC.Selected = 0;

            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();


            if (eMG.CurPlayerITC.Is(eMG.WhoseMove.PlayerT))
            {
                if (eMG.LessonTC.Is(LessonTypes.ClickPick))
                {
                    if (twT == ToolWeaponTypes.Pick)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }

                if (eMG.PlayerInfoE(eMG.WhoseMove.PlayerT).LevelE(LevelTypes.First).UnitsInGame(UnitTypes.Pawn) > 0)
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
                    eMG.MistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    eMG.MistakeC.Timer = 0;
                    eMG.SoundActionC(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                eMG.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMG.MistakeC.Timer = 0;
                eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
            }


            eMG.NeedUpdateView = true;
        }
    }
}