using Assets.Scripts;
using Photon.Pun;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class AttackUnitMasterSystem : RPCMasterSystemReduction
{
    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    private bool _isAttacked;


    public override void Run()
    {
        base.Run();

        CellUnitWorker.GetCellsForAttack(Info.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, XyPreviousCell);

        var isFindedSimple = TryFindCellInList(XySelectedCell, availableCellsSimpleAttack);
        var isFindedUnique = TryFindCellInList(XySelectedCell, availableCellsUniqueAttack);


        if (isFindedSimple || isFindedUnique)
        {
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).AmountSteps = 0;
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).IsProtected = false;
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).IsRelaxed = false;

            int damageToPrevious = 0;
            int damageToSelelected = 0;


            damageToSelelected += CellUnitWorker.SimplePowerDamage(XyPreviousCell);
            damageToSelelected -= CellUnitWorker.PowerProtection(XySelectedCell);


            if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).IsMelee)
            {
                damageToPrevious += CellUnitWorker.SimplePowerDamage(XySelectedCell);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(XyPreviousCell);
                }
            }

            else
            {
                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(XyPreviousCell);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).AmountHealth -= damageToPrevious;
            _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth -= damageToSelelected;


            if (!_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType == UnitTypes.King)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                    {
                        _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).ActorNumber);
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                    {

                    }
                }


                CellUnitWorker.ResetUnit(XyPreviousCell);
            }

            if (!_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType == UnitTypes.King)
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).ActorNumber);

                CellUnitWorker.ResetUnit(XySelectedCell);
                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Rook
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.RookCrossbow
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Bishop
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.BishopCrossbow)
                {
                    CellUnitWorker.ShiftUnit(XyPreviousCell, XySelectedCell);
                    CellUnitWorker.ResetUnit(XyPreviousCell);
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        _photonPunRPC.AttackUnitToGeneral(Info.Sender, _isAttacked, _isAttacked);
        _photonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, XyPreviousCell, XySelectedCell);
    }
}
