using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class DownEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            ScoutViewUIC.AddListScout(ExecuteScout);

            DonerUICom.AddListener(Done);

            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Rook, delegate { CreateUnit(UnitTypes.Rook); });
            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Bishop, delegate { CreateUnit(UnitTypes.Bishop); });

            KingZoneViewUIC.AddListenerToSetKing_Button(delegate { GetUnit(UnitTypes.King); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Rook, delegate { GetUnit(UnitTypes.Rook); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Bishop, delegate { GetUnit(UnitTypes.Bishop); });

            GiveTakeViewUIC.AddListUpgradeButton(ToggleUpgradeUnit);
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Pick, delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Sword, delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Shield, delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });

            HeroDownUIC.AddList(Hero);
        }

        private void ExecuteScout()
        {
            if (WhoseMoveC.IsMyMove)
            {
                SelectorC.Set(CellClickTypes.OldNewUnit);
                OldNewC.Set(UnitTypes.Scout);
            }
        }

        private void Done()
        {
            if (!InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelUnitTypes.Wood))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectC.Play(ClipGameTypes.Mistake);
            }

            SelectorC.Reset();
            SelUnitC.ResetSelUnit();
        }

        private void CreateUnit(UnitTypes unitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                GetterUnitsDataUIC.ResetCurTimer(unitType);

                RpcSys.CreateUnitToMaster(unitType);
            }
        }

        private void GetUnit(UnitTypes unitType)
        {
            SelectorC.Reset();
            SelectorC.IdxCurCell = default;
            SelectorC.IdxPreVisionCell = default;
            SelectorC.DefSelectedCell();
            GetterUnitsDataUIC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.Iron))
                {
                    SelUnitC.SelUnitType = unitType;
                    SelUnitC.LevelSelUnitType = LevelUnitTypes.Iron;
                }
                else if (InvUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.Wood))
                {
                    SelUnitC.SelUnitType = unitType;
                    SelUnitC.LevelSelUnitType = LevelUnitTypes.Wood;
                }
                else
                {
                    GetterUnitsDataUIC.ActiveNeedCreateButton(unitType, true);
                }
            }
        }

        private void ToggleToolWeapon(ToolWeaponTypes tWType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                if (HintComC.IsOnHint)
                {
                    if (tWType == ToolWeaponTypes.Pick)
                    {
                        if (!HintDataUIC.IsActive(VideoClipTypes.Pick))
                        {
                            HintViewUIC.SetActiveHintZone(true);
                            HintViewUIC.SetVideoClip(VideoClipTypes.Pick);
                            HintDataUIC.SetActive(VideoClipTypes.Pick, true);
                        }
                    }
                    else
                    {
                        if (!HintDataUIC.IsActive(VideoClipTypes.UpgToolWeapon))
                        {
                            HintViewUIC.SetActiveHintZone(true);
                            HintViewUIC.SetVideoClip(VideoClipTypes.UpgToolWeapon);
                            HintDataUIC.SetActive(VideoClipTypes.UpgToolWeapon, true);
                        }
                    }
                }

                if (SelectorC.Is(CellClickTypes.GiveTakeTW))
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
                    SelectorC.Set(CellClickTypes.GiveTakeTW);
                    TwGiveTakeC.TWTypeForGive = tWType;

                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                        //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    }
                    else TwGiveTakeC.SetLevel(tWType, LevelTWTypes.Iron);
                }
            }
        }

        private void ToggleUpgradeUnit()
        {
            if (WhoseMoveC.IsMyMove)
            {
                if (HintComC.IsOnHint)
                {
                    if (!HintDataUIC.IsActive(VideoClipTypes.UpgToolWeapon))
                    {
                        HintViewUIC.SetActiveHintZone(true);
                        HintViewUIC.SetVideoClip(VideoClipTypes.UpgToolWeapon);
                        HintDataUIC.SetActive(VideoClipTypes.UpgToolWeapon, true);
                    }
                }
                SelectorC.Set(CellClickTypes.UpgradeUnit);
            }
        }

        private void Hero()
        {
            SelectorC.Set(CellClickTypes.OldNewUnit);
            OldNewC.Set(HeroInvC.Hero(WhoseMoveC.CurPlayerI));
        }
    }
}