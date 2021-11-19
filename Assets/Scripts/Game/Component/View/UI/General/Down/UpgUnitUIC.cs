using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UpgUnitUIC
    {
        private static Button _button;

        public UpgUnitUIC(Transform downZone)
        {
            _button = downZone.Find(CellClickTypes.UpgradeUnit.ToString() + "_Button").GetComponent<Button>();
        }


        public static void AddList(UnityAction action) => _button.onClick.AddListener(action);
    }
}