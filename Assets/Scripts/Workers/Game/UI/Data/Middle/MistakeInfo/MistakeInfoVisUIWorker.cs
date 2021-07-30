using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo
{
    internal sealed class MistakeInfoVisUIWorker
    {
        private static MistakeInfoVisUIContainer _ourContainer;

        internal MistakeInfoVisUIWorker(MistakeInfoVisUIContainer ourContainer)
        {
            _ourContainer = ourContainer;
        }

        private static GameObject ParentGO => _ourContainer.MistakeInfoVisUIEnt_ParentCom.ParentGO;

        internal static void SetActiveParent(bool isActive) => ParentGO.SetActive(isActive);
    }
}
