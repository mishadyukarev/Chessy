using ECS;

namespace Game.Game
{
    public struct CellUnitStunEs
    {
        static Entity[] _units;

        public static ref C StepsForExitStun<C>(in byte idx) where C : struct, ICellUnitStunE => ref _units[idx].Get<C>();

        public CellUnitStunEs(in EcsWorld gameW)
        {
            _units = new Entity[CellValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _units.Length; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }

    public interface ICellUnitStunE { }
}