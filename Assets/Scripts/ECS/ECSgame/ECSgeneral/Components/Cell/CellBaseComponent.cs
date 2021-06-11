using System.Collections.Generic;
using UnityEngine;

internal struct CellBaseComponent
{
    private GameObject _parentGO;
    private GameObject _cellGO;
    private Dictionary<bool, bool> _isStartedDict;
    internal bool IsSelected;

    internal bool IsStarted(bool isMaster) => _isStartedDict[isMaster];
    internal int InstanceIDGO => _cellGO.GetInstanceID();
    internal bool IsActiveSelfGO => _cellGO.activeSelf;

    internal void Fill(GameObject parentGO, GameObject cellGO, Dictionary<bool, bool> isStartedDict)
    {
        _parentGO = parentGO;
        _cellGO = cellGO;

        _isStartedDict = isStartedDict;
    }
}
