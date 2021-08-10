using Leopotam.Ecs;

internal sealed class DestroyMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    //private EcsWorld _currentGameWorld;
    //private EcsFilter<InfoMasCom> _infoFilter;
    //private EcsFilter<DestroyMasCom, XyCellForDoingMasCom> _destroyFilter;

    public void Init()
    {
        //_currentGameWorld.NewEntity()
        //    .Replace(new DestroyMasCom())
        //    .Replace(new XyCellForDoingMasCom(new int[2]));
    }

    public void Run()
    {
        //var sender = _infoFilter.Get1(0).FromInfo.Sender;
        //var xyForDestory = _destroyFilter.Get2(0).XyCellForDoing;


        //if (CellUnitsDataSystem.HaveMaxAmountSteps(xyForDestory))
        //{
        //    var buildingType = CellBuildDataSystem.BuildTypeCom(xyForDestory).BuildingType;

        //    RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Destroy);

        //    if (buildingType == BuildingTypes.City)
        //    {
        //        RPCGameSystem.EndGameToMaster(CellUnitsDataSystem.ActorNumber(xyForDestory));
        //    }
        //    CellUnitsDataSystem.ResetAmountSteps(xyForDestory);

        //    if (buildingType == BuildingTypes.Farm) CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.Fertilizer, xyForDestory);

        //    MainGameSystem.XyBuildingsCom.RemoveXyBuild(buildingType, CellUnitsDataSystem.IsMasterClient(xyForDestory), xyForDestory);
        //    CellBuildDataSystem.ResetBuild(xyForDestory);
        //}
        //else
        //{
        //    RPCGameSystem.MistakeStepsUnitToGeneral(sender);
        //    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
        //}
    }
}