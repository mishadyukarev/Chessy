using Chessy.View.Component;
using UnityEngine;
namespace Chessy.View.UI.Entity
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