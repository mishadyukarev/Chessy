using static Game.Game.EntityPool;
using static Game.Game.EntityVPool;
using static Game.Game.CenterKingUIE;
using static Game.Game.DownToolWeaponUIEs;
using Game.Common;

namespace Game.Game
{
    sealed class DownEventUIS
    {
        internal DownEventUIS()
        {
            UIEntDownScout.Scout<ButtonUIC>().AddListener(ExecuteScout);
            DownHeroUIE.ButtonC.AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);

            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { BuyUnit(UnitTypes.Archer); });


            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { GetUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { GetUnit(UnitTypes.Archer); });

            UIEntDownUpgrade.Upgrade<ButtonUIC>().AddListener(ToggleUpgradeUnit);

            Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener( delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
        }

        void ExecuteScout()
        {
            Entities.SelectedIdxE.IdxC.Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (Entities.WhoseMoveE.IsMyMove)
            {
                if (!ScoutHeroCooldown(UnitTypes.Scout, Entities.WhoseMoveE.CurPlayerI).Have)
                {
                    if (Entities.WhoseMoveE.IsMyMove)
                    {
                        ClickerObject<CellClickC>().Click = CellClickTypes.GiveScout;
                    }
                }
                else
                {
                    SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }
        void Hero()
        {
            Entities.SelectedIdxE.IdxC.Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (Entities.WhoseMoveE.IsMyMove)
            {
                if (!ScoutHeroCooldown(UnitTypes.Elfemale, Entities.WhoseMoveE.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Click = CellClickTypes.GiveHero;
                }
                else
                {
                    SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }


        void Done()
        {
            if (!InventorUnitsE.Units(UnitTypes.King, LevelTypes.First, Entities.WhoseMoveE.CurPlayerI).Have)
            {
                EntityPool.Rpc.DoneToMaster();
            }
            else
            {
                SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
            }
        }

        void BuyUnit(in UnitTypes unit)
        {
            if (Entities.WhoseMoveE.IsMyMove)
            {
                GetterUnitsEs.GetterUnit<TimerC>(unit).Reset();

                EntityPool.Rpc.CreateUnitToMaster(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetUnit(UnitTypes unitT)
        {
            Entities.SelectedIdxE.IdxC.Reset();

            GetterUnitsEs.GetterUnit<TimerC>(unitT).Reset();

            if (Entities.WhoseMoveE.IsMyMove)
            {
                if (InventorUnitsE.Units(unitT, LevelTypes.Second, Entities.WhoseMoveE.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Click = CellClickTypes.SetUnit;

                    SelectedUnitE.SelUnit<UnitTC>().Unit = unitT;
                    SelectedUnitE.SelUnit<LevelTC>().Level = LevelTypes.Second;
                }

                else if (InventorUnitsE.Units(unitT, LevelTypes.First, Entities.WhoseMoveE.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Click = CellClickTypes.SetUnit;

                    SelectedUnitE.SelUnit<UnitTC>().Unit = unitT;
                    SelectedUnitE.SelUnit<LevelTC>().Level = LevelTypes.First;
                }

                else
                {
                    GetterUnitsEs.GetterUnit<IsActiveC>(unitT).IsActive = true;
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            Entities.SelectedIdxE.IdxC.Reset();

            ref var selToolWeaponC = ref SelectedToolWeaponE.SelectedTW<ToolWeaponC>();
            ref var selLevelTWC = ref SelectedToolWeaponE.SelectedTW<LevelTC>();

            if (Entities.WhoseMoveE.IsMyMove)
            {
                if (tw == ToolWeaponTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }


                if (ClickerObject<CellClickC>().Is(CellClickTypes.GiveTakeTW))
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
                    ClickerObject<CellClickC>().Click = CellClickTypes.GiveTakeTW;

                    selToolWeaponC.ToolWeapon = tw;

                    if (tw == ToolWeaponTypes.Shield)
                    {
                        if (selLevelTWC.Is(LevelTypes.First))
                            selLevelTWC.Level = LevelTypes.Second;

                        else selLevelTWC.Level = LevelTypes.First;
                    }
                    else selLevelTWC.Level = LevelTypes.Second;
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleUpgradeUnit()
        {
            Entities.SelectedIdxE.IdxC.Reset();

            if (Entities.WhoseMoveE.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                ClickerObject<CellClickC>().Click = CellClickTypes.UpgradeUnit;
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
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