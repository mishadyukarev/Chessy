using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Photon.Pun;

internal sealed class DestroyMasterSystem : SystemMasterReduction
{
    private int[] XyCell => _eMM.DestroyEnt_XyCellCom.XyCell;


    public override void Run()
    {
        base.Run();

        if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCell))
        {
            var buildingType = CellBuildDataSystem.BuildTypeCom(XyCell).BuildingType;

            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (buildingType == BuildingTypes.City)
            {
                PhotonPunRPC.EndGameToMaster(CellUnitsDataSystem.ActorNumber(XyCell));
            }
            CellUnitsDataSystem.ResetAmountSteps(XyCell);

            if (buildingType == BuildingTypes.Farm) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, XyCell);

            InitSystem.XyBuildingsCom.RemoveXyBuild(buildingType, CellUnitsDataSystem.IsMasterClient(XyCell), XyCell);
            CellBuildDataSystem.ResetBuild(XyCell);
        }
        else
        {
            PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
        }
    }
}