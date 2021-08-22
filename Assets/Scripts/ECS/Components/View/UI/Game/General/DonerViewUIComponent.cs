using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct DonerViewUIComponent
    {
        private Button _doner_Button;

        internal DonerViewUIComponent(GameObject downZone_GO)
        {
            _doner_Button = downZone_GO.transform.Find("DonerButton").GetComponent<Button>();
        }

        internal void AddListener(UnityAction unityAction) => _doner_Button.onClick.AddListener(unityAction);
        internal void SetColor(Color color) => _doner_Button.image.color = color;
    }
}
