using UnityEngine;

internal struct ParentComponent
{
    private GameObject _parentGO;

    internal void SetParent(GameObject parentGO) => _parentGO = parentGO;

    internal void SetActive(bool isActive) => _parentGO.SetActive(isActive);
}