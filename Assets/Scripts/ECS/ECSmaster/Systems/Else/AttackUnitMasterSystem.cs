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

        _startValues = supportManager.StartValuesConfig;
    }


    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN);

        _unitPathComponentRef.Unref().GetAvailableCellsForAttack(xyPreviousCellIN, playerIN, out List<int[]> xyAvailableCellsForAttack);

        if (CellUnitComponent(xyPreviousCellIN).HaveAmountSteps && CellUnitComponent(xyPreviousCellIN).IsHim(playerIN))
        {
            if (_cellManager.TryFindCellInList(xySelectedCellIN, xyAvailableCellsForAttack))
            {
                CellUnitComponent(xyPreviousCellIN).AmountSteps -= _startValues.AmountStepsPawn;
                CellUnitComponent(xyPreviousCellIN).IsProtected = false;
                CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;



                int damageToSelelected = 0;

                if (CellEnvironmentComponent(xySelectedCellIN).HaveHill) damageToSelelected -= _startValues.ProtectionHill;
                if (CellEnvironmentComponent(xySelectedCellIN).HaveTree) damageToSelelected -= _startValues.ProtectionTree;


                switch (CellBuildingComponent(xySelectedCellIN).BuildingType)
                {
                    case BuildingTypes.None:
                        break;

                    case BuildingTypes.City:
                        damageToSelelected -= _startValues.ProtectionCity;
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

                        damageToSelelected += _startValues.PowerDamagePawn;
                        if (CellUnitComponent(xySelectedCellIN).IsProtected) damageToSelelected -= _startValues.ProtectionPawn;

                        break;

                    default:
                        break;
                }

                switch (CellUnitComponent(xySelectedCellIN).UnitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        break;

                    default:
                        break;
                }


                CellUnitComponent(xySelectedCellIN).AmountHealth -= damageToSelelected;
                CellUnitComponent(xyPreviousCellIN).AmountHealth -= CellUnitComponent(xySelectedCellIN).PowerDamage;

                if(CellUnitComponent(xyPreviousCellIN).AmountHealth <= _startValues.AMOUNT_FOR_DEATH)
                {
                    CellUnitComponent(xyPreviousCellIN).ResetUnit();
                }   

                if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValues.AMOUNT_FOR_DEATH)
                {
                    CellUnitComponent(xySelectedCellIN).SetUnit(CellUnitComponent(xyPreviousCellIN));
                    CellUnitComponent(xyPreviousCellIN).ResetUnit();
                }
            }
        }
    }
}
