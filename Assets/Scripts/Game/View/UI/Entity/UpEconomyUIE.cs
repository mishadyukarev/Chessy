using Chessy.Common;
using Chessy.Common.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct UpEconomyUIE
    {
        readonly Dictionary<ResourceTypes, TextUIC> _economy;
        readonly Dictionary<ResourceTypes, TextUIC> _economyExtract;

        public readonly GameObjectVC ParenGOC;

        public TextUIC Economy(in ResourceTypes res) => _economy[res];
        public TextUIC EconomyExtract(in ResourceTypes res) => _economyExtract[res];


        public UpEconomyUIE(in Transform upZone)
        {
            _economy = new Dictionary<ResourceTypes, TextUIC>();
            _economyExtract = new Dictionary<ResourceTypes, TextUIC>();

            var res = upZone.Find("ResourcesZone");

            ParenGOC = new GameObjectVC(res.gameObject);

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                var resZone = res.Find(resT.ToString());

                _economy.Add(resT, new TextUIC(resZone.Find(resT.ToString() + "_TMP").GetComponent<TextMeshProUGUI>()));


                if (resT != ResourceTypes.Gold && resT != ResourceTypes.Iron)
                {
                    _economyExtract.Add(resT, new TextUIC(resZone.Find(resT.ToString() + "Adding_TMP").GetComponent<TextMeshProUGUI>()));
                }
            }
        }
    }
}