using Chessy.Model;
using Chessy.Model.Entity.View.UI.Down;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct DownToolWeaponUIE
    {
        readonly Dictionary<string, ImageUIC> _toolWeapon;
        readonly Dictionary<ToolsWeaponsWarriorTypes, ButtonUIC> _buttons;
        readonly Dictionary<ToolsWeaponsWarriorTypes, ImageUIC> _images;
        readonly Dictionary<ToolsWeaponsWarriorTypes, TextUIC> _texts;

        public readonly GameObjectVC ParentGOC;
        public readonly CostUIE CostE;

        public ImageUIC LevelImageC(in ToolsWeaponsWarriorTypes tw, in LevelTypes level) => _toolWeapon[tw.ToString() + level];
        public ButtonUIC ButtonC(in ToolsWeaponsWarriorTypes tw) => _buttons[tw];
        public ImageUIC ImageC(in ToolsWeaponsWarriorTypes tw) => _images[tw];
        public TextUIC TextC(in ToolsWeaponsWarriorTypes tw) => _texts[tw];

        public DownToolWeaponUIE(in Transform downZone)
        {
            var gTZone = downZone.Find("GiveTake");

            ParentGOC = new GameObjectVC(gTZone.gameObject);


            _toolWeapon = new Dictionary<string, ImageUIC>();
            _buttons = new Dictionary<ToolsWeaponsWarriorTypes, ButtonUIC>();
            _images = new Dictionary<ToolsWeaponsWarriorTypes, ImageUIC>();
            _texts = new Dictionary<ToolsWeaponsWarriorTypes, TextUIC>();


            var costZone = gTZone.Find("Cost+");

            CostE = new CostUIE(
                costZone.Find("StepsCost_TMP").GetComponent<TextMeshProUGUI>(),
                costZone.Find("WoodCost_TMP").GetComponent<TextMeshProUGUI>(),
                costZone.Find("IronCost_TMP").GetComponent<TextMeshProUGUI>());


            for (var tw = ToolsWeaponsWarriorTypes.None + 1; tw < ToolsWeaponsWarriorTypes.End; tw++)
            {
                var zone = gTZone.Find(tw.ToString());

                _buttons.Add(tw, new ButtonUIC(zone.Find("Button").GetComponent<Button>()));
                _images.Add(tw, new ImageUIC(zone.Find("Back_Image").GetComponent<Image>()));
                _texts.Add(tw, new TextUIC(zone.Find("Amount_TextMP").GetComponent<TextMeshProUGUI>()));

                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    _toolWeapon.Add(tw.ToString() + level, new ImageUIC(zone.Find(tw.ToString()).Find(level + "_Image").GetComponent<Image>()));
                }
            }
        }
    }
}
