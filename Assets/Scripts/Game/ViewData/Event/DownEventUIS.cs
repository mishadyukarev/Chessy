﻿using System;
using static Chessy.Game.DownToolWeaponUIEs;
using static Chessy.Game.EntityVPool;

namespace Chessy.Game
{
    sealed class DownEventUIS : SystemUIAbstract
    {
        readonly Action _updateUI;

        internal DownEventUIS(in Action updateUI, in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _updateUI = updateUI;

            UIEs.DownEs.DonerE.ButtonC.AddListener(Done);

            UIEs.DownEs.ScoutE.ButtonC.AddListener(ExecuteScout);
            UIEs.DownEs.HeroE.ButtonC.AddListener(Hero);


            UIEs.DownEs.PawnE.ButtonUIC.AddListener(delegate { GetPawn(); });

            //Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            //Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            //Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
            //Button<ButtonUIC>(ToolWeaponTypes.BowCrossbow).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            //Button<ButtonUIC>(ToolWeaponTypes.Axe).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
        }

        void ExecuteScout()
        {
            E.SelectedIdxC.Idx = 0;

            TryOnHint(VideoClipTypes.CreatingScout);

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                if (!E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
                {
                    E.SelectedUnitE.Set(UnitTypes.Scout, LevelTypes.First);
                    E.CellClickTC.Click = CellClickTypes.SetUnit;
                }
                else
                {
                    E.Sound(ClipTypes.Mistake).Action.Invoke();
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }
        void Hero()
        {
            E.SelectedIdxC.Idx = 0;
            TryOnHint(VideoClipTypes.CreatingHero);

            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                var curPlayer = E.CurPlayerITC.Player;

                var myHeroT = E.PlayerE(curPlayer).AvailableHeroTC.Unit;

                if (E.UnitInfo(curPlayer, LevelTypes.First, myHeroT).HaveInInventor)
                {
                    if (!E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
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
            if (!E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
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

                if (E.PlayerE(curPlayerI).PeopleInCity > 0)
                {
                    var pawnsInGame = E.UnitInfo(curPlayerI, LevelTypes.First, UnitTypes.Pawn).UnitsInGame
                        + E.UnitInfo(curPlayerI, LevelTypes.Second, UnitTypes.Pawn).UnitsInGame;

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