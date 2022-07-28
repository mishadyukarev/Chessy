using Chessy.View.Component;
using UnityEngine;
namespace Chessy.View.UI.Center
{
    public readonly struct SettingGodLessonUIE
    {
        public readonly GameObjectVC ParentGOC;

        public SettingGodLessonUIE(in Transform settingGodZone)
        {
            ParentGOC = new GameObjectVC(settingGodZone.gameObject);
        }
    }
}