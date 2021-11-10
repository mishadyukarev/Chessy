using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct SelectorUIC
    {
        private static Image _back_Image;
        private static Dictionary<CellClickTypes, GameObject> _selZones_GOs;

        public SelectorUIC(GameObject centerZone_GO)
        {
            var selZone_Trans = centerZone_GO.transform.Find("SelectorTypeZone");

            _selZones_GOs = new Dictionary<CellClickTypes, GameObject>();

            _back_Image = selZone_Trans.Find("Back_Image").GetComponent<Image>();
            _selZones_GOs.Add(CellClickTypes.PickFire, selZone_Trans.Find("PickAdultForestZone").gameObject);
            _selZones_GOs.Add(CellClickTypes.GiveTakeTW, selZone_Trans.Find("GiveTakeToolZone").gameObject);
            _selZones_GOs.Add(CellClickTypes.OldNewUnit, selZone_Trans.Find("ScoutZone").gameObject);
            _selZones_GOs.Add(CellClickTypes.UpgradeUnit, selZone_Trans.Find("UpgradeUnitZone").gameObject);
            _selZones_GOs.Add(CellClickTypes.StunElfemale, selZone_Trans.Find("UpgradeUnitZone").gameObject);
            _selZones_GOs.Add(CellClickTypes.PutOutFireElfemale, selZone_Trans.Find("UpgradeUnitZone").gameObject);
        }

        public static void SetActive(CellClickTypes cellClickType, bool isActive)
        {
            _back_Image.gameObject.SetActive(true);
            if (_selZones_GOs.ContainsKey(cellClickType)) _selZones_GOs[cellClickType].SetActive(isActive);
            else throw new System.Exception();
        }
        public static void DisableAll()
        {
            _back_Image.gameObject.SetActive(false);
            foreach (var item in _selZones_GOs.Keys) _selZones_GOs[item].SetActive(false);
        }
    }
}
