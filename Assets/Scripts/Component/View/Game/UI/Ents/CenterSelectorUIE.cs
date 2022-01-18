using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterSelectorUIE
    {
        static Dictionary<CellClickTypes, Entity> _sel;
        static Dictionary<UniqueAbilityTypes, Entity> _uniq;

        public static ref C SelectorUI<C>(in CellClickTypes click) where C : struct => ref _sel[click].Get<C>();
        public static ref C SelectorUI<C>(in UniqueAbilityTypes uniq) where C : struct => ref _uniq[uniq].Get<C>();


        public static HashSet<CellClickTypes> KeysClick
        {
            get
            {
                var keys = new HashSet<CellClickTypes>();
                foreach (var item in _sel) keys.Add(item.Key);
                return keys;
            }
        }
        public static HashSet<UniqueAbilityTypes> KeysUnique
        {
            get
            {
                var keys = new HashSet<UniqueAbilityTypes>();
                foreach (var item in _uniq) keys.Add(item.Key);
                return keys;
            }
        }

        public CenterSelectorUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _sel = new Dictionary<CellClickTypes, Entity>();
            _uniq = new Dictionary<UniqueAbilityTypes, Entity>();


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
                    _uniq.Add(UniqueAbilityTypes.FireArcher, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.FireArcher.ToString()).gameObject)));

                    _uniq.Add(UniqueAbilityTypes.StunElfemale, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.StunElfemale.ToString()).gameObject)));

                    _uniq.Add(UniqueAbilityTypes.ChangeDirectionWind, gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.ChangeDirectionWind.ToString()).gameObject)));
                }
            }
        }
    }
}