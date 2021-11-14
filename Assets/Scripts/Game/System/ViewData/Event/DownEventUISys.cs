using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class DownEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            ScoutUIC.AddListScout(ExecuteScout);
            HeroDownUIC.AddList(Hero);

            DonerUICom.AddListener(Done);

            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Archer, delegate { CreateUnit(UnitTypes.Archer); });

            KingZoneViewUIC.AddListenerToSetKing_Button(delegate { GetUnit(UnitTypes.King); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Archer, delegate { GetUnit(UnitTypes.Archer); });

            GiveTakeViewUIC.AddListUpgradeButton(ToggleUpgradeUnit);
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Pick, delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Sword, delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Shield, delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
        }

        private void ExecuteScout()
        {
            TryOnHint(VideoClipTypes.CreatingScout);

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(WhoseMoveC.CurPlayerI, UnitTypes.Scout))
                {
                    if (WhoseMoveC.IsMyMove)
                    {
                        CellClickC.Set(CellClickTypes.GiveScout);
                    }
                }
                else
                {
                    SoundEffectC.Play(ClipGameTypes.Mistake);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }
        private void Hero()
        {
            TryOnHint(VideoClipTypes.CreatingHero);

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(WhoseMoveC.CurPlayerI, InvUnitsC.MyHero))
                {
                    CellClickC.Set(CellClickTypes.GiveHero);
                    IdxSel.Idx = default;
                }
                else
                {
                    SoundEffectC.Play(ClipGameTypes.Mistake);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }


        private void Done()
        {
            if (!InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelUnitTypes.First))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectC.Play(ClipGameTypes.Mistake);
            }

            CellClickC.Reset();
            SelUnitC.Reset();
        }

        private void CreateUnit(UnitTypes unitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                GetterUnitsC.ResetCurTimer(unitType);

                RpcSys.CreateUnitToMaster(unitType);
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void GetUnit(UnitTypes unitType)
        {
            CellClickC.Reset();
            IdxCur.Idx = default;
            IdxPreVis.Idx = default;
            IdxSel.Reset();
            GetterUnitsC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InvUnitsC.Have(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.Second))
                {
                    SelUnitC.SetSelUnit(unitType, LevelUnitTypes.Second);
                }
                else if (InvUnitsC.Have(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.First))
                {
                    SelUnitC.SetSelUnit(unitType, LevelUnitTypes.First);
                }
                else
                {
                    GetterUnitsC.ActiveNeedCreateButton(unitType, true);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void ToggleToolWeapon(ToolWeaponTypes tWType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                if (tWType == ToolWeaponTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }

                if (CellClickC.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        if (TwGiveTakeC.TWTypeForGive == tWType)
                        {
                            if (TwGiveTakeC.Level(tWType) == LevelTWTypes.Wood) TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Iron);
                            else TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Wood);
                        }
                        else
                        {
                            TwGiveTakeC.TWTypeForGive = tWType;
                            TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Wood);
                        }
                    }
                    else
                    {
                        TwGiveTakeC.TWTypeForGive = tWType;
                        TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Iron);
                    }
                }
                else
                {
                    CellClickC.Set(CellClickTypes.GiveTakeTW);
                    TwGiveTakeC.TWTypeForGive = tWType;

                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                        //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    }
                    else TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Iron);
                }
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }

        private void ToggleUpgradeUnit()
        {
            if (WhoseMoveC.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                CellClickC.Set(CellClickTypes.UpgradeUnit);
            }
            else SoundEffectC.Play(ClipGameTypes.Mistake);
        }


        private void TryOnHint(VideoClipTypes videoClip)
        {
            if (HintComC.IsOnHint)
            {
                if (!HintC.WasActived(videoClip))
                {
                    HintViewUIC.SetActiveHintZone(true);
                    HintViewUIC.SetVideoClip(videoClip);
                    HintC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}