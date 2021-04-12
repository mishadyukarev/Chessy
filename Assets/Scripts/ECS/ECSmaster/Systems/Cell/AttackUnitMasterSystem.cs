using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;

public class AttackUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef = default;


    internal AttackUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _attackUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.AttackUnitMasterComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }


    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN);

        _unitPathComponentRef.Unref().GetAvailableCellsForAttack(xyPreviousCellIN, playerIN, out List<int[]> xyAvailableCellsForAttack);

        if (CellUnitComponent(xyPreviousCellIN).HaveAmountSteps && CellUnitComponent(xyPreviousCellIN).IsHim(playerIN))
        {
            if (_cellManager.TryFindCellInList(xySelectedCellIN, xyAvailableCellsForAttack))
            {
                CellUnitComponent(xyPreviousCellIN).IsProtected = false;
                CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;

                int commonDamage = 0;
                if (CellEnvironmentComponent(xySelectedCellIN).HaveHill) commonDamage -= _nameValueManager.PROTECTION_HILL;
                if (CellEnvironmentComponent(xySelectedCellIN).HaveTree) commonDamage -= _nameValueManager.PROTECTION_TREE;

                switch (CellBuildingComponent(xySelectedCellIN).BuildingType)
                {
                    case BuildingTypes.None:
                        break;

                    case BuildingTypes.City:
                        commonDamage -= _nameValueManager.PROTECTION_CAMP;
                        break;

                    default:
                        break;
                }

                switch (CellUnitComponent(xyPreviousCellIN).UnitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        ExecutePawnAttack(xyPreviousCellIN, xySelectedCellIN, commonDamage);
                        break;

                    default:
                        break;
                }
            }
        }
    }

    private void ExecutePawnAttack(int[] xyPreviousCellIN, int[] xySelectedCellIN, int commonDamage)
    {
        commonDamage += _nameValueManager.POWER_DAMAGE_PAWN;
        if (CellUnitComponent(xySelectedCellIN).IsProtected) commonDamage -= _nameValueManager.PROTECTION_PAWN;

        CellUnitComponent(xySelectedCellIN).TakeHealth(commonDamage);

        CellUnitComponent(xyPreviousCellIN).TakeAmountSteps(_nameValueManager.AMOUNT_STEPS_PAWN);

        if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _nameValueManager.AMOUNT_FOR_DEATH)
        {
            CellUnitComponent(xySelectedCellIN).SetResetUnit(CellUnitComponent(xyPreviousCellIN));
            CellUnitComponent(xyPreviousCellIN).ResetUnit();
        }
    }
}
