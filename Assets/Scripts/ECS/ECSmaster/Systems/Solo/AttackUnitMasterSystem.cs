﻿using Leopotam.Ecs;
using Photon.Realtime;


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



        CellUnitComponent(xyPreviousCellIN).AmountSteps -= _startValues.AMOUNT_STEPS_PAWN;
        CellUnitComponent(xyPreviousCellIN).IsProtected = false;
        CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;

        int damageToSelelected = 0;

        if (CellEnvironmentComponent(xySelectedCellIN).HaveHill) damageToSelelected -= _startValues.PROTECTION_HILL;
        if (CellEnvironmentComponent(xySelectedCellIN).HaveTree) damageToSelelected -= _startValues.PROTECTION_TREE;


        switch (CellBuildingComponent(xySelectedCellIN).BuildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                damageToSelelected -= _startValues.PROTECTION_CITY;
                break;

            default:
                break;
        }

        damageToSelelected += CellUnitComponent(xyPreviousCellIN).PowerDamage;

        switch (CellUnitComponent(xySelectedCellIN).UnitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:

                if (CellUnitComponent(xySelectedCellIN).IsProtected) damageToSelelected -= _startValues.PROTECTION_KING;

                break;

            case UnitTypes.Pawn:

                if (CellUnitComponent(xySelectedCellIN).IsProtected) damageToSelelected -= _startValues.PROTECTION_PAWN;

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
    }
}
