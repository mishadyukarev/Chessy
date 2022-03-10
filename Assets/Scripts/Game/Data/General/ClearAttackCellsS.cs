namespace Chessy.Game.System.Model
{
    sealed class ClearAttackCellsS : SystemAbstract, IEcsRunSystem
    {
        internal ClearAttackCellsS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Clear();
                E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Clear();
            }
        }
    }
}