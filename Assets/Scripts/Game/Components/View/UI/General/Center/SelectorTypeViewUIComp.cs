using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct SelectorTypeViewUIComp
    {
        private Image _back_Image;
        private GameObject _pickAdultForest_GO;
        private GameObject _giveTakeTool_GO;

        internal SelectorTypeViewUIComp(GameObject centerZone_GO)
        {
            var selZone_Trans = centerZone_GO.transform.Find("SelectorTypeZone");

            _back_Image = selZone_Trans.Find("Back_Image").GetComponent<Image>();
            _pickAdultForest_GO = selZone_Trans.Find("PickAdultForestZone").gameObject;
            _giveTakeTool_GO = selZone_Trans.Find("GiveTakeToolZone").gameObject;
        }


        internal void SetActiveBack(bool isActive) => _back_Image.gameObject.SetActive(isActive);

        internal void SetActivePickAdultForest(bool isActive) => _pickAdultForest_GO.SetActive(isActive);
        internal void SetActiveGiveTake(bool isActive) => _giveTakeTool_GO.SetActive(isActive);
    }
}
