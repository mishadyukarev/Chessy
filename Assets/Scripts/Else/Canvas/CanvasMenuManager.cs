using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class CanvasMenuManager : CanvasManager
{
    internal GameObject InMenuZone;

    internal TextMeshProUGUI LogText;
    internal Button CreateRoomButton;
    internal Button JoinRandomButton;
    internal Button QuitButton;

    internal CanvasMenuManager(Canvas canvas) : base(canvas)
    {
        InMenuZone = GameObject.Find("InMenuZone");

        LogText = GameObject.Find("LogText").GetComponent<TextMeshProUGUI>();
        CreateRoomButton = GameObject.Find("CreateRoomButton").GetComponent<Button>();
        JoinRandomButton = GameObject.Find("JoinRandomButton").GetComponent<Button>();
        QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
    }


    internal void Active(bool isActive)
    {
        InMenuZone.gameObject.SetActive(isActive);
    }
}
