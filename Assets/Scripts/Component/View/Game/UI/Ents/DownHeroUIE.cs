using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct DownHeroUIE
    {
        static Entity _ent;
        static Dictionary<UnitTypes, Entity> _units;

        public static ref GameObjectVC Parent => ref _ent.Get<GameObjectVC>();
        public static ref ButtonUIC ButtonC => ref _ent.Get<ButtonUIC>();
        public static ref ImageUIC Image(in UnitTypes unit) => ref _units[unit].Get<ImageUIC>();
        public static ref TextUIC Cooldown => ref _ent.Get<TextUIC>();

        public DownHeroUIE(in EcsWorld gameW, in Transform down)
        {
            var hero = down.Find("Hero");

            _ent = gameW.NewEntity()
                .Add(new GameObjectVC(hero.gameObject))
                .Add(new ButtonUIC(hero.Find("Button").GetComponent<Button>()))
                .Add(new TextUIC(hero.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));



            _units = new Dictionary<UnitTypes, Entity>();
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Skeleton; unit++)
            {
                _units.Add(unit, gameW.NewEntity()
                    .Add(new ImageUIC(hero.Find(unit.ToString()).GetComponent<Image>())));
            }
        }
    }
}