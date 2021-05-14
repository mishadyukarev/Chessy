using Leopotam.Ecs;
using Photon.Realtime;
using static MainGame;

public struct AttackUnitMasterComponent
{
    private CellBaseOperations _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _playerIN;

    private bool _isAttacked;
    private bool _isKilledAttacker;
    private bool _isKilledDefender;

    public AttackUnitMasterComponent(StartValuesGameConfig nameValueManager, CellBaseOperations cellManager, SystemsMasterManager systemsMasterManager)
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

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Multiple, nameof(AttackUnitMasterSystem));

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



internal class AttackUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<AttackUnitMasterComponent> _attackUnitMasterComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;


    internal AttackUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _attackUnitMasterComponentRef = eCSmanager.EntitiesMasterManager.AttackUnitMasterComponentRef;
    }

    public void Run()
    {
        _attackUnitMasterComponentRef.Unref().Unpack(out int[] xyPreviousCellIN, out int[] xySelectedCellIN, out Player playerIN);

        InstanceGame.CellManager.CellFinderWay.GetCellsForAttack(xyPreviousCellIN, playerIN, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack);

        if (CellUnitComponent(xyPreviousCellIN).MinAmountSteps)
        {
            if (CellUnitComponent(xyPreviousCellIN).IsHisUnit(playerIN))
            {
                if (CellUnitComponent(xySelectedCellIN).HaveUnit)
                {
                    var isFindedSimple = _cellBaseOperations.TryFindCellInList(xySelectedCellIN, availableCellsSimpleAttack);
                    var isFindedUnique = _cellBaseOperations.TryFindCellInList(xySelectedCellIN, availableCellsUniqueAttack);

                    if (isFindedSimple || isFindedUnique)
                    {
                        CellUnitComponent(xyPreviousCellIN).AmountSteps = 0;
                        CellUnitComponent(xyPreviousCellIN).IsProtected = false;
                        CellUnitComponent(xyPreviousCellIN).IsRelaxed = false;

                        int damageToPrevious = 0;

                        if (CellUnitComponent(xyPreviousCellIN).UnitType != UnitTypes.Rook && CellUnitComponent(xyPreviousCellIN).UnitType != UnitTypes.Bishop)
                            damageToPrevious += CellUnitComponent(xySelectedCellIN).SimplePowerDamage;



                        int damageToSelelected = 0;

                        damageToSelelected += CellUnitComponent(xyPreviousCellIN).SimplePowerDamage;
                        if (isFindedUnique) damageToSelelected += CellUnitComponent(xyPreviousCellIN).UniquePowerDamage;
                        damageToSelelected -= CellUnitComponent(xySelectedCellIN).PowerProtection
                            (CellEnvironmentComponent(xySelectedCellIN).ListEnvironmentTypes, CellBuildingComponent(xySelectedCellIN).BuildingType);

                        if (damageToSelelected < 0) damageToSelelected = 0;


                        CellUnitComponent(xyPreviousCellIN).AmountHealth -= damageToPrevious;
                        CellUnitComponent(xySelectedCellIN).AmountHealth -= damageToSelelected;

                        bool isKilledAttacked = false;
                        bool isKilledDefender = false;

                        if (CellUnitComponent(xyPreviousCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            CellUnitComponent(xyPreviousCellIN).ResetUnit();
                            isKilledAttacked = true;
                        }

                        if (CellUnitComponent(xySelectedCellIN).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            if (CellUnitComponent(xySelectedCellIN).UnitType == UnitTypes.King) _photonPunRPC.EndGame(CellUnitComponent(xyPreviousCellIN).ActorNumber);

                            CellUnitComponent(xySelectedCellIN).ResetUnit();
                            if (CellUnitComponent(xyPreviousCellIN).UnitType != UnitTypes.Rook && CellUnitComponent(xyPreviousCellIN).UnitType != UnitTypes.Bishop)
                            {
                                CellUnitComponent(xySelectedCellIN).SetUnit(CellUnitComponent(xyPreviousCellIN));
                                CellUnitComponent(xyPreviousCellIN).ResetUnit();
                            }
                            isKilledDefender = true;
                        }

                        _attackUnitMasterComponentRef.Unref().Pack(true, isKilledAttacked, isKilledDefender);
                    }
                }
            }
        }
    }
}