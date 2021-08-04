using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers
{
    internal sealed class CenterSupTextUIViewContainer
    {
        private EcsEntity _mistakeInfoVisUIEnt;
        internal ref ParentComponent MistakeInfoVisUIEnt_ParentCom => ref _mistakeInfoVisUIEnt.Get<ParentComponent>();
        internal ref TextMeshProUGUIComponent MistakeInfoVisUIEnt_TextMeshProUGUICom => ref _mistakeInfoVisUIEnt.Get<TextMeshProUGUIComponent>();

        internal CenterSupTextUIViewContainer(GameObject middleZone, EcsWorld gameWorld)
        {
            var mistakeZoneGO = middleZone.transform.Find("MistakeZone").gameObject;
            _mistakeInfoVisUIEnt = gameWorld.NewEntity()
                .Replace(new ParentComponent(mistakeZoneGO))
                .Replace(new TextMeshProUGUIComponent(mistakeZoneGO.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));
        }
    }
}
