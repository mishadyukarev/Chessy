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
            Es.SelectedIdxC.Idx = 0;

            TryOnHint(VideoClipTypes.CreatingScout);

            if (Es.IsMyMove)
            {
                if (!Es.PlayerE(Es.CurPlayerI.Player).UnitsInfoE(UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
                {
                    //Es.SelUnitTC.SetSelectedUnit((UnitTypes.Scout, LevelTypes.First), Es.SelUnitLevelTC, ref Es.CellClickTC);

                    //Unit = unit.Item1;
                    //levTC.Level = unit.Item2;
                    //clickC.Click = CellClickTypes.SetUnit;
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
            Es.SelectedIdxC.Idx = 0;
            TryOnHint(VideoClipTypes.CreatingHero);

            if (Es.IsMyMove)
            {
                var curPlayer = Es.CurPlayerI.Player;

                var myHeroT = Es.PlayerE(curPlayer).AvailableHeroTC.Unit;

                if (Es.PlayerE(curPlayer).UnitsInfoE(myHeroT).HaveInInventor)
                {
                    if (!Es.PlayerE(Es.CurPlayerI.Player).UnitsInfoE(UnitTypes.Scout).ScoutHeroCooldownC.HaveCooldown)
                    {
                        Es.SelectedUnitE.UnitTC.Unit = myHeroT;
                        Es.SelectedUnitE.LevelTC.Level = LevelTypes.First;
                        Es.CellClickTC.Click = CellClickTypes.SetUnit;
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
            if (!Es.PlayerE(Es.CurPlayerI.Player).UnitsInfoE(UnitTypes.King).HaveInInventor)
            {
                Es.RpcE.DoneToMaster();
            }
            else
            {
                SoundV(ClipTypes.Mistake).Play();
            }
        }
        void GetPawn()
        {
            Es.SelectedIdxC.Idx = 0;

            if (Es.IsMyMove)
            {
                var curPlayerI = Es.CurPlayerI.Player;

                if (Es.PlayerE(curPlayerI).PeopleInCity > 0)
                {
                    if (Es.PlayerE(curPlayerI).UnitsInfoE(UnitTypes.Pawn).UnitsInGame < Es.PlayerE(curPlayerI).MaxAvailablePawns)
                    {
                        Es.SelectedUnitE.UnitTC.Unit = UnitTypes.Pawn;
                        Es.SelectedUnitE.LevelTC.Level = LevelTypes.First;
                        Es.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
                else
                {
                    Es.MistakeE.MistakeTC.Mistake = MistakeTypes.NeedMorePeopleInCity;
                    Es.MistakeE.TimerC.Timer = 0;
                    Es.Sound(ClipTypes.Mistake).Action.Invoke();
                }


            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            Es.SelectedIdxC.Idx = 0;

            if (Es.IsMyMove)
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
                    if (Es.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                    {
                        if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                        {
                            if (Es.SelectedTWE.LevelTC.Is(LevelTypes.First)) levT = LevelTypes.Second;
                        }
                        else if (tw != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                    }   
                    else
                    {
                        levT = Es.SelectedTWE.LevelTC.Level;
                    }
                }
                else if (tw == ToolWeaponTypes.Axe || tw == ToolWeaponTypes.Sword)
                {
                    levT = LevelTypes.Second;
                }

                Es.SelectedTWE.ToolWeaponTC.ToolWeapon = tw;
                Es.SelectedTWE.LevelTC.Level = levT;




                Es.CellClickTC.Click = CellClickTypes.GiveTakeTW;
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