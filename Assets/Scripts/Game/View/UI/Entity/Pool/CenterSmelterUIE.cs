using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct CenterSmelterUIE
    {
        readonly Dictionary<ResourceTypes, TextUIC> _texts;

        public readonly Chessy.Common.Component.GameObjectVC Zone;
        public readonly ButtonUIC ButtonC;
        public readonly ButtonUIC ExitButtonC;

        public TextUIC TextC(in ResourceTypes resT) => _texts[resT];

        internal CenterSmelterUIE(in Transform leftZone)
        {
            _texts = new Dictionary<ResourceTypes, TextUIC>();

            var zone = leftZone.Find("Smelter+");

            var toggler = zone.Find("Toggler+");

            var button = toggler.Find("Button+").GetComponent<Button>();

            Zone = new Chessy.Common.Component.GameObjectVC(zone.gameObject);
            ButtonC = new ButtonUIC(button);
            ExitButtonC = new ButtonUIC(zone.Find("Exit").Find("Button").GetComponent<Button>());

            for (var resT = ResourceTypes.Wood; resT < ResourceTypes.End; resT++)
            {
                _texts.Add(resT, new TextUIC(toggler.Find("Text" + resT.ToString() + "_TMP+").GetComponent<TextMeshProUGUI>()));
            }
        }
    }
}