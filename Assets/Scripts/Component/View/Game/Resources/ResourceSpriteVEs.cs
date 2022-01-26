using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct ResourceSpriteVEs
    {
        static Dictionary<bool, ResourceSpriteVE> _cells;
        static Dictionary<string, ResourceSpriteVE> _units;
        static Dictionary<string, ResourceSpriteVE> _archers;
        static Dictionary<string, ResourceSpriteVE> _toolWeapons;
        static Dictionary<UniqueAbilityTypes, ResourceSpriteVE> _abilities;
        static Dictionary<BuildingTypes, ResourceSpriteVE> _buildings;
        static Dictionary<BuildingTypes, ResourceSpriteVE> _buildingsBack;

        public static ResourceSpriteVE Sprite(in bool isWhite) => _cells[isWhite];
        public static ResourceSpriteVE Sprite(in UnitTypes unit, in LevelTypes level) => _units[unit.ToString() + level];
        public static ResourceSpriteVE Sprite(in bool isRook, in LevelTypes level) => _archers[isRook.ToString() + level];
        public static ResourceSpriteVE Sprite(in ToolWeaponTypes tw, in LevelTypes level) => _toolWeapons[tw.ToString() + level];
        public static ResourceSpriteVE Sprite(in UniqueAbilityTypes ability) => _abilities[ability];
        public static ResourceSpriteVE Sprite(in BuildingTypes build) => _buildings[build];
        public static ResourceSpriteVE SpriteBack(in BuildingTypes build) => _buildingsBack[build];


        public ResourceSpriteVEs(in EcsWorld gameW)
        {
            _cells = new Dictionary<bool, ResourceSpriteVE>();
            _cells.Add(false, new ResourceSpriteVE(gameW, "Black_Sprite"));
            _cells.Add(true, new ResourceSpriteVE(gameW, "White_Sprite"));


            var spriteName = "_Sprite";

            var folder = "Unit/";
            _units = new Dictionary<string, ResourceSpriteVE>();
            _archers = new Dictionary<string, ResourceSpriteVE>();

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    if (unit == UnitTypes.Archer)
                    {
                        _archers.Add(true.ToString() + level, new ResourceSpriteVE(gameW, folder + "Rook" + "_" + (int)level + spriteName));
                        _archers.Add(false.ToString() + level, new ResourceSpriteVE(gameW, folder + "Bishop" + "_" + (int)level + spriteName));

                    }
                    else
                    {
                        _units.Add(unit.ToString() + level, new ResourceSpriteVE(gameW, folder + unit + "_" + (int)level + spriteName));
                    }
                }
            }


            folder = "ToolWeapon/";
            _toolWeapons = new Dictionary<string, ResourceSpriteVE>();
            for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    _toolWeapons.Add(tw.ToString() + level, new ResourceSpriteVE(gameW, folder + tw + "_" + (int)level + spriteName));
                }
            }


            folder = "Unique/";
            _abilities = new Dictionary<UniqueAbilityTypes, ResourceSpriteVE>();

            for (var unique = UniqueAbilityTypes.None + 1; unique < UniqueAbilityTypes.End; unique++)
            {
                _abilities.Add(unique, new ResourceSpriteVE(gameW, folder + unique + spriteName));
            }


            folder = "Building/";
            _buildings = new Dictionary<BuildingTypes, ResourceSpriteVE>();
            _buildingsBack = new Dictionary<BuildingTypes, ResourceSpriteVE>();

            for (var building = BuildingTypes.None + 1; building < BuildingTypes.End; building++)
            {
                _buildings.Add(building, new ResourceSpriteVE(gameW, folder + building + spriteName));
                _buildingsBack.Add(building, new ResourceSpriteVE(gameW, folder + building + "Back" + spriteName));
            }
        }
    }
}