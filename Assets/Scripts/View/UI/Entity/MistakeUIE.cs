using Chessy.Model;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Chessy.View.UI.Entity
{
    public readonly struct MistakeUIE
    {
        readonly Dictionary<MistakeTypes, GameObjectVC> _zones;
        readonly Dictionary<ResourceTypes, TextUIC> _needAmountRes;

        public GameObjectVC Zones(in MistakeTypes mistake) => _zones[mistake];
        public TextUIC NeedAmountResources(in ResourceTypes res) => _needAmountRes[res];


        public HashSet<MistakeTypes> KeysMistake
        {
            get
            {
                var keys = new HashSet<MistakeTypes>();
                foreach (var item in _zones) keys.Add(item.Key);
                return keys;
            }
        }
        public HashSet<ResourceTypes> KeysResource
        {
            get
            {
                var keys = new HashSet<ResourceTypes>();
                foreach (var item in _needAmountRes) keys.Add(item.Key);
                return keys;
            }
        }


        public MistakeUIE(in Transform centerZone)
        {
            _zones = new Dictionary<MistakeTypes, GameObjectVC>();
            _needAmountRes = new Dictionary<ResourceTypes, TextUIC>();


            var mistakeZone = centerZone.Find("Mistake");



            for (var mistake = MistakeTypes.Economy; mistake < MistakeTypes.End; mistake++)
            {
                _zones.Add(mistake, new GameObjectVC(mistakeZone.Find(mistake.ToString()).gameObject));
            }

            for (var res = ResourceTypes.Food; res < ResourceTypes.End; res++)
            {
                var economy = mistakeZone.transform.Find(MistakeTypes.Economy.ToString());

                _needAmountRes.Add(res, new TextUIC(economy.Find(res.ToString()).Find("TMP").GetComponent<TextMeshProUGUI>()));
            }
        }
    }
}
