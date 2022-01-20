using ECS;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitBuildingButtonEs
    {
        static Dictionary<ButtonTypes, Entity[]> _buildButtonUnits;
        public static ref C UnitBuildButton<C>(in ButtonTypes button, in byte idx) where C : struct
        {
            if (!_buildButtonUnits.ContainsKey(button)) throw new Exception();
            return ref _buildButtonUnits[button][idx].Get<C>();
        }

        public CellUnitBuildingButtonEs(in EcsWorld gameW)
        {
            _buildButtonUnits = new Dictionary<ButtonTypes, Entity[]>();
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                _buildButtonUnits.Add(button, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _buildButtonUnits[button][idx] = gameW.NewEntity()
                        .Add(new BuildingTC());
                }
            }
        }
    }
}