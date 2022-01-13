using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntBuild
    {
        static readonly Dictionary<ButtonTypes, Entity> _buttons;

        public static ref C Button<C>(in ButtonTypes but) where C : struct => ref _buttons[but].Get<C>();

        static UIEntBuild()
        {
            _buttons = new Dictionary<ButtonTypes, Entity>();
            for (var buildBut = ButtonTypes.Start; buildBut <= ButtonTypes.End; buildBut++)
                _buttons.Add(buildBut, default);
        }
        public UIEntBuild(in EcsWorld gameW, in Transform rightZone)
        {
            var buildZone = rightZone.Find("BuildingZone");


            _buttons[ButtonTypes.First] = gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("BuildingAbilityButton1").GetComponent<Button>()));

            _buttons[ButtonTypes.Second] = gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("BuildingAbilityButton2").GetComponent<Button>()));

            var button = buildZone.Find("BuildingAbilityButton3").GetComponent<Button>();
            _buttons[ButtonTypes.Third] = gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new ImageUIC(button.transform.Find("Image (4)").GetComponent<Image>()));
        }
    }
}
