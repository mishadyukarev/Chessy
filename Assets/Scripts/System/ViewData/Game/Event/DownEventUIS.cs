using System;
using static Game.Game.DownToolWeaponUIEs;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class DownEventUIS : SystemUIAbstract
    {
        readonly Action _updateUI;

        internal DownEventUIS(in Action updateUI, in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _updateUI = updateUI;

            DownScoutUIEs.Scout<ButtonUIC>().AddListener(ExecuteScout);
            DownHeroUIE.ButtonC.AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);


            DownPawnUIE.ButtonUIC.AddListener(delegate { GetPawn(); });

            Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
            Button<ButtonUIC>(ToolWeaponTypes.BowCrossbow).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            Button<ButtonUIC>(ToolWeaponTypes.Axe).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
        }

        void ExecuteScout()
        {
            E.SelectedIdxC.Idx = 0;

            TryOnHint(VideoClipTypes.CreatingScout);

            if (E.IsMyMove)
            {
                if (!E.PlayerE(E.CurPlayerI.Player).UnitsInfoE(UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
                {
                    E.SelectedUnitE.Set(UnitTypes.Scout, LevelTypes.First);
                    E.CellClickTC.Click = CellClickTypes.SetUnit;
                }
                else
                {
                    SoundV(ClipTypes.Mistake).Play();
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void Hero()
        {
            E.SelectedIdxC.Idx = 0;
            TryOnHint(VideoClipTypes.CreatingHero);

            if (E.IsMyMove)
            {
                var curPlayer = E.CurPlayerI.Player;

                var myHeroT = E.PlayerE(curPlayer).AvailableHeroTC.Unit;

                if (E.PlayerE(curPlayer).UnitsInfoE(myHeroT).HaveInInventor)
                {
                    if (!E.PlayerE(E.CurPlayerI.Player).UnitsInfoE(UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
                    {
                        E.SelectedUnitE.UnitTC.Unit = myHeroT;
                        E.SelectedUnitE.LevelTC.Level = LevelTypes.First;
                        E.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        SoundV(ClipTypes.Mistake).Play();
                    }
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void Done()
        {
            if (!E.PlayerE(E.CurPlayerI.Player).UnitsInfoE(UnitTypes.King).HaveInInventor)
            {
                E.RpcE.DoneToMaster();
            }
            else
            {
                SoundV(ClipTypes.Mistake).Play();
            }
        }
        void GetPawn()
        {
            E.SelectedIdxC.Idx = 0;

            if (E.IsMyMove)
            {
                var curPlayerI = E.CurPlayerI.Player;

                if (E.PlayerE(curPlayerI).PeopleInCity > 0)
                {
                    if (E.PlayerE(curPlayerI).UnitsInfoE(UnitTypes.Pawn).UnitsInGame < E.PlayerE(curPlayerI).MaxAvailablePawns)
                    {
                        E.SelectedUnitE.UnitTC.Unit = UnitTypes.Pawn;
                        E.SelectedUnitE.LevelTC.Level = LevelTypes.First;
                        E.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
                else
                {
                    E.MistakeE.MistakeTC.Mistake = MistakeTypes.NeedMorePeopleInCity;
                    E.MistakeE.TimerC.Timer = 0;
                    E.Sound(ClipTypes.Mistake).Action.Invoke();
                }


            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            E.SelectedIdxC.Idx = 0;

            if (E.IsMyMove)
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
            else SoundV(ClipTypes.Mistake).Play();

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