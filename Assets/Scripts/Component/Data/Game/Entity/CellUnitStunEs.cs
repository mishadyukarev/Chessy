using ECS;

namespace Game.Game
{
    public struct CellUnitStunEs
    {
        static Entity[] _units;

        public static ref AmountC ForExitStun(in byte idx)=> ref _units[idx].Get<AmountC>();

        public CellUnitStunEs(in EcsWorld gameW)
        {
            _units = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _units.Length; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }
    }
}