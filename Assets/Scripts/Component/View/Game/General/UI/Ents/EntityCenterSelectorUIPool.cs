using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityCenterSelectorUIPool
    {
        static readonly Dictionary<CellClickTypes, Entity> _sel;
        static readonly Dictionary<UniqueAbilityTypes, Entity> _uniq;

        public static ref C SelectorUI<C>(in CellClickTypes click) where C : struct => ref _sel[click].Get<C>();
        public static ref C SelectorUI<C>(in UniqueAbilityTypes uniq) where C : struct => ref _uniq[uniq].Get<C>();


        static EntityCenterSelectorUIPool()
        {
            _sel = new Dictionary<CellClickTypes, Entity>();
            _uniq = new Dictionary<UniqueAbilityTypes, Entity>();

            for (var click = CellClickTypes.Start; click < CellClickTypes.End; click++) _sel.Add(click, default);
            for (var uniq = UniqueAbilityTypes.Start; uniq < UniqueAbilityTypes.End; uniq++) _uniq.Add(uniq, default);
        }
        public EntityCenterSelectorUIPool(in WorldEcs gameW, in Transform centerZone)
        {
            var selZone = centerZone.transform.Find("SelectorTypeZone");

            for (var click = CellClickTypes.SetUnit; click <= CellClickTypes.UniqAbil; click++)
            {
                click = (CellClickTypes)((int)click);
                var str = click.ToString();
                var go = selZone.Find(str).gameObject;

                _sel[click] = gameW.NewEntity()
                    .Add(new GameObjectVC(go));


                if (click == CellClickTypes.UniqAbil)
                {
                    _uniq[UniqueAbilityTypes.FireArcher] = gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.FireArcher.ToString()).gameObject));

                    _uniq[UniqueAbilityTypes.StunElfemale] = gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.StunElfemale.ToString()).gameObject));

                    _uniq[UniqueAbilityTypes.ChangeDirWind] = gameW.NewEntity()
                        .Add(new GameObjectVC(go.transform.Find(UniqueAbilityTypes.ChangeDirWind.ToString()).gameObject));
                }
            }
        }
    }
}