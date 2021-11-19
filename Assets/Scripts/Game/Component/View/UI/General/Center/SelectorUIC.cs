using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
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

            for (var click = CellClickTypes.Third; click < CellClickTypes.End; click++)
            {
                _selZones_GOs.Add(click, selZone_Trans.Find(click.ToString()).gameObject);
            }
        }

        public static void SyncView(CellClickTypes click)
        {
            _back_Image.gameObject.SetActive(false);
            foreach (var item in _selZones_GOs.Keys) _selZones_GOs[item].SetActive(false);

            if (_selZones_GOs.ContainsKey(click))
            {
                _back_Image.gameObject.SetActive(true);
                _selZones_GOs[click].SetActive(true);
            }
        }
    }
}
