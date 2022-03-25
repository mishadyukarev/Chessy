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
            eMGame.CellsC.Selected = 0;


            eMGame.Sound(ClipTypes.Click).Invoke();

            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                if (eMGame.PlayerInfoE(eMGame.WhoseMove.Player).LevelE(LevelTypes.First).UnitsInGame(UnitTypes.Pawn) > 0)
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
                        if (eMGame.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            if (twT == ToolWeaponTypes.Shield || twT == ToolWeaponTypes.BowCrossbow)
                            {
                                if (eMGame.SelectedE.ToolWeaponC.LevelT == LevelTypes.First) levT = LevelTypes.Second;
                            }
                            else if (twT != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                        }
                        else
                        {
                            levT = eMGame.SelectedE.ToolWeaponC.LevelT;
                        }
                    }
                    else if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.Sword)
                    {
                        levT = LevelTypes.Second;
                    }

                    eMGame.SelectedE.ToolWeaponC.ToolWeaponT = twT;
                    eMGame.SelectedE.ToolWeaponC.LevelT = levT;


                    eMGame.CellClickTC.Click = CellClickTypes.GiveTakeTW;
                }
                else
                {
                    eMGame.MistakeC.MistakeT = MistakeTypes.NeedPawnsInGame;
                    eMGame.MistakeC.Timer = 0;
                    eMGame.Sound(ClipTypes.WritePensil).Invoke();
                }
            }
            else
            {
                eMGame.MistakeC.MistakeT = MistakeTypes.NeedWaitQueue;
                eMGame.MistakeC.Timer = 0;
                eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();
            }

            //_updateUI.Invoke();
        }
    }
}