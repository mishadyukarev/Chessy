using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct GetterUnitsViewUICom
    {
        private Dictionary<UnitTypes, Button> _taker_Buttons;
        private Dictionary<UnitTypes, Button> _createUnit_Buttons;
        private Dictionary<UnitTypes, TextMeshProUGUI> _amountUnits_TextMPs;

        internal GetterUnitsViewUICom(GameObject downZone_GO)
        {
            var takeUnitZone_GO = downZone_GO.transform.Find("TakeUnitZone");


            _taker_Buttons = new Dictionary<UnitTypes, Button>();
            _taker_Buttons.Add(UnitTypes.Pawn, takeUnitZone_GO.transform.Find("TakeUnit1Button").GetComponent<Button>());
            _taker_Buttons.Add(UnitTypes.Rook, takeUnitZone_GO.transform.Find("TakeUnit2Button").GetComponent<Button>());
            _taker_Buttons.Add(UnitTypes.Bishop, takeUnitZone_GO.transform.Find("TakeUnit3Button").GetComponent<Button>());


            _createUnit_Buttons = new Dictionary<UnitTypes, Button>();
            _createUnit_Buttons.Add(UnitTypes.Pawn, takeUnitZone_GO.transform.Find("CreatePawn_Button").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Rook, takeUnitZone_GO.transform.Find("CreateRook_Button").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Bishop, takeUnitZone_GO.transform.Find("CreateBishop_Button").GetComponent<Button>());


            _amountUnits_TextMPs = new Dictionary<UnitTypes, TextMeshProUGUI>();
            _amountUnits_TextMPs.Add(UnitTypes.Pawn, _taker_Buttons[UnitTypes.Pawn].transform.Find("AmountPawnZone").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            _amountUnits_TextMPs.Add(UnitTypes.Rook, _taker_Buttons[UnitTypes.Rook].transform.Find("AmountRookZone").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            _amountUnits_TextMPs.Add(UnitTypes.Bishop, _taker_Buttons[UnitTypes.Bishop].transform.Find("AmountBishopZone").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }

        internal void AddListener(UnitTypes unitType, UnityAction unityAction) => _taker_Buttons[unitType].onClick.AddListener(unityAction);
        internal void AddListenerToCreateUnit(UnitTypes unitType, UnityAction unityAction) => _createUnit_Buttons[unitType].onClick.AddListener(unityAction);

        internal void SetActiveButton(UnitTypes unitType, bool isActive) => _taker_Buttons[unitType].gameObject.SetActive(isActive);
        internal void SetActiveCreateButton(UnitTypes unitType, bool isActive) => _createUnit_Buttons[unitType].gameObject.SetActive(isActive);

        internal void SetTextToAmountUnits(UnitTypes unitType, string text) => _amountUnits_TextMPs[unitType].text = text;
    }
}
