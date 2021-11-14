using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct GetPawnArcherUIC
    {
        private static Dictionary<UnitTypes, Button> _taker_Buttons;
        private static Dictionary<UnitTypes, Button> _createUnit_Buttons;
        private static Dictionary<UnitTypes, TextMeshProUGUI> _amountUnits_TextMPs;

        public GetPawnArcherUIC(Transform takeUnitZone)
        {
            var takeUnitZone_GO = takeUnitZone;


            _taker_Buttons = new Dictionary<UnitTypes, Button>();
            _taker_Buttons.Add(UnitTypes.Pawn, takeUnitZone_GO.transform.Find(UnitTypes.Pawn.ToString()).GetComponent<Button>());
            _taker_Buttons.Add(UnitTypes.Archer, takeUnitZone_GO.transform.Find(UnitTypes.Archer.ToString()).GetComponent<Button>());

            _createUnit_Buttons = new Dictionary<UnitTypes, Button>();
            _createUnit_Buttons.Add(UnitTypes.Pawn, takeUnitZone_GO.transform.Find("CreatePawn_Button").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Archer, takeUnitZone_GO.transform.Find("CreateRook_Button").GetComponent<Button>());

            _amountUnits_TextMPs = new Dictionary<UnitTypes, TextMeshProUGUI>();
            _amountUnits_TextMPs.Add(UnitTypes.Pawn, _taker_Buttons[UnitTypes.Pawn].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            _amountUnits_TextMPs.Add(UnitTypes.Archer, _taker_Buttons[UnitTypes.Archer].transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }

        public static void AddListener(UnitTypes unitType, UnityAction unityAction) => _taker_Buttons[unitType].onClick.AddListener(unityAction);
        public static void AddListenerToCreateUnit(UnitTypes unitType, UnityAction unityAction) => _createUnit_Buttons[unitType].onClick.AddListener(unityAction);

        public static void SetActiveButton(UnitTypes unitType, bool isActive) => _taker_Buttons[unitType].gameObject.SetActive(isActive);
        public static void SetActiveCreateButton(UnitTypes unitType, bool isActive) => _createUnit_Buttons[unitType].gameObject.SetActive(isActive);
        public static void SetTextToAmountUnits(UnitTypes unitType, string text) => _amountUnits_TextMPs[unitType].text = text;
    }
}
