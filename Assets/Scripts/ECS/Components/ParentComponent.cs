using UnityEngine;

internal struct ParentComponent
{
    internal GameObject ParentGO { get; set; }


    internal ParentComponent(GameObject go) => ParentGO = go;
}