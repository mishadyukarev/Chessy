using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct SelectorUIC
    {
        private static Image _back;
        private static Dictionary<CellClickTypes, GameObject> _selZones;
        private static Dictionary<UniqueAbilityTypes, GameObject> _uniqZones;

        public SelectorUIC(GameObject centerZone)
        {
            var selZone = centerZone.transform.Find("SelectorTypeZone");

            _selZones = new Dictionary<CellClickTypes, GameObject>();
            _uniqZones = new Dictionary<UniqueAbilityTypes, GameObject>();

            _back = selZone.Find("Back_Image").GetComponent<Image>();

            for (var click = CellClickTypes.SetUnit; click <= CellClickTypes.UniqAbil; click++)
            {
                click = (CellClickTypes)((int)click);
                var str = click.ToString();
                var go = selZone.Find(str).gameObject;

                _selZones.Add(click, go);


                if (click == CellClickTypes.UniqAbil)
                {
                    _uniqZones.Add(UniqueAbilityTypes.FireArcher, go.transform.Find(UniqueAbilityTypes.FireArcher.ToString()).gameObject);
                    _uniqZones.Add(UniqueAbilityTypes.StunElfemale, go.transform.Find(UniqueAbilityTypes.StunElfemale.ToString()).gameObject);
                    _uniqZones.Add(UniqueAbilityTypes.ChangeDirWind, go.transform.Find(UniqueAbilityTypes.ChangeDirWind.ToString()).gameObject);
                }
            }
        }

        public static void SyncView(CellClickTypes click, UniqueAbilityTypes uniqAbil)
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
