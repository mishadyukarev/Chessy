using ECS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntDownHero
    {
        static Entity _hero;
        static Entity _cooldown;

        public static ref C Scout<C>() where C : struct => ref _hero.Get<C>();
        public static ref C Cooldown<C>() where C : struct => ref _cooldown.Get<C>();

        public UIEntDownHero(in EcsWorld gameW, in Transform gtZone)
        {
            var button = gtZone.Find(UnitTypes.Elfemale.ToString() + "_Button").GetComponent<Button>();

            _hero = gameW.NewEntity()
                .Add(new ButtonUIC(button));

            _cooldown = gameW.NewEntity()
                .Add(new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));
        }
    }
}