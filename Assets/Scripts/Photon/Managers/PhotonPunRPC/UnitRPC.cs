using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal partial class PhotonPunRPC
{

    #region AttackUnit

    [PunRPC]
    private void AttackUnitGeneral(bool isAttacked)
    {

    }

    #endregion


    #region ShiftUnit

    internal void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell)
        => PhotonView.RPC(nameof(ShiftUnitMaster), RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void ShiftUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        List<int[]> xyAvailableCellsForShift = InstanceGame.CellManager.CellFinderWay.GetCellsForShift(xyPreviousCell);

        if (_eGM.CellUnitComponent(xyPreviousCell).IsHisUnit(info.Sender) && _eGM.CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                _eGM.CellUnitComponent(xySelectedCell).SetUnit(_eGM.CellUnitComponent(xyPreviousCell));


                _eGM.CellUnitComponent(xyPreviousCell).ResetUnit();


                _eGM.CellUnitComponent(xySelectedCell).AmountSteps
                    -= _eGM.CellUnitComponent(xySelectedCell).NeedAmountSteps(_eGM.CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes);
                if (_eGM.CellUnitComponent(xySelectedCell).AmountSteps < 0) _eGM.CellUnitComponent(xySelectedCell).AmountSteps = 0;

                _eGM.CellUnitComponent(xySelectedCell).IsProtected = false;
                _eGM.CellUnitComponent(xySelectedCell).IsRelaxed = false;
            }
        }

        RefreshAllToMaster();
    }

    #endregion


    #region Protect

    public void ProtectUnitToMaster(bool isActive, in int[] xyCell)
        => PhotonView.RPC(nameof(ProtectUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void ProtectUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = true;
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        RefreshAllToMaster();
    }

    #endregion


    #region Relax

    public void RelaxUnitToMaster(bool isActive, in int[] xyCell) => PhotonView.RPC(nameof(RelaxUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void RelaxUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = true;
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }
        RefreshAllToMaster();
    }

    #endregion


    internal void AttackUnitToMaster(int[] xyPreviousCell, int[] xySelectedCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RPCTypes.Attack, new object[] { xyPreviousCell, xySelectedCell });
    internal void AttackUnitToGeneral(Player playerTo, bool isAttacked, bool isActivatedSound) => PhotonView.RPC(NameRPC, playerTo, false, RPCTypes.Attack, new object[] { isAttacked, isActivatedSound });
    internal void AttackUnitToGeneral(RpcTarget rpcTarget, bool isAttacked, bool isActivatedSound) => PhotonView.RPC(NameRPC, rpcTarget, false, RPCTypes.Attack, new object[] { isAttacked, isActivatedSound });

    internal void BuildToMaster(int[] xyCell, BuildingTypes buildingType) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RPCTypes.Build, new object[] { xyCell, buildingType });
    internal void DestroyBuildingToMaster(int[] xyCell) => PhotonView.RPC(NameRPC, RpcTarget.MasterClient, true, RPCTypes.Destroy, new object[] { xyCell });

    [PunRPC]
    private void RPC(bool isToMaster, RPCTypes rPCType, object[] objects, PhotonMessageInfo info)
    {
        _eGM.GeneralRPCEntFromInfoCom.FromInfo = info;

        if (isToMaster)
        {
            switch (rPCType)
            {
                case RPCTypes.None:
                    break;

                case RPCTypes.Build:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _eMM.MasterRPCEntBuildingTypeCom.BuildingType = (BuildingTypes)objects[1];
                    _sMM.TryInvokeRunSystem(nameof(BuilderMasterSystem), _sMM.SoloSystems);
                    break;

                case RPCTypes.Destroy:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXyCellCom.XyCell);
                    _sMM.TryInvokeRunSystem(nameof(DestroyMasterSystem), _sMM.SoloSystems);
                    break;

                case RPCTypes.Attack:
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[0], _eMM.MasterRPCEntXySelPreCom.XyPrevious);
                    CellManager.CellBaseOperations.CopyXYinTo((int[])objects[1], _eMM.MasterRPCEntXySelPreCom.XySelected);
                    _sMM.TryInvokeRunSystem(nameof(AttackUnitMasterSystem), _sMM.SoloSystems);
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (rPCType)
            {
                case RPCTypes.None:
                    break;

                case RPCTypes.Build:
                    break;

                case RPCTypes.Destroy:
                    break;

                case RPCTypes.Attack:
                    if ((bool)objects[0])
                        _eGM.SelectorESelectorC.AttackUnitAction();
                    if ((bool)objects[1])
                        _soundComponentRef.Unref().AttackSoundAction();

                    break;

                default:
                    break;
            }
        }
        RefreshAllToMaster();
    }


    #region Unique Abilities Pawn

    public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType)
        => PhotonView.RPC(nameof(UniqueAbilityPawnMaster), RpcTarget.MasterClient, xy, uniqueAbilitiesPawnType);

    [PunRPC]
    private void UniqueAbilityPawnMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType, PhotonMessageInfo info)
    {
        switch (uniqueAbilitiesPawnType)
        {
            case UniqueAbilitiesPawnTypes.AbilityOne:



                break;

            case UniqueAbilitiesPawnTypes.AbilityTwo:
                break;

            case UniqueAbilitiesPawnTypes.AbilityThree:
                break;

            default:
                break;
        }

        RefreshAllToMaster();
    }

    #endregion

}
