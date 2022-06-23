using Chessy.Common.Component;
using TMPro;
using UnityEngine;

namespace Chessy.Model
{
    public readonly struct UpEconomyUIE
    {
        readonly TextUIC[] _economy;
        readonly TextUIC[] _economyExtract;

        public readonly GameObjectVC ParenGOC;

        public TextUIC Economy(in ResourceTypes res) => _economy[(byte)res];
        public TextUIC EconomyExtract(in ResourceTypes res) => _economyExtract[(byte)res];


        public UpEconomyUIE(in Transform upZone)
        {
            _economy = new TextUIC[(byte)ResourceTypes.End];
            _economyExtract = new TextUIC[(byte)ResourceTypes.End];

            var res = upZone.Find("ResourcesZone");

            ParenGOC = new GameObjectVC(res.gameObject);

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                var resZone = res.Find(resT.ToString());

                _economy[(byte)resT] = new TextUIC(resZone.Find(resT.ToString() + "_TMP").GetComponent<TextMeshProUGUI>());


                if (resT != ResourceTypes.Gold && resT != ResourceTypes.Iron)
                {
                    _economyExtract[(byte)resT] = new TextUIC(resZone.Find(resT.ToString() + "Adding_TMP").GetComponent<TextMeshProUGUI>());
                }
            }
        }
    }
}