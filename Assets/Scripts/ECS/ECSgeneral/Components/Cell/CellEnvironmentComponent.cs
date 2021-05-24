using System.Collections.Generic;
using UnityEngine;

public struct CellEnvironmentComponent
{
    private EntitiesGeneralManager _eGM;
    private int[] _xy;

    private bool _haveFood;
    private bool _haveMountain;
    private bool _haveTree;
    private bool _haveHill;
    private GameObject _foodGO;
    private GameObject _mountainGO;
    private GameObject _treeGO;
    private GameObject _hillGO;
    internal int AmountTrees;

    internal bool HaveFood => _haveFood;
    internal bool HaveMountain => _haveMountain;
    internal bool HaveTree => _haveTree;
    internal bool HaveHill => _haveHill;

    internal List<EnvironmentTypes> ListEnvironmentTypes
    {
        get
        {
            List<EnvironmentTypes> listEnvironmentTypes = new List<EnvironmentTypes>();

            if (_haveFood) listEnvironmentTypes.Add(EnvironmentTypes.Food);
            if (_haveTree) listEnvironmentTypes.Add(EnvironmentTypes.Tree);
            if (_haveHill) listEnvironmentTypes.Add(EnvironmentTypes.Hill);

            return listEnvironmentTypes;
        }
    }



    internal CellEnvironmentComponent(EntitiesGeneralManager eGM, GameObjectPool gameObjectPool, params int[] xy)
    {
        _eGM = eGM;
        _xy = xy;

        AmountTrees = default;

        _haveFood = false;
        _haveMountain = false;
        _haveTree = false;
        _haveHill = false;

        _foodGO = gameObjectPool.CellEnvironmentFoodGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _mountainGO = gameObjectPool.CellEnvironmentMountainGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _treeGO = gameObjectPool.CellEnvironmentTreeGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _hillGO = gameObjectPool.CellEnvironmentHillGOs[_xy[_eGM.X], _xy[_eGM.Y]];
    }


    internal void SetResetEnvironment(bool isActive, EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.Mountain:
                _haveMountain = isActive;
                _mountainGO.SetActive(isActive);
                break;

            case EnvironmentTypes.Tree:
                _haveTree = isActive;
                _treeGO.SetActive(isActive);
                AmountTrees = Random.Range(30, 50);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = isActive;
                _hillGO.SetActive(isActive);
                break;

            case EnvironmentTypes.Food:
                _haveFood = isActive;
                _foodGO.SetActive(isActive);
                break;

            default:
                break;
        }
    }
}
