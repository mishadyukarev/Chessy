using Leopotam.Ecs;


public struct AttackUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;

    public AttackUnitMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
    }


    public void AttackUnit(in int[] xyPreviousCellIN, in int[] xySelectedCellIN)
    {
        _cellManager.CopyXYinTo(xyPreviousCellIN, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCellIN, _xySelectedCellIN);

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(AttackUnitMasterSystem));
    }

    public void Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN)
    {
        xyPreviousCellIN = _xyPreviousCellIN;
        xySelectedCellIN = _xySelectedCellIN;
    }
}



public class AttackUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;


    internal AttackUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _attackUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.AttackUnitMasterComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }

    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN);


        CellUnitComponent(xyPreviousCellIN).AmountSteps -= _startValuesGameConfig.AMOUNT_STEPS_PAWN;
        CellUnitComponent(xyPreviousCellIN).IsProtected = false;
        CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;


        if(CellUnitComponent(xySelectedCellIN).HaveUnit)
        {
            int damageToPrevious = 0;
            damageToPrevious += CellUnitComponent(xySelectedCellIN).PowerDamage;

            int damageToSelelected = 0;
            damageToSelelected += CellUnitComponent(xyPreviousCellIN).PowerDamage;
            damageToSelelected -= CellUnitComponent(xySelectedCellIN).PowerProtection;
            damageToSelelected -= CellEnvironmentComponent(xySelectedCellIN).PowerProtection;
            damageToSelelected -= CellBuildingComponent(xySelectedCellIN).PowerProtection;

            if (damageToSelelected <= 0) damageToSelelected = 0;


            CellUnitComponent(xyPreviousCellIN).AmountHealth -= damageToPrevious;
            CellUnitComponent(xySelectedCellIN).AmountHealth -= damageToSelelected;


            if (CellUnitComponent(xyPreviousCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
            {
                CellUnitComponent(xyPreviousCellIN).ResetUnit();
            }

            if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
            {
                CellUnitComponent(xySelectedCellIN).ResetUnit();
                CellUnitComponent(xySelectedCellIN).SetUnit(CellUnitComponent(xyPreviousCellIN));
                CellUnitComponent(xyPreviousCellIN).ResetUnit();
            }
        }

        else if (CellBuildingComponent(xySelectedCellIN).HaveBuilding)
        {

        }
    }
}
