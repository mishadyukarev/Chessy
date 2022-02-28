using Chessy.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct UpEconomyUIE
    {
        readonly Dictionary<ResourceTypes, TextUIC> _economy;
        readonly Dictionary<ResourceTypes, TextUIC> _economyExtract;

        public TextUIC Economy(in ResourceTypes res) => _economy[res];
        public TextUIC EconomyExtract(in ResourceTypes res) => _economyExtract[res];

        public UpEconomyUIE(in Transform upZone)
        {
            _economy = new Dictionary<ResourceTypes, TextUIC>();
            _economyExtract = new Dictionary<ResourceTypes, TextUIC>();


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var resZone = upZone.Find("ResourcesZone").Find(res.ToString());

                _economy.Add(res, new TextUIC(resZone.Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>()));


                if (res != ResourceTypes.Gold && res != ResourceTypes.Iron)
                {
                    _economyExtract.Add(res, new TextUIC(resZone.Find(res.ToString() + "Adding_TMP").GetComponent<TextMeshProUGUI>()));
                }
            }
        }
    }
}