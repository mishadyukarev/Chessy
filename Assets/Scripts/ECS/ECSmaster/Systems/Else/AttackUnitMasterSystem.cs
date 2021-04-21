using Leopotam.Ecs;
using Photon.Realtime;


public struct AttackUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _fromPlayerIN;

    private bool _isAttackedOUT;

    public AttackUnitMasterComponent(StartValuesConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _fromPlayerIN = default;

        _isAttackedOUT = default;
    }


    public bool TryAttackUnit(in int[] xyPreviousCellIN, in int[] xySelectedCellIN, in Player fromPlayerIN)
    {
        _cellManager.CopyXYinTo(xyPreviousCellIN, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCellIN, _xySelectedCellIN);
        _fromPlayerIN = fromPlayerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(AttackUnitMasterSystem));

        return _isAttackedOUT;
    }

    public void Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player fromPlayerIN)
    {
        xyPreviousCellIN = _xyPreviousCellIN;
        xySelectedCellIN = _xySelectedCellIN;
        fromPlayerIN = _fromPlayerIN;
    }

    internal void Pack(bool isAttackedOUT) => _isAttackedOUT = isAttackedOUT;
}



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
