﻿using Leopotam.Ecs;

internal sealed class GetterCellSystem : CellGeneralReduction, IEcsRunSystem
{
    internal GetterCellSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (_eGM.RayComponentSelectorEnt.RaycastHit2D != default)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int one = _eGM.CellEnt_CellCom(x, y).InstanceIDcellGO;
                    int two = _eGM.RayComponentSelectorEnt.RaycastHit2D.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _eGM.SelectorESelectorC.XYcurrentCell = new int[] { x, y };
                        _eGM.SelectorESelectorC.IsGettedCell = true;
                        return;
                    }
                }
            }
            _eGM.SelectorESelectorC.XYcurrentCell = new int[] { -1, -1 };
            _eGM.SelectorESelectorC.IsGettedCell = false;
        }
    }
}
