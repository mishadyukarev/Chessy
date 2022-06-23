using Chessy.Common.Component;
using Chessy.Model.Entity.View.UI.Down;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model
{
    public readonly struct DownToolWeaponUIE
    {
        readonly Dictionary<string, ImageUIC> _toolWeapon;
        readonly Dictionary<ToolWeaponTypes, ButtonUIC> _buttons;
        readonly Dictionary<ToolWeaponTypes, ImageUIC> _images;
        readonly Dictionary<ToolWeaponTypes, TextUIC> _texts;

        public readonly GameObjectVC ParentGOC;
        public readonly CostUIE CostE;

        public ImageUIC LevelImageC(in ToolWeaponTypes tw, in LevelTypes level) => _toolWeapon[tw.ToString() + level];
        public ButtonUIC ButtonC(in ToolWeaponTypes tw) => _buttons[tw];
        public ImageUIC ImageC(in ToolWeaponTypes tw) => _images[tw];
        public TextUIC TextC(in ToolWeaponTypes tw) => _texts[tw];

        public DownToolWeaponUIE(in Transform downZone)
        {
            var gTZone = downZone.Find("GiveTake");

            ParentGOC = new GameObjectVC(gTZone.gameObject);


            _toolWeapon = new Dictionary<string, ImageUIC>();
            _buttons = new Dictionary<ToolWeaponTypes, ButtonUIC>();
            _images = new Dictionary<ToolWeaponTypes, ImageUIC>();
            _texts = new Dictionary<ToolWeaponTypes, TextUIC>();


            var costZone = gTZone.Find("Cost+");

            CostE = new CostUIE(
                costZone.Find("StepsCost_TMP").GetComponent<TextMeshProUGUI>(),
                costZone.Find("WoodCost_TMP").GetComponent<TextMeshProUGUI>(),
                costZone.Find("IronCost_TMP").GetComponent<TextMeshProUGUI>());


            for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
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
