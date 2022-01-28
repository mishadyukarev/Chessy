using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterSelectorUIE
    {
        static Dictionary<CellClickTypes, Entity> _sel;
        static Dictionary<AbilityTypes, Entity> _uniq;

        public static ref C SelectorUI<C>(in CellClickTypes click) where C : struct => ref _sel[click].Get<C>();
        public static ref C SelectorUI<C>(in AbilityTypes uniq) where C : struct => ref _uniq[uniq].Get<C>();


        public static HashSet<CellClickTypes> KeysClick
        {
            get
            {
                var keys = new HashSet<CellClickTypes>();
                foreach (var item in _sel) keys.Add(item.Key);
                return keys;
            }
        }
        public static HashSet<AbilityTypes> KeysUnique
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _uniq) keys.Add(item.Key);
                return keys;
            }
        }

        public CenterSelectorUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _sel = new Dictionary<CellClickTypes, Entity>();
            _uniq = new Dictionary<AbilityTypes, Entity>();


            var selZone = centerZone.transform.Find("SelectorType");

            for (var click = CellClickTypes.SimpleClick; click <= CellClickTypes.UniqueAbility; click++)
            {
                click = (CellClickTypes)((int)click);
                var str = click.ToString();
                var go = selZone.Find(str).gameObject;

                _sel.Add(click, gameW.NewEntity()
                    .Add(new GameObjectVC(go)));


                if (click == CellClickTypes.UniqueAbility)
                {
                    _uniq.Add(AbilityTypes.FireArcher, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(AbilityTypes.FireArcher.ToString()).gameObject)));

                    _uniq.Add(AbilityTypes.StunElfemale, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(AbilityTypes.StunElfemale.ToString()).gameObject)));

                    _uniq.Add(AbilityTypes.ChangeDirectionWind, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(AbilityTypes.ChangeDirectionWind.ToString()).gameObject)));
                }
            }
        }
    }
}