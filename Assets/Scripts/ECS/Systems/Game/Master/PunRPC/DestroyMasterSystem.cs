using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Photon.Pun;
using static Assets.Scripts.CellEnvirDataWorker;

internal sealed class DestroyMasterSystem : SystemMasterReduction
{
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

            InfoBuidlingsWorker.RemoveXyBuild(buildingType, CellUnitsDataWorker.IsMasterClient(XyCell), XyCell);
            CellBuildingsDataWorker.ResetBuild(XyCell);
        }
        else
        {
            PhotonPunRPC.MistakeStepsUnitToGeneral(RpcWorker.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
    }
}