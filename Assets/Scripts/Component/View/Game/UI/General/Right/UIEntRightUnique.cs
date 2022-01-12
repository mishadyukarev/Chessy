using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntRightUnique
    {
        static readonly Dictionary<UniqueButtonTypes, Entity> _buttons;
        static readonly Dictionary<string, Entity> _zones;


        public static ref C Buttons<C>(in UniqueButtonTypes but) where C : struct => ref _buttons[but].Get<C>();
        public static ref C Zones<C>(in UniqueButtonTypes but, in UniqueAbilityTypes uniq) where C : struct => ref _zones[but.ToString() + uniq].Get<C>();

        static UIEntRightUnique()
        {
            _buttons = new Dictionary<UniqueButtonTypes, Entity>();
            _zones = new Dictionary<string, Entity>();

            for (var uniqBut = UniqueButtonTypes.Start; uniqBut < UniqueButtonTypes.End; uniqBut++)
            {
                _buttons.Add(uniqBut, default);
                for (var uniq = UniqueAbilityTypes.Start; uniq < UniqueAbilityTypes.End; uniq++)
                {
                    _zones.Add(uniqBut.ToString() + uniq, default);
                }
            }   
        }
        public UIEntRightUnique(in EcsWorld gameW, in Transform right)
        {
            var uniqZone = right.transform.Find("UniqueAbilitiesZone");

            for (var uniqBut = UniqueButtonTypes.First; uniqBut < UniqueButtonTypes.End; uniqBut++)
            {
                var button = uniqZone.Find(uniqBut.ToString()).GetComponent<Button>();

                _buttons[uniqBut] = gameW.NewEntity()
                    .Add(new ButtonUIC(button))
                    .Add(new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));


                for (var uniq = UniqueAbilityTypes.First; uniq < UniqueAbilityTypes.End; uniq++)
                {
                    _zones[uniqBut.ToString() + uniq] = gameW.NewEntity()
                        .Add( new GameObjectVC(button.transform.Find(uniq.ToString()).gameObject));
                }
            }
        }
    }
}