using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct SelectorUIC
    {
        private static Image _back_Image;
        private static GameObject _pickAdultForest_GO;
        private static GameObject _giveTakeTool_GO;

        internal SelectorUIC(GameObject centerZone_GO)
        {
            var selZone_Trans = centerZone_GO.transform.Find("SelectorTypeZone");

            _back_Image = selZone_Trans.Find("Back_Image").GetComponent<Image>();
            _pickAdultForest_GO = selZone_Trans.Find("PickAdultForestZone").gameObject;
            _giveTakeTool_GO = selZone_Trans.Find("GiveTakeToolZone").gameObject;
        }


        internal static void SetActiveBack(bool isActive) => _back_Image.gameObject.SetActive(isActive);

        internal static void SetActivePickAdultForest(bool isActive) => _pickAdultForest_GO.SetActive(isActive);
        internal static void SetActiveGiveTake(bool isActive) => _giveTakeTool_GO.SetActive(isActive);
    }
}
