using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct MotionsDataUIC
    {
        //private static Dictionary<PlayerTypes, bool> _isActivatedUI;
        public static bool IsActivated { get; set; }
        public static int AmountMotions { get; set; }


        //public static Dictionary<PlayerTypes, bool> IsActivatedUI
        //{
        //    get
        //    {
        //        var dict = new Dictionary<PlayerTypes, bool>();

        //        foreach (var item_0 in _isActivatedUI)
        //        {
        //            dict.Add(item_0.Key, item_0.Value);
        //        }

        //        return dict;
        //    }
        //}

        public MotionsDataUIC(int amountMotions)
        {
            AmountMotions = amountMotions;

            //_isActivatedUI = new Dictionary<PlayerTypes, bool>();
            //for (var player = (PlayerTypes)1; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
            //{
            //    _isActivatedUI.Add(player, false);
            //}
        }

        //public static void Set(PlayerTypes player, bool isActive)
        //{
        //    if (!_isActivatedUI.ContainsKey(player)) throw new Exception();

        //    _isActivatedUI[player] = isActive;
        //}
        //public static void Sync(PlayerTypes player, bool isActive)
        //{
        //    if (!_isActivatedUI.ContainsKey(player)) throw new Exception();

        //    _isActivatedUI[player] = isActive;
        //}
    }
}
