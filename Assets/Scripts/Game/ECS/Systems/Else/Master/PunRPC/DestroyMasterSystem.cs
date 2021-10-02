using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

internal sealed class DestroyMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoCom> _infoFilter = default;
    private EcsFilter<ForDestroyMasCom> _destroyFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    private EcsFilter<EndGameDataUIComponent> _endGameFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.sender;
        var idxCellForDestory = _destroyFilter.Get1(0).IdxForDestroy;

        ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCellForDestory);
        ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCellForDestory);
        ref var curBuildDataCom = ref _cellBuildFilter.Get1(idxCellForDestory);
        ref var curOwnerBuildCom = ref _cellBuildFilter.Get2(idxCellForDestory);
        ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCellForDestory);


        if (curUnitDataCom.HaveMaxAmountSteps)
        {
            RpcSys.SoundToGeneral(PhotonTargets.All, SoundEffectTypes.Destroy);

            if (curBuildDataCom.IsBuildType(BuildingTypes.City))
            {
                _endGameFilter.Get1(0).PlayerWinner = curOwnUnitCom.PlayerType;
            }
            curUnitDataCom.ResetAmountSteps();

            if (curBuildDataCom.IsBuildType(BuildingTypes.Farm)) curEnvDataCom.ResetEnvironment(EnvirTypes.Fertilizer);

            curBuildDataCom.DefBuildType();
        }
        else
        {
            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
        }
    }
}