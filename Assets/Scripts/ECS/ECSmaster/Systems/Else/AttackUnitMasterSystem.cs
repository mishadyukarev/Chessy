using Leopotam.Ecs;
using Photon.Realtime;

public class AttackUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;


    internal AttackUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _attackUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.AttackUnitMasterComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }

    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN);

        var xyAvailableCellsForAttackOUT = _unitPathComponentRef.Unref().GetAvailableCellsForAttack(xyPreviousCellIN, playerIN);

        if (CellUnitComponent(xyPreviousCellIN).HaveAmountSteps
            && CellUnitComponent(xyPreviousCellIN).IsHim(playerIN)
            && _cellManager.TryFindCellInList(xySelectedCellIN, xyAvailableCellsForAttackOUT))
        {
            CellUnitComponent(xyPreviousCellIN).AmountSteps -= _startValues.AMOUNT_STEPS_PAWN;
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

                    damageToSelelected += _startValues.PowerDamageKing;

                    break;

                case UnitTypes.Pawn:

                    damageToSelelected += _startValues.PowerDamagePawn;


                    break;

                default:
                    break;
            }

            switch (CellUnitComponent(xySelectedCellIN).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:

                    if (CellUnitComponent(xySelectedCellIN).IsProtected) damageToSelelected -= _startValues.ProtectionKing;

                    break;

                case UnitTypes.Pawn:

                    if (CellUnitComponent(xySelectedCellIN).IsProtected) damageToSelelected -= _startValues.ProtectionPawn;

                    break;

                default:
                    break;
            }


            CellUnitComponent(xySelectedCellIN).AmountHealth -= damageToSelelected;
            CellUnitComponent(xyPreviousCellIN).AmountHealth -= CellUnitComponent(xySelectedCellIN).PowerDamage;

            if (CellUnitComponent(xyPreviousCellIN).AmountHealth <= _startValues.AMOUNT_FOR_DEATH)
            {
                CellUnitComponent(xyPreviousCellIN).ResetUnit();
            }

            if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValues.AMOUNT_FOR_DEATH)
            {
                CellUnitComponent(xySelectedCellIN).ResetUnit();
                CellUnitComponent(xySelectedCellIN).SetUnit(CellUnitComponent(xyPreviousCellIN));
                CellUnitComponent(xyPreviousCellIN).ResetUnit();
            }

            _attackUnitMasterComponentRef.Unref().Pack(true);

        }
        else
        {
            _attackUnitMasterComponentRef.Unref().Pack(false);
        }
    }
}
