using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class DestroyMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
        ref var curOnwerCellUnitCom = ref _cellUnitFilter.Get2(idxCellForDestory);
        ref var curCellBuildDataCom = ref _cellBuildFilter.Get1(idxCellForDestory);
        ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


        if (curCellUnitDataCom.HaveMaxAmountSteps)
        {
            RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

            if (curCellBuildDataCom.IsBuildType(BuildingTypes.City))
            {
                //RpcSys.EndGameToMaster((byte)curOnwerCellUnitCom.ActorNumber);
            }
            curCellUnitDataCom.ResetAmountSteps();

            if (curCellBuildDataCom.IsBuildType(BuildingTypes.Farm)) curCellEnvDataCom.ResetEnvironment(EnvironmentTypes.Fertilizer);
 
            curCellBuildDataCom.ResetBuildType();
        }
        else
        {
            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
        }
    }
}