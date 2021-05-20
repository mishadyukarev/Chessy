using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static MainGame;
using Photon.Realtime;

public struct CellComponent
{
    private EntitiesGeneralManager _eGM;

    private int[] _xy;
    private bool _isStartMaster;
    private bool _isStartOther;
    private GameObject _cellGO;

    private int _x => _eGM.X;
    private int _y => _eGM.X;

    internal bool IsSelected;
    internal bool IsStartMaster => _isStartMaster;
    internal bool IsStartOther => _isStartOther;
    internal int InstanceIDcellGO => _cellGO.GetInstanceID();



    internal CellComponent(EntitiesGeneralManager eGM, StartValuesGameConfig startValuesGameConfig, params int[] xy)
    {
        _eGM = eGM;

        _xy = xy;

        if (startValuesGameConfig.IS_TEST)
        {
            _isStartMaster = true;
            _isStartOther = true;
        }
        else
        {
            _isStartMaster = _xy[_eGM.Y] < 3 && _xy[_eGM.X] > 2 && _xy[_eGM.X] < 12;
            _isStartOther = _xy[_eGM.Y] > 8 && _xy[_eGM.X] > 2 && _xy[_eGM.X] < 12;
        }

        IsSelected = default;

        _cellGO = InstanceGame.GameObjectPool.CellsGO[_xy[_eGM.X], _xy[_eGM.Y]];
    }
}
