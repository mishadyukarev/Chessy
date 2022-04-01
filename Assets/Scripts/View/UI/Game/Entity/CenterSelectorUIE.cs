using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CenterSelectorUIE
    {
        //readonly Dictionary<CellClickTypes, Chessy.Common.Component.GameObjectVC> _sel;
        //readonly Dictionary<AbilityTypes, Chessy.Common.Component.GameObjectVC> _uniq;

        //public Chessy.Common.Component.GameObjectVC SelectorUI(in CellClickTypes click) => _sel[click];
        //public Chessy.Common.Component.GameObjectVC SelectorUI(in AbilityTypes uniq) => _uniq[uniq];


        //public HashSet<CellClickTypes> KeysClick
        //{
        //    get
        //    {
        //        var keys = new HashSet<CellClickTypes>();
        //        foreach (var item in _sel) keys.Add(item.Key);
        //        return keys;
        //    }
        //}
        //public HashSet<AbilityTypes> KeysUnique
        //{
        //    get
        //    {
        //        var keys = new HashSet<AbilityTypes>();
        //        foreach (var item in _uniq) keys.Add(item.Key);
        //        return keys;
        //    }
        //}

        public CenterSelectorUIE(in Transform centerZone)
        {
            //_sel = new Dictionary<CellClickTypes, Chessy.Common.Component.GameObjectVC>();
            //_uniq = new Dictionary<AbilityTypes, Chessy.Common.Component.GameObjectVC>();


            //var selZone = centerZone.transform.Find("SelectorType");

            //for (var click = CellClickTypes.SimpleClick; click < CellClickTypes.End; click++)
            //{
            //    click = (CellClickTypes)((int)click);
            //    var str = click.ToString();
            //    var go = selZone.Find(str).gameObject;

            //    _sel.Add(click, new Chessy.Common.Component.GameObjectVC(go));


            //    if (click == CellClickTypes.UniqueAbility)
            //    {
            //        _uniq.Add(AbilityTypes.FireArcher, new Chessy.Common.Component.GameObjectVC(go.transform.Find(AbilityTypes.FireArcher.ToString()).gameObject));
            //        _uniq.Add(AbilityTypes.StunElfemale, new Chessy.Common.Component.GameObjectVC(go.transform.Find(AbilityTypes.StunElfemale.ToString()).gameObject));
            //        _uniq.Add(AbilityTypes.ChangeDirectionWind, new Chessy.Common.Component.GameObjectVC(go.transform.Find(AbilityTypes.ChangeDirectionWind.ToString()).gameObject));
            //    }
            //}
        }
    }
}