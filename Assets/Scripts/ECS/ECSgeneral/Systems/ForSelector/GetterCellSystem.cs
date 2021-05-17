using Leopotam.Ecs;

internal sealed class GetterCellSystem : CellGeneralReduction, IEcsRunSystem
{
    internal GetterCellSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (_eGM.RayComponentSelectorEnt.RaycastHit2D != default)
        {
            for (int x = 0; x < _eGM.Xcount; x++)
            {
                for (int y = 0; y < _eGM.Ycount; y++)
                {
                    int one = _eGM.CellComponent(x, y).InstanceIDcell;
                    int two = _eGM.RayComponentSelectorEnt.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _eGM.SelectorComponentSelectorEnt.XYcurrentCell = new int[] { x, y };
                        _eGM.SelectorComponentSelectorEnt.IsGettedCell = true;
                        return;
                    }
                }
            }
            _eGM.SelectorComponentSelectorEnt.XYcurrentCell = new int[] { -1, -1 };
            _eGM.SelectorComponentSelectorEnt.IsGettedCell = false;
        }
    }
}
