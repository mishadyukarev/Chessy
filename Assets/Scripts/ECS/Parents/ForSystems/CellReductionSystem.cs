using Leopotam.Ecs;

public abstract class CellReduction : ReductionSystem
{
    protected CellManager _cellManager = default;

    protected EcsComponentRef<CellComponent>[,] _cellComponentRef;
    protected EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef;
    protected EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef;
    protected EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef;
    protected EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef;

    protected int X => _startValues.X;
    protected int Y => _startValues.Y;
    protected int XYforArray => _startValues.XY_FOR_ARRAY;

    protected int Xcount => _cellComponentRef.GetUpperBound(X) + 1;
    protected int Ycount => _cellComponentRef.GetUpperBound(Y) + 1;


    protected CellReduction(ECSmanager eCSmanager, SupportManager supportManager) : base(supportManager)
    {
        _cellManager = supportManager.CellManager;

        _cellComponentRef = eCSmanager.EntitiesGeneralManager.CellComponentRef;
        _cellEnvironmentComponentRef = eCSmanager.EntitiesGeneralManager.CellEnvironmentComponentRef;
        _cellSupportVisionComponentRef = eCSmanager.EntitiesGeneralManager.CellSupportVisionComponentRef;
        _cellUnitComponentRef = eCSmanager.EntitiesGeneralManager.CellUnitComponentRef;
        _cellBuildingComponentRef = eCSmanager.EntitiesGeneralManager.CellBuildingComponentRef;
    }


    protected ref CellComponent CellComponent(params int[] xy)
        => ref _cellComponentRef[xy[X], xy[Y]].Unref();
    protected ref CellComponent.UnitComponent CellUnitComponent(params int[] xy)
        => ref _cellUnitComponentRef[xy[X], xy[Y]].Unref();
    protected ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy)
        => ref _cellEnvironmentComponentRef[xy[X], xy[Y]].Unref();
    protected ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy)
        => ref _cellSupportVisionComponentRef[xy[X], xy[Y]].Unref();
    protected ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy)
        => ref _cellBuildingComponentRef[xy[X], xy[Y]].Unref();
}
