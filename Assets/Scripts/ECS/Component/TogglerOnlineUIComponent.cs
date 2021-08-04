using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component
{
    internal struct TogglerOnlineUIComponent
    {
        private Button _joinOnlineOfflineButton;
        private Dictionary<SupportLayerTypes, Image> _supportLayers;

        internal void AddListener(UnityAction unityAction) => _joinOnlineOfflineButton.onClick.AddListener(unityAction);
        internal void SetActiveButton(bool isActive) => _joinOnlineOfflineButton.gameObject.SetActive(isActive);


        internal void SetActiveSupLayers(SupportLayerTypes supportLayerType, bool isActive) => _supportLayers[supportLayerType].gameObject.SetActive(isActive);

    }
}
