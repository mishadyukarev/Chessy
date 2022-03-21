//using Chessy.Common;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Chessy.Game
//{
//    public readonly struct CenterUpgradeUIE
//    {
//        readonly Dictionary<ButtonTypes, ButtonUIC> _buttons;

//        public readonly Chessy.Common.Component.GameObjectVC Parent;

//        public ButtonUIC ButtonC(in ButtonTypes buttonT) => _buttons[buttonT];

//        public CenterUpgradeUIE(in Transform centerZone)
//        {
//            var parent = centerZone.Find("PickUpgradeZone");

//            Parent = new Chessy.Common.Component.GameObjectVC(parent.gameObject);


//            _buttons = new Dictionary<ButtonTypes, ButtonUIC>();


//            for (var buttonT = ButtonTypes.None + 1; buttonT <= ButtonTypes.Third; buttonT++)
//            {
//                _buttons.Add(buttonT, new ButtonUIC(parent.Find(buttonT.ToString()).Find("Button").GetComponent<Button>()));
//            }
//        }
//    }
//}