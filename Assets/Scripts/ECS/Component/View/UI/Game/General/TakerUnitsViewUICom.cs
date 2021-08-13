using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct TakerUnitsViewUICom
    {
        private Dictionary<UnitTypes, Button> _taker_Buttons;

        private Dictionary<UnitTypes, Button> _createUnit_Buttons;

        internal TakerUnitsViewUICom(GameObject downZone_GO)
        {
            _taker_Buttons = new Dictionary<UnitTypes, Button>();

            var takeUnitZone_GO = downZone_GO.transform.Find("TakeUnitZone");

            for (UnitTypes unitType = 0; unitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; unitType++)
            {
                if (unitType == UnitTypes.King)
                {
                    _taker_Buttons.Add(unitType, takeUnitZone_GO.transform.Find("TakeUnit0Button").GetComponent<Button>());
                }

                else if (unitType == UnitTypes.Pawn_Axe)
                {
                    _taker_Buttons.Add(unitType, takeUnitZone_GO.transform.Find("TakeUnit1Button").GetComponent<Button>());
                }

                else if (unitType == UnitTypes.Rook_Bow)
                {
                    _taker_Buttons.Add(unitType, takeUnitZone_GO.transform.Find("TakeUnit2Button").GetComponent<Button>());
                }

                else if (unitType == UnitTypes.Bishop_Bow)
                {
                    _taker_Buttons.Add(unitType, takeUnitZone_GO.transform.Find("TakeUnit3Button").GetComponent<Button>());
                }
            }

            _createUnit_Buttons = new Dictionary<UnitTypes, Button>();
            _createUnit_Buttons.Add(UnitTypes.Pawn_Axe, takeUnitZone_GO.transform.Find("CreatePawn_Button").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Rook_Bow, takeUnitZone_GO.transform.Find("CreateRook_Button").GetComponent<Button>());
            _createUnit_Buttons.Add(UnitTypes.Bishop_Bow, takeUnitZone_GO.transform.Find("CreateBishop_Button").GetComponent<Button>());
        }

        internal void AddListener(UnitTypes unitType, UnityAction unityAction) => _taker_Buttons[unitType].onClick.AddListener(unityAction);
        internal void AddListenerToCreateUnit(UnitTypes unitType, UnityAction unityAction) => _createUnit_Buttons[unitType].onClick.AddListener(unityAction);
        internal void SetColorButton(UnitTypes unitType, Color color) => _taker_Buttons[unitType].image.color = color;
        internal void SetActiveButton(UnitTypes unitType, bool isActive) => _taker_Buttons[unitType].gameObject.SetActive(isActive);

        internal void SetActiveCreateButton(UnitTypes unitType, bool isActive) => _createUnit_Buttons[unitType].gameObject.SetActive(isActive);
    }
}
