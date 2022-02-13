using static Game.Game.DownToolWeaponUIEs;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class DownEventUIS : SystemUIAbstract
    {
        internal DownEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            DownScoutUIEs.Scout<ButtonUIC>().AddListener(ExecuteScout);
            DownHeroUIE.ButtonC.AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);


            DownPawnUIE.ButtonUIC.AddListener(delegate { GetPawn(); });

            DownUpgradeUIE.ButtonUIC.AddListener(ToggleUpgradeUnit);

            Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
            Button<ButtonUIC>(ToolWeaponTypes.BowCrossbow).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            Button<ButtonUIC>(ToolWeaponTypes.Axe).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
        }

        void ExecuteScout()
        {
            Es.SelectedIdxE.Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (Es.WhoseMoveE.IsMyMove)
            {
                if (!Es.ScoutHeroCooldownE(UnitTypes.Scout, Es.WhoseMoveE.CurPlayerI).HaveCooldown)
                {
                    if (Es.WhoseMoveE.IsMyMove)
                    {
                        Es.SelectedUnitE.SetSelectedUnit(UnitTypes.Scout, LevelTypes.First, Es.ClickerObjectE);
                    }
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
            Es.SelectedIdxE.Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (Es.WhoseMoveE.IsMyMove)
            {
                if (Es.InventorUnitsEs.HaveHero(Es.WhoseMoveE.CurPlayerI, out var myHero))
                {
                    if (!Es.ScoutHeroCooldownE(myHero, Es.WhoseMoveE.CurPlayerI).HaveCooldown)
                    {
                        Es.SelectedUnitE.SetSelectedUnit(myHero, LevelTypes.First, Es.ClickerObjectE);
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
            if (!Es.InventorUnitsEs.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMoveE.CurPlayerI).HaveUnits)
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
            Es.SelectedIdxE.Reset();

            if (Es.WhoseMoveE.IsMyMove)
            {
                var curPlayerI = Es.WhoseMoveE.CurPlayerI;

                if (Es.PeopleInCityE(curPlayerI).CanGetPeople && Es.MaxAvailablePawnsE(curPlayerI).CanGetPawn(Es.WhereWorker))
                {
                    Es.SelectedUnitE.SetSelectedUnit(UnitTypes.Pawn, LevelTypes.First, Es.ClickerObjectE);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            Es.SelectedIdxE.Reset();

            if (Es.WhoseMoveE.IsMyMove)
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

                if (Es.ClickerObjectE.CellClickCRef.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                    {
                        if (Es.SelectedToolWeaponE.Is(LevelTypes.First)) levT = LevelTypes.Second;
                    }
                    else if (tw != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;

                    Es.SelectedToolWeaponE.Set(tw, levT);
                }
                else
                {
                    Es.SelectedToolWeaponE.Set(tw, Es.SelectedToolWeaponE.Level);
                }

                Es.ClickerObjectE.CellClickCRef.Click = CellClickTypes.GiveTakeTW;

                //if (Es.ClickerObjectE.CellClickCRef.Is(CellClickTypes.GiveTakeTW))
                //{
                //    if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                //    {
                //        if (selToolWeaponC.ToolWeapon == tw)
                //        {
                //            if (selLevelTWC.Is(LevelTypes.First))
                //            {
                //                selLevelTWC.Level = LevelTypes.Second;
                //            }
                //            else selLevelTWC.Level = LevelTypes.First;
                //        }
                //        else
                //        {
                //            selToolWeaponC.ToolWeapon = tw;
                //            selLevelTWC.Level = LevelTypes.First;
                //        }
                //    }
                //    else
                //    {
                //        selToolWeaponC.ToolWeapon = tw;
                //        selLevelTWC.Level = LevelTypes.Second;
                //    }
                //}
                //else
                //{

                //}
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void ToggleUpgradeUnit()
        {
            Es.SelectedIdxE.Reset();

            if (Es.WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                Es.ClickerObjectE.CellClickCRef.Click = CellClickTypes.UpgradeUnit;
            }
            else SoundV(ClipTypes.Mistake).Play();
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