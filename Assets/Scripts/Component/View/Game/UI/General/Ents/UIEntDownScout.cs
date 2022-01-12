using ECS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntDownScout
    {
        static Entity _scout;
        static Entity _cooldown;

        public static ref C Scout<C>() where C : struct => ref _scout.Get<C>();
        public static ref C Cooldown<C>() where C : struct => ref _cooldown.Get<C>();

        public UIEntDownScout(in EcsWorld gameW, in Transform gtZone)
        {
            var button = gtZone.Find(UnitTypes.Scout.ToString() + "_Button").GetComponent<Button>();

            _scout = gameW.NewEntity()
                .Add(new ButtonUIC(button));

            _cooldown = gameW.NewEntity()
                .Add(new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));
        }
    }
}