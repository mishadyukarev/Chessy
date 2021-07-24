using UnityEngine;

internal struct CellBaseComponent
{
    private GameObject _parentGO;

    internal void StartFill(GameObject parentGO)
    {
        _parentGO = parentGO;
    }
}
