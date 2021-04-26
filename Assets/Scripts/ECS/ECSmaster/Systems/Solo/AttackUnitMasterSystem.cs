﻿using Leopotam.Ecs;
using Photon.Realtime;

public struct AttackUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _playerIN;

    private bool _isAttacked;
    private bool _isKilledAttacker;
    private bool _isKilledDefender;

    public AttackUnitMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _isAttacked = default;
        _isKilledAttacker = default;
        _isKilledDefender = default;
    }


    public bool TryAttackUnit(in int[] xyPreviousCellIN, in int[] xySelectedCellIN, Player playerIN, out bool isKilledAttacker, out bool isKilledDefender)
    {
        _cellManager.CopyXYinTo(xyPreviousCellIN, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCellIN, _xySelectedCellIN);
        _playerIN = playerIN;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(AttackUnitMasterSystem));

        isKilledAttacker = _isKilledAttacker;
        isKilledDefender = _isKilledDefender;
        return _isAttacked;
    }

    public void Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN)
    {
        playerIN = _playerIN;
        xyPreviousCellIN = _xyPreviousCellIN;
        xySelectedCellIN = _xySelectedCellIN;
    }

    internal void Pack(bool isAttacked, bool isKilledAttacker, bool isKilledDefender)
    {
        _isAttacked = isAttacked;
        _isKilledAttacker = isKilledAttacker;
        _isKilledDefender = isKilledDefender;
    }
}



public class AttackUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;


    internal AttackUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonGameManager) : base(eCSmanager, supportGameManager)
    {
        _photonPunRPC = photonGameManager.PhotonPunRPC;

        _attackUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.AttackUnitMasterComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }

    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN);

        var xyAvailableCellsForAttack = _unitPathComponentRef.Unref().GetAvailableCells(UnitPathTypes.Attack, xyPreviousCellIN, playerIN);

        if (CellUnitComponent(xyPreviousCellIN).MinAmountSteps)
        {
            if (CellUnitComponent(xyPreviousCellIN).IsHim(playerIN))
            {
                if (_cellManager.TryFindCellInList(xySelectedCellIN, xyAvailableCellsForAttack))
                {
                    if (CellUnitComponent(xySelectedCellIN).HaveUnit)
                    {
                        CellUnitComponent(xyPreviousCellIN).AmountSteps -= _startValuesGameConfig.MAX_AMOUNT_STEPS_PAWN;
                        CellUnitComponent(xyPreviousCellIN).IsProtected = false;
                        CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;

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

                        bool isKilledAttacked = false;
                        bool isKilledDefender = false;

                        if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            if (CellUnitComponent(xySelectedCellIN).UnitType == UnitTypes.King) _photonPunRPC.EndGame(CellUnitComponent(xyPreviousCellIN).ActorNumber);
                            else if (CellBuildingComponent(xySelectedCellIN).BuildingType == BuildingTypes.City) _photonPunRPC.EndGame(CellUnitComponent(xyPreviousCellIN).ActorNumber);

                            CellUnitComponent(xySelectedCellIN).ResetUnit();
                            CellUnitComponent(xySelectedCellIN).SetUnit(CellUnitComponent(xyPreviousCellIN));
                            CellUnitComponent(xyPreviousCellIN).ResetUnit();
                            isKilledDefender = true;


                            if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                            {
                                CellUnitComponent(xySelectedCellIN).ResetUnit();
                                isKilledAttacked = true;
                            }
                        }


                        _attackUnitMasterComponentRef.Unref().Pack(true, isKilledAttacked, isKilledDefender);
                    }
                }
            }
        }
    }
}