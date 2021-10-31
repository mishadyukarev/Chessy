using Leopotam.Ecs;

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
            if (!InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, UnitTypes.King, LevelUnitTypes.Wood))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectC.Play(SoundEffectTypes.Mistake);
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
                if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, unitType, LevelUnitTypes.Iron))
                {
                    SelectorC.SelUnitType = unitType;
                    SelectorC.LevelSelUnitType = LevelUnitTypes.Iron;
                }
                else if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, unitType, LevelUnitTypes.Wood))
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
            if (WhoseMoveC.IsMyMove)
            {
                if (SelectorC.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        if (SelectorC.TWTypeForGive == tWType)
                        {
                            if (SelectorC.LevelTWType == LevelTWTypes.Wood) SelectorC.LevelTWType = LevelTWTypes.Iron;
                            else SelectorC.LevelTWType = LevelTWTypes.Wood;
                        }
                        else
                        {
                            SelectorC.TWTypeForGive = tWType;
                            SelectorC.LevelTWType = LevelTWTypes.Wood;
                        }
                    }
                    else
                    {
                        SelectorC.TWTypeForGive = tWType;
                        SelectorC.LevelTWType = LevelTWTypes.Iron;
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
                    else SelectorC.LevelTWType = LevelTWTypes.Iron;
                }
            }
        }

        private void ToggleUpgradeUnit() => SelectorC.CellClickType = CellClickTypes.UpgradeUnit;
    }
}