using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.UI.Game.General
{
    internal struct MistakeDataUICom
    {
        private Dictionary<ResourceTypes, bool> _needResources;

        internal MistakeTypes MistakeTypes { get; set; }
        internal float CurrentTime { get; set; }

        internal MistakeDataUICom(Dictionary<ResourceTypes, bool> needResources) : this()
        {
            _needResources = needResources;

            for (ResourceTypes resourcesType = 0; resourcesType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourcesType++)
            {
                _needResources.Add(resourcesType, default);
            }
        }

        internal void ResetMistakeType() => MistakeTypes = default;

        internal bool GetNeedResources(ResourceTypes resourceType) => _needResources[resourceType];
        internal void AddNeedResources(ResourceTypes resourceTypes) => _needResources[resourceTypes] = true;
        internal void ClearAllNeeds()
        {
            for (ResourceTypes resourcesType = 0; resourcesType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourcesType++)
            {
                _needResources[resourcesType] = default;
            }
        }
    }
}
