using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct UniqFirstButViewC
    {
        private static Button _button;
        private static Dictionary<UniqFirstAbilTypes, GameObject> _zones;

        public UniqFirstButViewC(Transform parent)
        {
            _button = parent.Find("First").GetComponent<Button>();

            _zones = new Dictionary<UniqFirstAbilTypes, GameObject>();
            _zones.Add(UniqFirstAbilTypes.Seed, _button.transform.Find("SeedAdForest").gameObject);
            _zones.Add(UniqFirstAbilTypes.FirePawn, _button.transform.Find("FirePawn").gameObject);
            _zones.Add(UniqFirstAbilTypes.PutOutFirePawn, _button.transform.Find("NoneFire").gameObject);
            _zones.Add(UniqFirstAbilTypes.FireArcher, _button.transform.Find("FireArcher").gameObject);
            _zones.Add(UniqFirstAbilTypes.CircularAttack, _button.transform.Find("CircularAttack").gameObject);
        }

        public static void AddListener(UnityAction action) => _button.onClick.AddListener(action);
        public static void SetActive(UniqFirstAbilTypes ability)
        {
            if(ability == default)
            {
                _button.gameObject.SetActive(false);
            }
            else
            {
                _button.gameObject.SetActive(true);

                _zones[ability].SetActive(true);
                foreach (var item in _zones)
                {
                    if (item.Key != ability)
                    {
                        _zones[item.Key].SetActive(false);
                    }
                }
            }
        }
    }
}