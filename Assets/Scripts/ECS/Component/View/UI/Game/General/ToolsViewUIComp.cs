using Assets.Scripts.Abstractions.Enums.Cell;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct ToolsViewUIComp
    {
        private TextMeshProUGUI _amountPicks_TextMP;
        private TextMeshProUGUI _amountSwords_TextMP;
        //private TextMeshProUGUI _rookOrBishopCrossbow_TextMP;

        internal string TextAmountPicks
        {
            get => _amountPicks_TextMP.text;
            set => _amountPicks_TextMP.text = value;
        }

        internal string TextAmountSwords
        {
            get => _amountSwords_TextMP.text;
            set => _amountSwords_TextMP.text = value;
        }

        internal ToolsViewUIComp(GameObject upZone_GO)
        {
            var toolsZone_GO = upZone_GO.transform.Find("ToolsZone").gameObject;

            _amountPicks_TextMP = toolsZone_GO.transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>();
            _amountSwords_TextMP = toolsZone_GO.transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>();

            //_rookOrBishopCrossbow_TextMP = toolsZone_GO.transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>();
        }
    }
}
