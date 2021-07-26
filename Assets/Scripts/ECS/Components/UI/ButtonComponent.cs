using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

internal struct ButtonComponent
{
    internal Button Button { get; private set; }

    internal ButtonComponent(Button button) => Button = button;
}