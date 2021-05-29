using System.Collections.Generic;
using UnityEngine;

internal struct CellBaseComponent
{
    private Dictionary<bool, bool> _isStartedDict;
    private GameObject _cellGO;

    internal bool IsSelected;
    internal bool IsStarted(bool isMaster) => _isStartedDict[isMaster];
    internal int InstanceIDGO => _cellGO.GetInstanceID();
    internal bool IsActiveSelfGO => _cellGO.activeSelf;


    internal CellBaseComponent(StartValuesGameConfig startValuesGameConfig, ObjectPool objectPool, int x, int y)
    {
        _isStartedDict = new Dictionary<bool, bool>();

        if (startValuesGameConfig.IS_TEST)
        {
            _isStartedDict[true] = true;
            _isStartedDict[false] = true;
        }
        else
        {
            _isStartedDict[true] = y < 3 && x > 2 && x < 12;
            _isStartedDict[false] = y > 8 && x > 2 && x < 12;
        }

        IsSelected = false;

        _cellGO = objectPool.CellsGO[x, y];
    }
}
