using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General
{
    internal struct InventorWeaponsComp
    {
        private Dictionary<bool, Dictionary<WeaponTypes, byte>> _inventorWeapons;

        internal InventorWeaponsComp(Dictionary<bool, Dictionary<WeaponTypes, byte>> inventorWeapons)
        {
            _inventorWeapons = inventorWeapons;
            for (byte i = 0; i < 2; i++)
            {
                var isMaster = true;
                if (i == 1) isMaster = false;

                var dict = new Dictionary<WeaponTypes, byte>();
                for (WeaponTypes weaponType = 0; weaponType < (WeaponTypes)Enum.GetNames(typeof(WeaponTypes)).Length; weaponType++)
                {
                    dict.Add(weaponType, default);
                }
                _inventorWeapons.Add(isMaster, dict);
            }
        }

        internal void SetAmountTools(bool key, WeaponTypes weaponType, byte value) => _inventorWeapons[key][weaponType] = value;
        internal byte GetAmountWeapons(bool key, WeaponTypes weaponType) => _inventorWeapons[key][weaponType];

        internal void AddAmountTools(bool key, WeaponTypes weaponType, byte adding = 1) => _inventorWeapons[key][weaponType] += adding;
        internal void TakeAmountTools(bool key, WeaponTypes weaponType, byte taking = 1) => _inventorWeapons[key][weaponType] -= taking;


        internal bool HaveTool(bool key, WeaponTypes weaponType) => _inventorWeapons[key][weaponType] > 0;
    }
}
