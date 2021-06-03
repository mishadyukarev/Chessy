using System;
using System.Collections.Generic;
using UnityEngine;
using static Main;

internal struct CellBaseComponent : IDisposable
{
    private Dictionary<bool, bool> _isStartedDict;
    private GameObject _cellGO;
    internal bool IsSelected;

    internal bool IsStarted(bool isMaster) => _isStartedDict[isMaster];
    internal int InstanceIDGO => _cellGO.GetInstanceID();
    internal bool IsActiveSelfGO => _cellGO.activeSelf;


    internal CellBaseComponent(List<IDisposable> disposables)
    {
        _isStartedDict = new Dictionary<bool, bool>();
        _cellGO = default;
        IsSelected = default;

        disposables.Add(this);
    }

    internal void Fill(ObjectPoolGame objectPoolGame, int x, int y)
    {      
        _cellGO = objectPoolGame.CellsGO[x, y];

        if (Instance.TestType == TestTypes.Standart)
        {
            _isStartedDict[true] = true;
            _isStartedDict[false] = true;
        }
        else
        {
            _isStartedDict[true] = y < 3 && x > 2 && x < 12;
            _isStartedDict[false] = y > 8 && x > 2 && x < 12;
        }
    }

    public void Dispose()
    {
        _isStartedDict[true] = default;
        _isStartedDict[false] = default;

        _cellGO = default;
        IsSelected = default;
    }
}
