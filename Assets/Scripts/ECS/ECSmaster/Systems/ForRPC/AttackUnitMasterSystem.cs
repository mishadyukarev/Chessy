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
        if (_eGM.CellUnitComponent(XyPreviousCell).MinAmountSteps && _eGM.CellUnitComponent(XyPreviousCell).IsHisUnit(Info.Sender)
            && _eGM.CellUnitComponent(XySelectedCell).HaveUnit)
        {

            bool isMeleeAttack = false;

            switch (_eGM.CellUnitComponent(XyPreviousCell).UnitType)
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


            InstanceGame.CellManager.CellFinderWay.GetCellsForAttack(XyPreviousCell, Info.Sender,
                out var availableCellsSimpleAttack, out var availableCellsUniqueAttack);


            var isFindedSimple = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(XySelectedCell, availableCellsSimpleAttack);
            var isFindedUnique = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(XySelectedCell, availableCellsUniqueAttack);


            if (isMeleeAttack)
            {
                if (isFindedSimple)
                {

                }

                if (isFindedUnique)
                {

                }
            }

            else
            {
                if (isFindedSimple)
                {

                }

                if (isFindedUnique)
                {

                }
            }


            if (isFindedSimple || isFindedUnique)
            {
                _eGM.CellUnitComponent(XyPreviousCell).AmountSteps = 0;
                _eGM.CellUnitComponent(XyPreviousCell).IsProtected = false;
                _eGM.CellUnitComponent(XyPreviousCell).IsRelaxed = false;

                int damageToPrevious = 0;
                int damageToSelelected = 0;

                switch (_eGM.CellUnitComponent(XyPreviousCell).UnitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        damageToPrevious += _eGM.CellUnitComponent(XySelectedCell).SimplePowerDamage;
                        break;

                    case UnitTypes.Pawn:
                        damageToPrevious += _eGM.CellUnitComponent(XySelectedCell).SimplePowerDamage;
                        break;

                    case UnitTypes.Rook:
                        break;

                    case UnitTypes.Bishop:
                        break;

                    default:
                        break;
                }


                damageToSelelected += _eGM.CellUnitComponent(XyPreviousCell).SimplePowerDamage;
                if (isFindedUnique) damageToSelelected += _eGM.CellUnitComponent(XyPreviousCell).UniquePowerDamage;
                damageToSelelected -= _eGM.CellUnitComponent(XySelectedCell).PowerProtection
                    (_eGM.CellEnvironmentComponent(XySelectedCell).ListEnvironmentTypes, _eGM.CellBuildingComponent(XySelectedCell).BuildingType);

                if (damageToSelelected < 0) damageToSelelected = 0;


                _eGM.CellUnitComponent(XyPreviousCell).AmountHealth -= damageToPrevious;
                _eGM.CellUnitComponent(XySelectedCell).AmountHealth -= damageToSelelected;

                if (_eGM.CellUnitComponent(XyPreviousCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
                {
                    _eGM.CellUnitComponent(XyPreviousCell).ResetUnit();
                }

                if (_eGM.CellUnitComponent(XySelectedCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
                {
                    if (_eGM.CellUnitComponent(XySelectedCell).UnitType == UnitTypes.King)
                        _photonPunRPC.EndGame(_eGM.CellUnitComponent(XyPreviousCell).ActorNumber);

                    _eGM.CellUnitComponent(XySelectedCell).ResetUnit();
                    if (_eGM.CellUnitComponent(XyPreviousCell).UnitType != UnitTypes.Rook && _eGM.CellUnitComponent(XyPreviousCell).UnitType != UnitTypes.Bishop)
                    {
                        _eGM.CellUnitComponent(XySelectedCell).SetUnit(_eGM.CellUnitComponent(XyPreviousCell));
                        _eGM.CellUnitComponent(XyPreviousCell).ResetUnit();
                    }
                }

                _isAttacked = true;
            }
            else _isAttacked = false;
        }
        else _isAttacked = false;

        _photonPunRPC.AttackUnitToGeneral(Info.Sender, _isAttacked, _isAttacked);
        _photonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked);
    }
}
