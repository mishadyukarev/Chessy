using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo
{
    internal sealed class CenterSupTextUIViewWorker
    {
        private static CenterSupTextUIViewContainer _container;

        private static GameObject ParentGO => _container.MistakeInfoVisUIEnt_ParentCom.ParentGO;
        private static TextMeshProUGUI TextMeshProUGUI => _container.MistakeInfoVisUIEnt_TextMeshProUGUICom.TextMeshProUGUI;
        internal static string Text
        {
            get => TextMeshProUGUI.text;
            set => TextMeshProUGUI.text = value;
        }

        internal CenterSupTextUIViewWorker(CenterSupTextUIViewContainer container)
        {
            _container = container;
        }

        internal static void SetActiveCenterBlock(bool isActive) => ParentGO.SetActive(isActive);
    }
}
