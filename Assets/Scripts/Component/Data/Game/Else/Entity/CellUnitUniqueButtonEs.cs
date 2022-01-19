using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitUniqueButtonEs
    {
        static Dictionary<ButtonTypes, Entity[]> _ents;

        public static ref C UniqueAbility<C>(in ButtonTypes button, in byte idx) where C : struct, IUnitUniqueButtonCellE
        {
            if (!_ents.ContainsKey(button)) throw new Exception();
            return ref _ents[button][idx].Get<C>();
        }

        public CellUnitUniqueButtonEs(in EcsWorld gameW)
        {
            _ents = new Dictionary<ButtonTypes, Entity[]>();
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                _ents.Add(button, new Entity[CellStartValues.ALL_CELLS_AMOUNT]);

                for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
                {
                    _ents[button][idx] = gameW.NewEntity()
                        .Add(new UniqueAbilityC());
                }
            }
        }
    }

    public interface IUnitUniqueButtonCellE { }
}