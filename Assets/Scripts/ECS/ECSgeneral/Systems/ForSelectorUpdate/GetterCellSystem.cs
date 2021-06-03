internal sealed class GetterCellSystem : SystemGeneralReduction
{
    internal GetterCellSystem() { }

    public override void Run()
    {
        if (_eGM.SelectorEnt_RayCom.RaycastHit2D != default)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int one = _eGM.CellEnt_CellBaseCom(x, y).InstanceIDGO;
                    int two = _eGM.SelectorEnt_RayCom.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _eGM.SelectorEntSelectorCom.XYcurrentCell = new int[] { x, y };
                        _eGM.SelectorEntSelectorCom.IsGettedCell = true;
                        return;
                    }
                }
            }
            _eGM.SelectorEntSelectorCom.XYcurrentCell = new int[] { -1, -1 };
            _eGM.SelectorEntSelectorCom.IsGettedCell = false;
        }
    }
}
