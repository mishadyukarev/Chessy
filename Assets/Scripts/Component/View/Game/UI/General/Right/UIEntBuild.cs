using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntBuild
    {
        static readonly Dictionary<BuildButtonTypes, Entity> _buttons;

        public static ref C Button<C>(in BuildButtonTypes but) where C : struct => ref _buttons[but].Get<C>();

        static UIEntBuild()
        {
            _buttons = new Dictionary<BuildButtonTypes, Entity>();
            for (var buildBut = BuildButtonTypes.Start; buildBut <= BuildButtonTypes.End; buildBut++)
                _buttons.Add(buildBut, default);
        }
        public UIEntBuild(in EcsWorld gameW, in Transform rightZone)
        {
            var buildZone = rightZone.Find("BuildingZone");


            _buttons[BuildButtonTypes.First] = gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("BuildingAbilityButton1").GetComponent<Button>()));

            _buttons[BuildButtonTypes.Second] = gameW.NewEntity()
                .Add(new ButtonUIC(buildZone.Find("BuildingAbilityButton2").GetComponent<Button>()));

            var button = buildZone.Find("BuildingAbilityButton3").GetComponent<Button>();
            _buttons[BuildButtonTypes.Third] = gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new ImageUIC(button.transform.Find("Image (4)").GetComponent<Image>()));
        }
    }
}
