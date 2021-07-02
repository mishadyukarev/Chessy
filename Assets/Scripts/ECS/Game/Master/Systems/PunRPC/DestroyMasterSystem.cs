﻿using Assets.Scripts;
using Assets.Scripts.Static;
using Photon.Pun;

internal sealed class DestroyMasterSystem : RPCMasterSystemReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_CellOwnerCom(XyCell).IsHim(info.Sender))
        {
            if (CellUnitWorker.HaveMaxSteps(XyCell))
            {
                if (_eGM.CellBuildEnt_BuilTypeCom(XyCell).BuildingType == BuildingTypes.City)
                {
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyCell).ActorNumber);
                }
                _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;

                CellBuildingWorker.ResetBuilding(true, XyCell);
            }
        }
    }
}