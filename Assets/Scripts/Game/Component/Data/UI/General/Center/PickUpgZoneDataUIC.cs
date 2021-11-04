using System.Collections.Generic;

namespace Scripts.Game
{
    public struct PickUpgZoneDataUIC
    {
        private static Dictionary<PlayerTypes, bool> _activatedZone;
        private static Dictionary<PlayerTypes, Dictionary<PickUpgradeTypes, bool>> _activated_Buts;

        public static Dictionary<PlayerTypes, Dictionary<PickUpgradeTypes, bool>> Activated_Buts
        {
            get
            {
                var dict_0 = new Dictionary<PlayerTypes, Dictionary<PickUpgradeTypes, bool>>();

                foreach (var item_0 in _activated_Buts)
                {
                    dict_0.Add(item_0.Key, new Dictionary<PickUpgradeTypes, bool>());

                    if (item_0.Key != default)
                        foreach (var item_1 in item_0.Value)
                        {
                            if (item_1.Key != default)
                                dict_0[item_0.Key][item_1.Key] = _activated_Buts[item_0.Key][item_1.Key];
                        }
                }

                return dict_0;
            }
        }

        public PickUpgZoneDataUIC(Dictionary<PlayerTypes, bool> activatedZone)
        {
            _activatedZone = activatedZone;
            _activated_Buts = new Dictionary<PlayerTypes, Dictionary<PickUpgradeTypes, bool>>();

            for (PlayerTypes player = 0; player < (PlayerTypes)typeof(PlayerTypes).GetEnumNames().Length; player++)
            {
                _activatedZone.Add(player, false);
                _activated_Buts.Add(player, new Dictionary<PickUpgradeTypes, bool>());

                foreach (var upgBut in typeof(PickUpgradeTypes).GetEnumValues())
                {
                    _activated_Buts[player].Add((PickUpgradeTypes)upgBut, true);
                }
            }


        }

        public static void SetActiveParent(PlayerTypes player, bool isActivated) => _activatedZone[player] = isActivated;
        public static bool IsActivated(PlayerTypes player) => _activatedZone[player];

        public static void SetActive_But(PlayerTypes player, PickUpgradeTypes pickUpg, bool isActivated) => _activated_Buts[player][pickUpg] = isActivated;

        //public static Dictionary<PickUpgradeTypes, bool> Activated_Buts(PlayerTypes player)
        //{
        //    var dict = new Dictionary<PickUpgradeTypes, bool>();
        //    foreach (var item in _activated_Buts[player])
        //    {
        //        if (item.Key != PickUpgradeTypes.None) dict.Add(item.Key, item.Value);
        //    }
        //    return dict;
        //}
    }
}