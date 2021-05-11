using Leopotam.Ecs;

public sealed class GetterCellSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<RayComponent> _rayComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    internal GetterCellSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
    }

    public void Run()
    {
        if (_rayComponentRef.Unref().RaycastHit2D != default)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    int one = CellComponent(x, y).InstanceIDcell;
                    int two = _rayComponentRef.Unref().RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _selectorComponentRef.Unref().XYcurrentCell = new int[] { x, y };
                        _selectorComponentRef.Unref().IsGettedCell = true;
                        return;
                    }
                }
            }
            _selectorComponentRef.Unref().XYcurrentCell = new int[] { -1, -1 };
            _selectorComponentRef.Unref().IsGettedCell = false;
        }
    }
}
