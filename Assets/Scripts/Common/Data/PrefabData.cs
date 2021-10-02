using UnityEngine;

[CreateAssetMenu(menuName = "Data/PrefabConfig ", fileName = "PrefabConfig ")]
public class PrefabData : ScriptableObject
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _cellGO;
    [SerializeField] private GameObject _backGroundCollider2D;

    [SerializeField] public Canvas Canvas => _canvas;
    [SerializeField] public Camera Camera => _camera;
    [SerializeField] public GameObject CellGO => _cellGO;
    [SerializeField] public GameObject BackGroundCollider2D => _backGroundCollider2D;
}
