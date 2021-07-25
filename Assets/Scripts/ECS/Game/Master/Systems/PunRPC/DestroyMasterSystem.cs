using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Photon.Pun;
using static Assets.Scripts.CellEnvirDataWorker;

internal sealed class DestroyMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private int[] XyCell => _eMM.DestroyEnt_XyCellCom.XyCell;


    public override void Run()
    {
        base.Run();

        if (CellUnitsDataWorker.HaveMaxAmountSteps(XyCell))
        {
            var buildingType = CellBuildingsDataWorker.GetBuildingType(XyCell);

            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (buildingType == BuildingTypes.City)
            {
                PhotonPunRPC.EndGameToMaster(CellUnitsDataWorker.ActorNumber(XyCell));
            }
            CellUnitsDataWorker.ResetAmountSteps(XyCell);

            if (buildingType == BuildingTypes.Farm) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);

            InfoBuidlingsWorker.TakeXyBuildingsInGame(buildingType, CellUnitsDataWorker.IsMasterClient(XyCell), XyCell);
            CellBuildingsDataWorker.ResetBuilding(XyCell);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
    }
}