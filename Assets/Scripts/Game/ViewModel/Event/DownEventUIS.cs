﻿using System;

namespace Chessy.Game
{
    sealed class DownEventUIS : SystemUIAbstract
    {
        readonly Action _updateUI;

        internal DownEventUIS(in Action updateUI, in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            _updateUI = updateUI;

            UIE.DownEs.DonerE.ButtonC.AddListener(Done);

            //UIE.DownEs.ScoutE.ButtonC.AddListener(ExecuteScout);
            UIE.DownEs.HeroE.ButtonC.AddListener(Hero);


            UIE.DownEs.PawnE.ButtonUIC.AddListener(delegate { GetPawn(); });

            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.BowCrossbow).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Axe).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
            UIE.DownEs.ToolWeaponE.ButtonC(ToolWeaponTypes.Staff).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Staff); });
        }

        //void ExecuteScout()
        //{
        //    E.SelectedIdxC.Idx = 0;

        //    TryOnHint(VideoClipTypes.CreatingScout);

        //    if (E.CurPlayerITC.Is(E.WhoseMove.Player))
        //    {
        //        if (!E.UnitInfoE(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.Scout).HeroCooldownC.HaveCooldown)
        //        {
        //            E.SelectedUnitE.Set(UnitTypes.Scout, LevelTypes.First);
        //            E.CellClickTC.Click = CellClickTypes.SetUnit;
        //        }
        //        else
        //        {
        //            E.Sound(ClipTypes.Mistake).Action.Invoke();
        //        }
        //    }
        //    else E.Sound(ClipTypes.Mistake).Action.Invoke();
        //}
        void Hero()
        {
            E.SelectedIdxC.Idx = 0;
            TryOnHint(VideoClipTypes.CreatingHero);

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                var curPlayer = E.CurPlayerITC.Player;

                var myHeroT = E.PlayerE(curPlayer).AvailableHeroTC.Unit;

                if (E.UnitInfoE(curPlayer, LevelTypes.First, myHeroT).HaveInInventor)
                {
                    if (!E.UnitInfoE(E.CurPlayerITC.Player, LevelTypes.First, myHeroT).HeroCooldownC.HaveCooldown)
                    {
                        E.SelectedUnitE.Set(myHeroT, LevelTypes.First);
                        E.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        E.Sound(ClipTypes.Mistake).Action.Invoke();
                    }
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }
        void Done()
        {
            if (!E.UnitInfoE(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
            {
                E.RpcPoolEs.DoneToMaster();
            }
            else
            {
                E.Sound(ClipTypes.Mistake).Action.Invoke();
            }
        }
        void GetPawn()
        {
            E.SelectedIdxC.Idx = 0;

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                var curPlayerI = E.CurPlayerITC.Player;

                if (E.PlayerE(curPlayerI).PeopleInCity >= 1)
                {
                    var pawnsInGame = E.UnitInfoE(curPlayerI, LevelTypes.First, UnitTypes.Pawn).UnitsInGame
                        + E.UnitInfoE(curPlayerI, LevelTypes.Second, UnitTypes.Pawn).UnitsInGame;

                    if (pawnsInGame < E.PlayerE(curPlayerI).MaxAvailablePawns)
                    {
                        E.SelectedUnitE.Set(UnitTypes.Pawn, LevelTypes.First);
                        E.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
                else
                {
                    E.MistakeE.Set(MistakeTypes.NeedMorePeopleInCity, 0);
                    E.Sound(ClipTypes.Mistake).Action.Invoke();
                }


            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }
        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            E.SelectedIdxC.Idx = 0;

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                if (tw == ToolWeaponTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }


                var levT = LevelTypes.First;

                if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                {
                    if (E.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                    {
                        if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                        {
                            if (E.SelectedTWE.LevelTC.Is(LevelTypes.First)) levT = LevelTypes.Second;
                        }
                        else if (tw != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                    }
                    else
                    {
                        levT = E.SelectedTWE.LevelTC.Level;
                    }
                }
                else if (tw == ToolWeaponTypes.Axe || tw == ToolWeaponTypes.Sword)
                {
                    levT = LevelTypes.Second;
                }

                E.SelectedTWE.ToolWeaponTC.ToolWeapon = tw;
                E.SelectedTWE.LevelTC.Level = levT;




                E.CellClickTC.Click = CellClickTypes.GiveTakeTW;
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();

            _updateUI.Invoke();
        }
        void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                //if (!HintC.WasActived(videoClip))
                //{
                //    //EntityCenterHintUIPool.SetActiveHintZone(true);
                //    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                //    HintC.SetWasActived(videoClip, true);
                //}
            }
        }
    }
}