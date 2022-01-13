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

            UIEntDownPawnArcher.Create<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { CreateUnit(UnitTypes.Pawn); });
            UIEntDownPawnArcher.Create<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { CreateUnit(UnitTypes.Archer); });


            Button<ButtonUIC>().AddListener(delegate { GetUnit(UnitTypes.King); });
            UIEntDownPawnArcher.Taker<ButtonUIC>(UnitTypes.Pawn).AddListener(delegate { GetUnit(UnitTypes.Pawn); });
            UIEntDownPawnArcher.Taker<ButtonUIC>(UnitTypes.Archer).AddListener(delegate { GetUnit(UnitTypes.Archer); });

            UIEntDownUpgrade.Upgrade<ButtonUIC>().AddListener(ToggleUpgradeUnit);

            Button<ButtonUIC>(TWTypes.Pick).AddListener( delegate { ToggleToolWeapon(TWTypes.Pick); });
            Button<ButtonUIC>(TWTypes.Sword).AddListener(delegate { ToggleToolWeapon(TWTypes.Sword); });
            Button<ButtonUIC>(TWTypes.Shield).AddListener(delegate { ToggleToolWeapon(TWTypes.Shield); });
        }

        void ExecuteScout()
        {
            SelIdx<IdxC>().Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (EntWhoseMove.IsMyMove)
            {
                if (!ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, EntWhoseMove.CurPlayerI).HaveCooldown)
                {
                    if (EntWhoseMove.IsMyMove)
                    {
                        ClickerObject<CellClickC>().Set(CellClickTypes.GiveScout);
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

            if (EntWhoseMove.IsMyMove)
            {
                if (!ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, EntWhoseMove.CurPlayerI).HaveCooldown)
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.GiveHero);
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
            if (!EntInventorUnits.Units<AmountC>(UnitTypes.King, LevelTypes.First, EntWhoseMove.CurPlayerI).Have)
            {
                EntityPool.Rpc<RpcC>().DoneToMaster();
            }
            else
            {
                SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
            }
        }

        void CreateUnit(UnitTypes unitType)
        {
            if (EntWhoseMove.IsMyMove)
            {
                GetterUnitsC.GetterUnit<TimerC>(unitType).Reset();

                EntityPool.Rpc<RpcC>().CreateUnitToMaster(unitType);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetUnit(UnitTypes unitType)
        {
            //CellClicker<CellClickC>().Set(CellClickTypes.FirstClick);
            SelIdx<IdxC>().Reset();

            GetterUnitsC.GetterUnit<TimerC>(unitType).Reset();

            if (EntWhoseMove.IsMyMove)
            {
                if (EntInventorUnits.Units<AmountC>(unitType, LevelTypes.Second, EntWhoseMove.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.SetUnit);
                    SelectedUnitEnt.SetSelUnit(unitType, LevelTypes.Second);
                }
                else if (EntInventorUnits.Units<AmountC>(unitType, LevelTypes.First, EntWhoseMove.CurPlayerI).Have)
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.SetUnit);
                    SelectedUnitEnt.SetSelUnit(unitType, LevelTypes.First);
                }
                else
                {
                    GetterUnitsC.GetterUnit<IsActivatedC>(unitType).IsActivated = true;
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(TWTypes tWType)
        {
            SelIdx<IdxC>().Reset();

            if (EntWhoseMove.IsMyMove)
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

            if (EntWhoseMove.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                ClickerObject<CellClickC>().Set(CellClickTypes.UpgradeUnit);
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