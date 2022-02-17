using System.Collections.Generic;

namespace Game.Game
{
    public struct ForPlayerCellUnitE
    {
        public readonly float[] NeedStepsForShift;
        public bool IsVisibleC;

        public IdxsC ForShift;
        readonly IdxsC[] _forAttack;

        public IdxsC ForAttack(in AttackTypes attack) => _forAttack[(byte)attack - 1];

        internal ForPlayerCellUnitE(in bool def) : this()
        {
            ForShift = new IdxsC(new HashSet<byte>());

            NeedStepsForShift = new float[StartValues.ALL_CELLS_AMOUNT];
            _forAttack = new IdxsC[(byte)AttackTypes.End - 1];

            _forAttack[(byte)AttackTypes.Simple - 1] = new IdxsC(new HashSet<byte>());
            _forAttack[(byte)AttackTypes.Unique - 1] = new IdxsC(new HashSet<byte>());
        }
    }
}