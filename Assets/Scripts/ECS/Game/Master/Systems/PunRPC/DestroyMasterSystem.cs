﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Pun;
using static Assets.Scripts.CellEnvironmentWorker;

internal sealed class DestroyMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCell => _eMM.DestroyEnt_XyCellCom.XyCell;


    public override void Run()
    {
        base.Run();

        //if (_eGM.CellUnitEnt_CellOwnerCom(XyCell).IsHim(Info.Sender))
        //{
        var unitType = _eGM.CellUnitEnt_UnitTypeCom(XyCell).UnitType;

        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps(unitType))
        {
            var buildingType = _eGM.CellBuildEnt_BuilTypeCom(XyCell).BuildingType;

            _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (buildingType == BuildingTypes.City)
            {
                _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyCell).ActorNumber);
            }
            _eGM.CellUnitEnt_CellUnitCom(XyCell).ResetAmountSteps();

            if (buildingType == BuildingTypes.Farm) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);
            CellBuildingWorker.ResetBuilding(true, XyCell);
        }
        else
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
        //}
    }
}