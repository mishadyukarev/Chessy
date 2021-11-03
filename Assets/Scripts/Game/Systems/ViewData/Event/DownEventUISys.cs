using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class DownEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            HeroZoneUIC.AddListScout(ExecuteScout);

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

        }

        private void ExecuteScout()
        {
            SelectorC.CellClickType = CellClickTypes.OldToNewUnit;
            SelectorC.UnitTypeOldToNew = UnitTypes.Scout;
        }

        private void Done()
        {
            if (!InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelUnitTypes.Wood))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectC.Play(ClipGameTypes.Mistake);
            }

            SelectorC.DefCellClickType();
            SelectorC.DefSelectedUnit();
        }

        private void CreateUnit(UnitTypes unitType)
        {
            GetterUnitsDataUIC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove) RpcSys.CreateUnitToMaster(unitType);
        }

        private void GetUnit(UnitTypes unitType)
        {
            SelectorC.DefCellClickType();
            SelectorC.IdxCurCell = default;
            SelectorC.IdxPreVisionCell = default;
            SelectorC.DefSelectedCell();
            GetterUnitsDataUIC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.Iron))
                {
                    SelectorC.SelUnitType = unitType;
                    SelectorC.LevelSelUnitType = LevelUnitTypes.Iron;
                }
                else if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayerI, unitType, LevelUnitTypes.Wood))
                {
                    SelectorC.SelUnitType = unitType;
                    SelectorC.LevelSelUnitType = LevelUnitTypes.Wood;
                }
                else
                {
                    GetterUnitsDataUIC.ActiveNeedCreateButton(unitType, true);
                }

            }
        }

        private void ToggleToolWeapon(ToolWeaponTypes tWType)
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

            if (WhoseMoveC.IsMyMove)
            {
                if (SelectorC.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        if (SelectorC.TWTypeForGive == tWType)
                        {
                            if (GiveTakeDataUIC.Level(tWType) == LevelTWTypes.Wood) GiveTakeDataUIC.SetLevel(tWType, LevelTWTypes.Iron);
                            else GiveTakeDataUIC.SetLevel(tWType, LevelTWTypes.Wood);
                        }
                        else
                        {
                            SelectorC.TWTypeForGive = tWType;
                            GiveTakeDataUIC.SetLevel(tWType, LevelTWTypes.Wood);
                        }
                    }
                    else
                    {
                        SelectorC.TWTypeForGive = tWType;
                        GiveTakeDataUIC.SetLevel(tWType, LevelTWTypes.Iron);
                    }
                }
                else
                {
                    SelectorC.CellClickType = CellClickTypes.GiveTakeTW;
                    SelectorC.TWTypeForGive = tWType;

                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                        //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    }
                    else GiveTakeDataUIC.SetLevel(tWType, LevelTWTypes.Iron);
                }
            }
        }

        private void ToggleUpgradeUnit()
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
            SelectorC.CellClickType = CellClickTypes.UpgradeUnit;
        }
    }
}