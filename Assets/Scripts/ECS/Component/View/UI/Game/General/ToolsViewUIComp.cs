using Assets.Scripts.Abstractions.Enums.Cell;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct ToolsViewUIComp
    {
        private Dictionary<PawnExtraToolTypes, TextMeshProUGUI> _pawnExtraTool_TextMPs;
        private TextMeshProUGUI _rookOrBishopCrossbow_TextMP;

        internal ToolsViewUIComp(GameObject upZone_GO)
        {
            var toolsZone_GO = upZone_GO.transform.Find("ToolsZone").gameObject;

            _pawnExtraTool_TextMPs = new Dictionary<PawnExtraToolTypes, TextMeshProUGUI>();
            _pawnExtraTool_TextMPs.Add(PawnExtraToolTypes.Pick, toolsZone_GO.transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
            _pawnExtraTool_TextMPs.Add(PawnExtraToolTypes.Sword, toolsZone_GO.transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());

            _rookOrBishopCrossbow_TextMP = toolsZone_GO.transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>();
        }

        internal void SetTextPawnExtraTool(PawnExtraToolTypes pawnExtraToolType, string text) => _pawnExtraTool_TextMPs[pawnExtraToolType].text = text;
    }
}
