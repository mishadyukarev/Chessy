using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class StartSpawnMenu : StartSpawn
{
    internal TextMeshProUGUI LogText;

    internal Button CreateRoomButton;
    internal Button JoinRandomButton;
    internal Button QuitButton;


    internal StartSpawnMenu(SupportMenuManager supportMenuManager) : base(supportMenuManager.ResourcesLoadMenuManager)
    {
        _canvas = GameObject.Instantiate(supportMenuManager.ResourcesLoadMenuManager.Canvas);

        LogText = GameObject.Find("LogText").GetComponent<TextMeshProUGUI>();
        CreateRoomButton = GameObject.Find("CreateRoomButton").GetComponent<Button>();
        JoinRandomButton = GameObject.Find("JoinRandomButton").GetComponent<Button>();
        QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
    }
}
