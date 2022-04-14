using Chessy.Game.Model.Component;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity.Cell.Unit
{
    public sealed class UnitMainE
    {
        public UnitTC UnitTC;
        public LevelTC LevelTC;
        public PlayerTC PlayerTC;
        public ConditionUnitTC ConditionTC;
        public IsRightArcherC IsRightArcherC;
        public readonly VisibleC VisibleC;
        public readonly CanSetUnitHereC CanSetUnitHereC;
        public readonly IdxsCellsC ForArson;
        public NeedUpdateViewC NeedUpdateViewC;

        internal UnitMainE()
        {
            VisibleC = new VisibleC(default);
            CanSetUnitHereC = new CanSetUnitHereC(new bool[(byte)PlayerTypes.End]);
            ForArson = new IdxsCellsC(new HashSet<byte>());
        }
    }
}