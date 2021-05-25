using UnityEngine;
using static MainGame;

internal struct CellComponent
{
    private int[] _xy;
    private bool _isStartMaster;
    private bool _isStartOther;
    private GameObject _cellGO;

    internal bool IsSelected;
    internal bool IsStartMaster => _isStartMaster;
    internal bool IsStartOther => _isStartOther;
    internal int InstanceID => _cellGO.GetInstanceID();
    internal bool IsActiveSelf => _cellGO.activeSelf;


    internal CellComponent(StartValuesGameConfig startValuesGameConfig, params int[] xy)
    {
        _xy = xy;

        if (startValuesGameConfig.IS_TEST)
        {
            _isStartMaster = true;
            _isStartOther = true;
        }
        else
        {
            _isStartMaster = _xy[startValuesGameConfig.Y] < 3 && _xy[startValuesGameConfig.X] > 2 && _xy[startValuesGameConfig.X] < 12;
            _isStartOther = _xy[startValuesGameConfig.Y] > 8 && _xy[startValuesGameConfig.X] > 2 && _xy[startValuesGameConfig.X] < 12;
        }

        IsSelected = false;

        _cellGO = Instance.GameObjectPool.CellsGO[_xy[startValuesGameConfig.X], _xy[startValuesGameConfig.Y]];
    }
}
