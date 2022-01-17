using static Game.Game.EntityPool;
using static Game.Game.EntityVPool;
using static Game.Game.CenterKingUIE;
using static Game.Game.UIEntDownToolWeapon;
using Game.Common;

namespace Game.Game
{
    sealed class DownEventUIS
    {
        internal DownEventUIS()
        {
            UIEntDownScout.Scout<ButtonUIC>().AddListener(ExecuteScout);
            UIEntDownHero.Scout<ButtonUIC>().AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);

            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { BuyUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.BuyUnit<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { BuyUnit(UnitTypes.Archer); });


            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { GetUnit(UnitTypes.Pawn); });
            PawnArcherDownUIE.Taker<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { GetUnit(UnitTypes.Archer); });

            UIEntDownUpgrade.Upgrade<ButtonUIC>().AddListener(ToggleUpgradeUnit);

            Button<ButtonUIC>(TWTypes.Pick).AddListener( delegate { ToggleToolWeapon(TWTypes.Pick); });
            Button<ButtonUIC>(TWTypes.Sword).AddListener(delegate { ToggleToolWeapon(TWTypes.Sword); });
            Button<ButtonUIC>(TWTypes.Shield).AddListener(delegate { ToggleToolWeapon(TWTypes.Shield); });
        }

        void ExecuteScout()
        {
            SelIdx<IdxC>().Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (WhoseMoveE.IsMyMove)
            {
                if (!ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, WhoseMoveE.CurPlayerI).HaveCooldown)
                {
                    if (WhoseMoveE.IsMyMove)
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
            SelIdx<IdxC>().Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (WhoseMoveE.IsMyMove)
            {
                if (!ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, WhoseMoveE.CurPlayerI).HaveCooldown)
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
            if (!EntInventorUnits.Units<AmountC>(UnitTypes.King, LevelTypes.First, WhoseMoveE.CurPlayerI).Have)
            {
                EntityPool.Rpc<RpcC>().DoneToMaster();
            }
            else
            {
                SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
            }
        }

        void BuyUnit(in UnitTypes unit)
        {
            if (WhoseMoveE.IsMyMove)
            {
                GetterUnitsE.GetterUnit<TimerC>(unit).Reset();

                EntityPool.Rpc<RpcC>().CreateUnitToMaster(unit);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetUnit(UnitTypes unitT)
        {
            SelIdx<IdxC>().Reset();

            GetterUnitsE.GetterUnit<TimerC>(unitT).Reset();

            if (WhoseMoveE.IsMyMove)
            {
                if (EntInventorUnits.Units<AmountC>(unitT, LevelTypes.Second, WhoseMoveE.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Click = CellClickTypes.SetUnit;

                    SelectedUnitE.SelUnit<UnitTC>().Unit = unitT;
                    SelectedUnitE.SelUnit<LevelTC>().Level = LevelTypes.Second;
                }

                else if (EntInventorUnits.Units<AmountC>(unitT, LevelTypes.First, WhoseMoveE.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Click = CellClickTypes.SetUnit;

                    SelectedUnitE.SelUnit<UnitTC>().Unit = unitT;
                    SelectedUnitE.SelUnit<LevelTC>().Level = LevelTypes.First;
                }

                else
                {
                    GetterUnitsE.GetterUnit<IsActiveC>(unitT).IsActive = true;
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(TWTypes tWType)
        {
            SelIdx<IdxC>().Reset();

            if (WhoseMoveE.IsMyMove)
            {
                if (tWType == TWTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }

                if (ClickerObject<CellClickC>().Is(CellClickTypes.GiveTakeTW))
                {
                    //if (tWType == TWTypes.Shield)
                    //{
                    //    if (TwGiveTakeC.TWTypeForGive == tWType)
                    //    {
                    //        if (TwGiveTakeC.Level(tWType) == LevelTypes.First) TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                    //        else TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                    //    }
                    //    else
                    //    {
                    //        TwGiveTakeC.Set(tWType);
                    //        TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                    //    }
                    //}
                    //else
                    //{
                    //    TwGiveTakeC.Set(tWType);
                    //    TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                    //}
                }
                else
                {
                    //ClickerObject<CellClickC>().Set(CellClickTypes.GiveTakeTW);
                    //TwGiveTakeC.Set(tWType);

                    //if (tWType == TWTypes.Shield)
                    //{
                    //    //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                    //    //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    //}
                    //else TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleUpgradeUnit()
        {
            SelIdx<IdxC>().Reset();

            if (WhoseMoveE.IsMyMove)
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