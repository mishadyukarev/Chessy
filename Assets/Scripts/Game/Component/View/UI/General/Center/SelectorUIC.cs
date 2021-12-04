using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct SelectorUIC
    {
        private static Image _back;
        private static Dictionary<CellClickTypes, GameObject> _selZones;
        private static Dictionary<UniqueAbilTypes, GameObject> _uniqZones;

        public SelectorUIC(GameObject centerZone)
        {
            var selZone = centerZone.transform.Find("SelectorTypeZone");

            _selZones = new Dictionary<CellClickTypes, GameObject>();
            _uniqZones = new Dictionary<UniqueAbilTypes, GameObject>();

            _back = selZone.Find("Back_Image").GetComponent<Image>();

            for (var click = CellClickTypes.SetUnit; click < CellClickTypes.End; click++)
            {
                click = (CellClickTypes)((int)click);
                var str = click.ToString();
                var go = selZone.Find(str).gameObject;

                _selZones.Add(click, go);

                
                if(click == CellClickTypes.UniqAbil)
                {
                    _uniqZones.Add(UniqueAbilTypes.FireArcher, go.transform.Find(UniqueAbilTypes.FireArcher.ToString()).gameObject);
                    _uniqZones.Add(UniqueAbilTypes.StunElfemale, go.transform.Find(UniqueAbilTypes.StunElfemale.ToString()).gameObject);
                    _uniqZones.Add(UniqueAbilTypes.ChangeDirWind, go.transform.Find(UniqueAbilTypes.ChangeDirWind.ToString()).gameObject);
                }
            }
        }

        public static void SyncView(CellClickTypes click, UniqueAbilTypes uniqAbil)
        {
            _back.gameObject.SetActive(false);
            foreach (var item in _selZones.Keys) _selZones[item].SetActive(false);
            foreach (var item in _uniqZones.Keys) _uniqZones[item].SetActive(false);

            if (_selZones.ContainsKey(click))
            {
                _back.gameObject.SetActive(true);
                _selZones[click].SetActive(true);


                if (_uniqZones.ContainsKey(uniqAbil))
                {
                    _uniqZones[uniqAbil].SetActive(true);
                }
            }
        }
    }
}
