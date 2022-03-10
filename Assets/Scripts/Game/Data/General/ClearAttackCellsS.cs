namespace Chessy.Game.System.Model
{
    sealed class ClearAttackCellsS : CellSystem, IEcsRunSystem
    {
        internal ClearAttackCellsS(in byte idx, in EntitiesModel eM) : base(idx, eM) { }

        public void Run()
        {
            E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Clear();
            E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Clear();
        }
    }
}