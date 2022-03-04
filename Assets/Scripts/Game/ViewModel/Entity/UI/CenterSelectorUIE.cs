using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CenterSelectorUIE
    {
        readonly Dictionary<CellClickTypes, GameObjectVC> _sel;
        readonly Dictionary<AbilityTypes, GameObjectVC> _uniq;

        public GameObjectVC SelectorUI(in CellClickTypes click) => _sel[click];
        public GameObjectVC SelectorUI(in AbilityTypes uniq) => _uniq[uniq];


        public HashSet<CellClickTypes> KeysClick
        {
            get
            {
                var keys = new HashSet<CellClickTypes>();
                foreach (var item in _sel) keys.Add(item.Key);
                return keys;
            }
        }
        public HashSet<AbilityTypes> KeysUnique
        {
            get
            {
                var keys = new HashSet<AbilityTypes>();
                foreach (var item in _uniq) keys.Add(item.Key);
                return keys;
            }
        }

        public CenterSelectorUIE(in Transform centerZone)
        {
            _sel = new Dictionary<CellClickTypes, GameObjectVC>();
            _uniq = new Dictionary<AbilityTypes, GameObjectVC>();


            var selZone = centerZone.transform.Find("SelectorType");

            for (var click = CellClickTypes.SimpleClick; click < CellClickTypes.End; click++)
            {
                click = (CellClickTypes)((int)click);
                var str = click.ToString();
                var go = selZone.Find(str).gameObject;

                _sel.Add(click, new GameObjectVC(go));


                if (click == CellClickTypes.UniqueAbility)
                {
                    _uniq.Add(AbilityTypes.FireArcher, new GameObjectVC(go.transform.Find(AbilityTypes.FireArcher.ToString()).gameObject));
                    _uniq.Add(AbilityTypes.StunElfemale, new GameObjectVC(go.transform.Find(AbilityTypes.StunElfemale.ToString()).gameObject));
                    _uniq.Add(AbilityTypes.ChangeDirectionWind, new GameObjectVC(go.transform.Find(AbilityTypes.ChangeDirectionWind.ToString()).gameObject));
                }
            }
        }
    }
}