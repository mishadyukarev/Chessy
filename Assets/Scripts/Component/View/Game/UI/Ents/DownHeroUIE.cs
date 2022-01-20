using ECS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct DownHeroUIE
    {
        static Entity _ent;

        public static ref GameObjectVC Parent => ref _ent.Get<GameObjectVC>();
        public static ref ButtonUIC ButtonC => ref _ent.Get<ButtonUIC>();
        public static ref TextUIC Cooldown => ref _ent.Get<TextUIC>();

        public DownHeroUIE(in EcsWorld gameW, in Transform gtZone)
        {
            var hero = gtZone.Find("Hero");

            _ent = gameW.NewEntity()
                .Add(new GameObjectVC(hero.gameObject))
                .Add(new ButtonUIC(hero.Find("Button").GetComponent<Button>()))
                .Add(new TextUIC(hero.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));      
        }
    }
}