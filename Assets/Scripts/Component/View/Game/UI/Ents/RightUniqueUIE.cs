using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct RightUniqueUIE
    {
        static Dictionary<ButtonTypes, Entity> _buttons;
        static Dictionary<string, Entity> _zones;


        public static ref GameObjectVC Paren(in ButtonTypes but) => ref _buttons[but].Get<GameObjectVC>();
        public static ref ButtonUIC Button(in ButtonTypes but) => ref _buttons[but].Get<ButtonUIC>();
        public static ref TextUIC Text(in ButtonTypes but) => ref _buttons[but].Get<TextUIC>();
        public static ref GameObjectVC Zone(in ButtonTypes but, in UniqueAbilityTypes uniq) => ref _zones[but.ToString() + uniq].Get<GameObjectVC>();


        public RightUniqueUIE(in EcsWorld gameW, in Transform right)
        {
            _buttons = new Dictionary<ButtonTypes, Entity>();
            _zones = new Dictionary<string, Entity>();

            var uniqZone = right.transform.Find("UniqueAbilitiesZone");

            for (var uniqBut = ButtonTypes.Start + 1; uniqBut < ButtonTypes.End; uniqBut++)
            {
                var button = uniqZone.Find(uniqBut.ToString());

                _buttons.Add(uniqBut, gameW.NewEntity()
                    .Add(new GameObjectVC(button.gameObject))
                    .Add(new ButtonUIC(button.Find("Button").GetComponent<Button>()))
                    .Add(new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>())));

                for (var unique = UniqueAbilityTypes.None + 1; unique < UniqueAbilityTypes.End; unique++)
                {
                    _zones.Add(uniqBut.ToString() + unique, gameW.NewEntity()
                        .Add(new GameObjectVC(button.transform.Find(unique.ToString()).gameObject)));
                }
            }
        }
    }
}