using UnityEngine;

[CreateAssetMenu(menuName = "Data/PrefabConfig ", fileName = "PrefabConfig ")]
internal class PrefabConfig : ScriptableObject
{
    [SerializeField] internal GameObject CellGO;
    [SerializeField] internal GameObject BackGroundCollider2D;
    [SerializeField] internal GameObject Camera;
}
