using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
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
        var unitType = CellUnitWorker.UnitType(XyCell);

        if (CellUnitWorker.HaveMaxAmountSteps(XyCell))
        {
            var buildingType = CellBuildingWorker.BuildingType(XyCell);

            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (buildingType == BuildingTypes.City)
            {
                PhotonPunRPC.EndGameToMaster(CellUnitWorker.ActorNumber(XyCell));
            }
            CellUnitWorker.ResetAmountSteps(XyCell);

            if (buildingType == BuildingTypes.Farm) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);

            InfoBuidlingsWorker.TakeAmountBuildingsInGame(buildingType, CellUnitWorker.IsMasterClient(XyCell), XyCell);
            CellBuildingWorker.ResetPlayerBuilding(XyCell);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
        //}
    }
}