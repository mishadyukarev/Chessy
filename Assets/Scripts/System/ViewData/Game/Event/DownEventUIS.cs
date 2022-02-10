using static Game.Game.DownToolWeaponUIEs;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class DownEventUIS : SystemViewAbstract
    {
        internal DownEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
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
                var whoseMove = Es.WhoseMoveE.CurPlayerI;

                if (Es.MaxAvailablePawnsE(whoseMove).CanGetPawn(Es.WhereWorker))
                {
                    Es.SelectedUnitE.SetSelectedUnit(UnitTypes.Pawn, LevelTypes.First, Es.ClickerObjectE);
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            Es.SelectedIdxE.Reset();

            ref var selToolWeaponC = ref Es.SelectedToolWeaponE.ToolWeaponTC;
            ref var selLevelTWC = ref Es.SelectedToolWeaponE.LevelTC;

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


                if (Es.ClickerObjectE.CellClickCRef.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tw == ToolWeaponTypes.Shield)
                    {
                        if (selToolWeaponC.ToolWeapon == tw)
                        {
                            if (selLevelTWC.Is(LevelTypes.First)) selLevelTWC.Level = LevelTypes.Second;
                            else selLevelTWC.Level = LevelTypes.First;
                        }
                        else
                        {
                            selToolWeaponC.ToolWeapon = tw;
                            selLevelTWC.Level = LevelTypes.First;
                        }
                    }
                    else
                    {
                        selToolWeaponC.ToolWeapon = tw;
                        selLevelTWC.Level = LevelTypes.Second;
                    }
                }
                else
                {
                    Es.ClickerObjectE.CellClickCRef.Click = CellClickTypes.GiveTakeTW;

                    selToolWeaponC.ToolWeapon = tw;

                    if (tw == ToolWeaponTypes.Shield)
                    {
                        if (selLevelTWC.Is(LevelTypes.First))
                            selLevelTWC.Level = LevelTypes.Second;

                        else selLevelTWC.Level = LevelTypes.First;
                    }
                    else if(tw == ToolWeaponTypes.BowCrossbow) selLevelTWC.Level = LevelTypes.First;
                    else selLevelTWC.Level = LevelTypes.Second;
                }
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