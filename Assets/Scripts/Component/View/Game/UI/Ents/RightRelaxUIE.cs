using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct RightRelaxUIE
    {
        static Entity _button;
        static Dictionary<UnitTypes, Entity> _zones;

        public static ref C Button<C>() where C : struct => ref _button.Get<C>();
        public static ref C Button<C>(in UnitTypes unit) where C : struct => ref _zones[unit].Get<C>();

        public RightRelaxUIE(in EcsWorld gameW, in Transform condZone)
        {
            _zones = new Dictionary<UnitTypes, Entity>();

            var button = condZone.Find("StandartAbilityButton2").GetComponent<Button>();

            _button = gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new ImageUIC(button.transform.Find("Image").GetComponent<Image>()));

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                _zones.Add(unit, gameW.NewEntity()
                   .Add(new GameObjectVC(button.transform.Find(unit.ToString()).gameObject)));
            }
        }
    }
}