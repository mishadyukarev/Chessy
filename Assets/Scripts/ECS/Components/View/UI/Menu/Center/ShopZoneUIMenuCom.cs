using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Menu.Center
{
    internal struct ShopZoneUIMenuCom
    {
        private GameObject _shopZone_GO;
        private Button _exit_Button;

        internal ShopZoneUIMenuCom(Transform centerZone_Trans)
        {
            _shopZone_GO = centerZone_Trans.Find("ShopZone").gameObject;
            _exit_Button = _shopZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }

        internal void EnableZone() => _shopZone_GO.SetActive(true);
        internal void DisableZone() => _shopZone_GO.SetActive(false);

        internal void AddListExit_Button(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}
