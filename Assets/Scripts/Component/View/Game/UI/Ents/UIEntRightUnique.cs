using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntRightUnique
    {
        static Dictionary<ButtonTypes, Entity> _buttons;
        static Dictionary<string, Entity> _zones;


        public static ref C Buttons<C>(in ButtonTypes but) where C : struct => ref _buttons[but].Get<C>();
        public static ref C Zones<C>(in ButtonTypes but, in UniqueAbilityTypes uniq) where C : struct => ref _zones[but.ToString() + uniq].Get<C>();


        public UIEntRightUnique(in EcsWorld gameW, in Transform right)
        {
            _buttons = new Dictionary<ButtonTypes, Entity>();
            _zones = new Dictionary<string, Entity>();

            var uniqZone = right.transform.Find("UniqueAbilitiesZone");

            for (var uniqBut = ButtonTypes.Start + 1; uniqBut < ButtonTypes.End; uniqBut++)
            {
                var button = uniqZone.Find(uniqBut.ToString()).GetComponent<Button>();

                _buttons.Add(uniqBut, gameW.NewEntity()
                    .Add(new ButtonUIC(button))
                    .Add(new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>())));

                for (var uniq = UniqueAbilityTypes.First; uniq < UniqueAbilityTypes.End; uniq++)
                {
                    _zones.Add(uniqBut.ToString() + uniq, gameW.NewEntity()
                        .Add(new GameObjectVC(button.transform.Find(uniq.ToString()).gameObject)));
                }
            }
        }
    }
}