using System.Collections.Generic;
using UnityEngine;

internal struct CellBaseComponent
{
    private GameObject _parentGO;
    private GameObject _cellGO;
    private Dictionary<bool, bool> _isStartedCellDict;

    internal bool IsSelected;

    internal bool IsStartedCell(bool isMaster) => _isStartedCellDict[isMaster];
    internal int InstanceIDGO => _cellGO.GetInstanceID();
    internal bool IsActiveSelfGO => _cellGO.activeSelf;

    internal void StartFill(GameObject parentGO, GameObject cellGO, Dictionary<bool, bool> isStartedDict)
    {
        _parentGO = parentGO;
        _cellGO = cellGO;

        _isStartedCellDict = isStartedDict;
    }
}
