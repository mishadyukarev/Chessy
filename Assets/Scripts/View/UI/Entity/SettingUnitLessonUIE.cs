using Chessy.Common.Component;
using UnityEngine;

namespace Chessy.Model.View.UI.Entity
{
    public readonly struct SettingUnitLessonUIE
    {
        public readonly GameObjectVC ParengGOVC;

        public SettingUnitLessonUIE(in Transform needSetKingZone)
        {
            ParengGOVC = new GameObjectVC(needSetKingZone.gameObject);
        }
    }
}