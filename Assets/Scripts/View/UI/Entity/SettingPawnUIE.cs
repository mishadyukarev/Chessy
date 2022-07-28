using Chessy.View.Component;
using UnityEngine;
namespace Chessy.View.UI.Entity
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