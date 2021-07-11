using Assets.Scripts.Abstractions.Enums;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Main;

internal struct CellBaseComponent
{
    private GameObject _parentGO;
    private GameObject _cellGO;
    private Dictionary<bool, bool> _isStartedCellDict;

    internal bool IsStartedCell(bool key) => _isStartedCellDict[key];
    internal int InstanceIDGO => _cellGO.GetInstanceID();
    internal bool IsActiveSelfGO => _cellGO.activeSelf;

    internal float GetEulerAngle(XyzTypes xyzType)
    {
        switch (xyzType)
        {
            case XyzTypes.X:
                return _cellGO.transform.rotation.eulerAngles.x;

            case XyzTypes.Y:
                return _cellGO.transform.rotation.eulerAngles.y;

            case XyzTypes.Z:
                return _cellGO.transform.rotation.eulerAngles.z;

            default:
                throw new System.Exception();
        }
    }

    internal void StartFill(GameObject parentGO, GameObject cellGO, Dictionary<bool, bool> isStartedDict)
    {
        _parentGO = parentGO;
        _cellGO = cellGO;

        _isStartedCellDict = isStartedDict;
    }

    internal void RotateAndFixRot()
    {
        _cellGO.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
    }
}
