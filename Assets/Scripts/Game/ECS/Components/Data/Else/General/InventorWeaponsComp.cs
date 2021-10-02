﻿//using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
//using Assets.Scripts.Supports;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Component.Data.Else.Game.General
//{
//    internal struct InventorWeaponsComp
//    {
//        private Dictionary<bool, Dictionary<WeaponTypes, byte>> _inventorWeapons;

//        internal InventorWeaponsComp(Dictionary<bool, Dictionary<WeaponTypes, byte>> inventorWeapons)
//        {
//            _inventorWeapons = inventorWeapons;
//            for (byte i = 0; i < 2; i++)
//            {
//                var isMaster = true;
//                if (i == 1) isMaster = false;

//                var dict = new Dictionary<WeaponTypes, byte>();
//                for (WeaponTypes weaponType = 0; weaponType < (WeaponTypes)Enum.GetNames(typeof(WeaponTypes)).Length; weaponType++)
//                {
//                    dict.Add(weaponType, default);
//                }
//                _inventorWeapons.Add(isMaster, dict);
//            }
//        }

//        internal void SetAmountWeapons(bool key, WeaponTypes weaponType, byte value) => _inventorWeapons[key][weaponType] = value;
//        internal byte GetAmountWeapons(bool key, WeaponTypes weaponType) => _inventorWeapons[key][weaponType];

//        internal void AddAmountWeapons(bool key, WeaponTypes weaponType, byte adding = 1) => _inventorWeapons[key][weaponType] += adding;
//        internal void AddAmountWeapons(bool key, ToolWeaponTypes toolWeaponType, byte adding = 1) => _inventorWeapons[key][toolWeaponType.TransInWeapon()] += adding;
//        internal void TakeAmountWeapons(bool key, WeaponTypes weaponType, byte taking = 1) => _inventorWeapons[key][weaponType] -= taking;
//        internal void TakeAmountWeapons(bool key, ToolWeaponTypes toolWeaponType, byte taking = 1) => _inventorWeapons[key][toolWeaponType.TransInWeapon()] -= taking;

//        internal bool HaveWeapon(bool key, WeaponTypes weaponType) => _inventorWeapons[key][weaponType] > 0;
//        internal bool HaveWeapon(bool key, ToolWeaponTypes toolWeaponType) => _inventorWeapons[key][toolWeaponType.TransInWeapon()] > 0;
//    }
//}