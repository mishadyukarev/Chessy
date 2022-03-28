using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class ToggleToolWeaponS : SystemModelGameAbs
    {
        public ToggleToolWeaponS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click(in ToolWeaponTypes twT)
        {
            e.CellsC.Selected = 0;

            e.Sound(ClipTypes.Click).Invoke();

            if (e.LessonTC.LessonT == Enum.LessonTypes.None)
            {
                if (e.CurPlayerITC.Is(e.WhoseMove.Player))
                {
                    if (e.PlayerInfoE(e.WhoseMove.Player).LevelE(LevelTypes.First).UnitsInGame(UnitTypes.Pawn) > 0)
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
                            if (e.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                            {
                                if (twT == ToolWeaponTypes.Shield || twT == ToolWeaponTypes.BowCrossbow)
                                {
                                    if (e.SelectedE.ToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                                }
                                else if (twT != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                            }
                            else
                            {
                                levT = e.SelectedE.ToolWeaponC.LevelT;
                            }
                        }
                        else if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.Sword)
                        {
                            levT = LevelTypes.Second;
                        }

                        e.SelectedE.ToolWeaponC.ToolWeaponT = twT;
                        e.SelectedE.ToolWeaponC.LevelT = levT;


                        e.CellClickTC.Click = CellClickTypes.GiveTakeTW;
                    }
                    else
                    {
                        e.MistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                        e.MistakeC.Timer = 0;
                        e.Sound(ClipTypes.WritePensil).Invoke();
                    }
                }
                else
                {
                    e.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                    e.MistakeC.Timer = 0;
                    e.Sound(ClipTypes.WritePensil).Action.Invoke();
                }
            }

            e.NeedUpdateView = true;
        }
    }
}