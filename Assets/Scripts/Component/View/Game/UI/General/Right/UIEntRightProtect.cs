using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct UIEntRightProtect
    {
        static Entity _button;
        static readonly Dictionary<UnitTypes, Entity> _zones;

        public static ref C Button<C>() where C : struct => ref _button.Get<C>();
        public static ref C Button<C>(in UnitTypes unit) where C : struct => ref _zones[unit].Get<C>();

        static UIEntRightProtect()
        {
            _zones = new Dictionary<UnitTypes, Entity>();
            for (var unit = UnitTypes.Start; unit <= UnitTypes.End; unit++)
                _zones.Add(unit, default);
        }
        public UIEntRightProtect(in EcsWorld gameW, in Transform condZone)
        {
            var button = condZone.Find("StandartAbilityButton1").GetComponent<Button>();

            _button = gameW.NewEntity()
                .Add(new ButtonUIC(button))
                .Add(new ImageUIC(button.transform.Find("Image").GetComponent<Image>()));



            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                _zones[unit] = gameW.NewEntity()
                    .Add(new GameObjectVC(button.transform.Find(unit.ToString()).gameObject));
            }
        }
    }
}