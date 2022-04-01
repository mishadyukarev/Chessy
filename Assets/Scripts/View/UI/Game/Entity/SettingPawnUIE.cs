using Chessy.Common.Component;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game.View.UI.Entity
{
    public readonly struct SettingPawnUIE
    {
        public readonly GameObjectVC ParentGOC;

        public SettingPawnUIE(in Transform settingPawnZone)
        {
            ParentGOC = new GameObjectVC(settingPawnZone.gameObject);
        }
    }
}