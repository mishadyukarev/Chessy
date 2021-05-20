using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class AttackUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal int[] XyPreviousCell => _eMM.MasterRPCEntXySelPreCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.MasterRPCEntXySelPreCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    private bool _isAttacked;

    internal AttackUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {

        bool isMeleeAttack = false;

        switch (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                isMeleeAttack = true;
                break;

            case UnitTypes.Pawn:
                isMeleeAttack = true;
                break;

            case UnitTypes.Rook:
                isMeleeAttack = false;
                break;

            case UnitTypes.Bishop:
                isMeleeAttack = false;
                break;

            default:
                break;
        }

        _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).GetCellsForAttack(Info.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack);

        var isFindedSimple = InstanceGame.CellBaseOperations.TryFindCellInList(XySelectedCell, availableCellsSimpleAttack);
        var isFindedUnique = InstanceGame.CellBaseOperations.TryFindCellInList(XySelectedCell, availableCellsUniqueAttack);


        if (isFindedSimple || isFindedUnique)
        {
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).AmountSteps = 0;
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).IsProtected = false;
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).IsRelaxed = false;

            int damageToPrevious = 0;
            int damageToSelelected = 0;


            damageToSelelected += _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).SimplePowerDamage;
            damageToSelelected -= _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).PowerProtection;


            if (isMeleeAttack)
            {
                damageToPrevious += _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).SimplePowerDamage;

                if (isFindedUnique)
                {
                    damageToSelelected += _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).UniquePowerDamage;
                }
            }

            else
            {
                if (isFindedUnique)
                {
                    damageToSelelected += _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).UniquePowerDamage;
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).AmountHealth -= damageToPrevious;
            _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth -= damageToSelelected;


            if (_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
            {
                _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).ResetUnit();
            }

            if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType == UnitTypes.King)
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_OwnerCom(XyPreviousCell).ActorNumber);

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).ResetUnit();
                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Rook && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Bishop)
                {
                    _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).SetUnit(XyPreviousCell);
                    _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).ResetUnit();
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        _photonPunRPC.AttackUnitToGeneral(Info.Sender, _isAttacked, _isAttacked);
        _photonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked);
    }
}
