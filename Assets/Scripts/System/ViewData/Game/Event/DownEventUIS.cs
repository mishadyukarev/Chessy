﻿using static Game.Game.DownToolWeaponUIEs;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class DownEventUIS : SystemViewAbstract
    {
        internal DownEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            UIEntDownScout.Scout<ButtonUIC>().AddListener(ExecuteScout);
            DownHeroUIE.ButtonC.AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);

            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { BuyUnit(UnitTypes.Archer); });


            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { GetUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { GetUnit(UnitTypes.Archer); });

            UIEntDownUpgrade.Upgrade<ButtonUIC>().AddListener(ToggleUpgradeUnit);

            Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
        }

        void ExecuteScout()
        {
            Es.SelectedIdxE.IdxC.Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (Es.WhoseMove.IsMyMove)
            {
                if (!Es.ScoutHeroCooldownE(UnitTypes.Scout, Es.WhoseMove.CurPlayerI).Cooldown.Have)
                {
                    if (Es.WhoseMove.IsMyMove)
                    {
                        Es.ClickerObject.CellClickC.Click = CellClickTypes.GiveScout;
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
            Es.SelectedIdxE.IdxC.Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (Es.WhoseMove.IsMyMove)
            {
                if (!Es.ScoutHeroCooldownE(UnitTypes.Elfemale, Es.WhoseMove.CurPlayerI).Cooldown.Have)
                {
                    Es.ClickerObject.CellClickC.Click = CellClickTypes.GiveHero;
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
            if (!Es.InventorUnitsEs.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMove.CurPlayerI).Units.Have)
            {
                Es.Rpc.DoneToMaster();
            }
            else
            {
                SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
            }
        }

        void BuyUnit(in UnitTypes unit)
        {
            if (Es.WhoseMove.IsMyMove)
            {
                GetterUnitsEs.GetterUnit<TimerC>(unit).Reset();

                Es.Rpc.CreateUnitToMaster(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetUnit(UnitTypes unitT)
        {
            Es.SelectedIdxE.IdxC.Reset();

            GetterUnitsEs.GetterUnit<TimerC>(unitT).Reset();

            if (Es.WhoseMove.IsMyMove)
            {
                if (Es.InventorUnitsEs.Units(unitT, LevelTypes.Second, Es.WhoseMove.CurPlayerI).Units.Have)
                {
                    Es.ClickerObject.CellClickC.Click = CellClickTypes.SetUnit;

                    Es.SelectedUnitE.UnitTC.Unit = unitT;
                    Es.SelectedUnitE.LevelTC.Level = LevelTypes.Second;
                }

                else if (Es.InventorUnitsEs.Units(unitT, LevelTypes.First, Es.WhoseMove.CurPlayerI).Units.Have)
                {
                    Es.ClickerObject.CellClickC.Click = CellClickTypes.SetUnit;

                    Es.SelectedUnitE.UnitTC.Unit = unitT;
                    Es.SelectedUnitE.LevelTC.Level = LevelTypes.First;
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
            Es.SelectedIdxE.IdxC.Reset();

            ref var selToolWeaponC = ref Es.SelectedToolWeaponE.ToolWeaponTC;
            ref var selLevelTWC = ref Es.SelectedToolWeaponE.LevelTC;

            if (Es.WhoseMove.IsMyMove)
            {
                if (tw == ToolWeaponTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }


                if (Es.ClickerObject.CellClickC.Is(CellClickTypes.GiveTakeTW))
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
                    Es.ClickerObject.CellClickC.Click = CellClickTypes.GiveTakeTW;

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
            Es.SelectedIdxE.IdxC.Reset();

            if (Es.WhoseMove.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                Es.ClickerObject.CellClickC.Click = CellClickTypes.UpgradeUnit;
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