﻿using static Game.Game.EntityPool;
using static Game.Game.EntityVPool;
using static Game.Game.EntityCenterKingUIPool;

namespace Game.Game
{
    sealed class DownEventUIS
    {
        internal DownEventUIS()
        {
            GetScoutUIC.AddListScout(ExecuteScout);
            GetHeroDownUIC.AddList(Hero);

            DonerUICom.AddListener(Done);

            GetPawnArcherUIC.AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            GetPawnArcherUIC.AddListenerToCreateUnit(UnitTypes.Archer, delegate { CreateUnit(UnitTypes.Archer); });


            Button<ButtonVC>().AddList(delegate { GetUnit(UnitTypes.King); });
            GetPawnArcherUIC.AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            GetPawnArcherUIC.AddListener(UnitTypes.Archer, delegate { GetUnit(UnitTypes.Archer); });

            UpgUnitUIC.AddList(ToggleUpgradeUnit);
            TwGiveTakeUIC.AddList_Button(TWTypes.Pick, delegate { ToggleToolWeapon(TWTypes.Pick); });
            TwGiveTakeUIC.AddList_Button(TWTypes.Sword, delegate { ToggleToolWeapon(TWTypes.Sword); });
            TwGiveTakeUIC.AddList_Button(TWTypes.Shield, delegate { ToggleToolWeapon(TWTypes.Shield); });
        }

        void ExecuteScout()
        {
            SelIdx<IdxC>().Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(UnitTypes.Scout, WhoseMoveC.CurPlayerI))
                {
                    if (WhoseMoveC.IsMyMove)
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

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(UnitTypes.Elfemale, WhoseMoveC.CurPlayerI))
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
            if (!InvUnitsC.Have(UnitTypes.King, LevelTypes.First, WhoseMoveC.CurPlayerI))
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
            if (WhoseMoveC.IsMyMove)
            {
                GetterUnitsC.ResetCurTimer(unitType);

                EntityPool.Rpc<RpcC>().CreateUnitToMaster(unitType);
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void GetUnit(UnitTypes unitType)
        {
            //CellClicker<CellClickC>().Set(CellClickTypes.FirstClick);
            SelIdx<IdxC>().Reset();

            GetterUnitsC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InvUnitsC.Have(unitType, LevelTypes.Second, WhoseMoveC.CurPlayerI))
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.SetUnit);
                    SelUnitC.SetSelUnit(unitType, LevelTypes.Second);
                }
                else if (InvUnitsC.Have(unitType, LevelTypes.First, WhoseMoveC.CurPlayerI))
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.SetUnit);
                    SelUnitC.SetSelUnit(unitType, LevelTypes.First);
                }
                else
                {
                    GetterUnitsC.ActiveNeedCreateButton(unitType, true);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleToolWeapon(TWTypes tWType)
        {
            SelIdx<IdxC>().Reset();

            if (WhoseMoveC.IsMyMove)
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
                    if (tWType == TWTypes.Shield)
                    {
                        if (TwGiveTakeC.TWTypeForGive == tWType)
                        {
                            if (TwGiveTakeC.Level(tWType) == LevelTypes.First) TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                            else TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                        }
                        else
                        {
                            TwGiveTakeC.Set(tWType);
                            TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                        }
                    }
                    else
                    {
                        TwGiveTakeC.Set(tWType);
                        TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                    }
                }
                else
                {
                    ClickerObject<CellClickC>().Set(CellClickTypes.GiveTakeTW);
                    TwGiveTakeC.Set(tWType);

                    if (tWType == TWTypes.Shield)
                    {
                        //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                        //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    }
                    else TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                }
            }
            else SoundV<AudioSourceVC>(ClipTypes.Mistake).Play();
        }

        void ToggleUpgradeUnit()
        {
            SelIdx<IdxC>().Reset();

            if (WhoseMoveC.IsMyMove)
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
                if (!HintC.WasActived(videoClip))
                {
                    //EntityCenterHintUIPool.SetActiveHintZone(true);
                    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                    HintC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}