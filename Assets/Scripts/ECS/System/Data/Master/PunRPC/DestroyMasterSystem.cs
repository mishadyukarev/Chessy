using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Photon.Pun;
using static Assets.Scripts.CellEnvirDataContainer;

internal sealed class DestroyMasterSystem : SystemMasterReduction
{
    private int[] XyCell => _eMM.DestroyEnt_XyCellCom.XyCell;


    public override void Run()
    {
        base.Run();

        if (CellUnitsDataContainer.HaveMaxAmountSteps(XyCell))
        {
            var buildingType = CellBuildDataContainer.GetBuildingType(XyCell);

            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (buildingType == BuildingTypes.City)
            {
                PhotonPunRPC.EndGameToMaster(CellUnitsDataContainer.ActorNumber(XyCell));
            }
            CellUnitsDataContainer.ResetAmountSteps(XyCell);

            if (buildingType == BuildingTypes.Farm) ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);

            InfoBuidlingsDataContainer.RemoveXyBuild(buildingType, CellUnitsDataContainer.IsMasterClient(XyCell), XyCell);
            CellBuildDataContainer.ResetBuild(XyCell);
        }
        else
        {
            PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
    }
}