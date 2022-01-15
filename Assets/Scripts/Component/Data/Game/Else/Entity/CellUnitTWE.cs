using ECS;

namespace Game.Game
{
    public struct CellUnitTWE
    {
        static Entity[] _unitTWs;

        public static ref T UnitTW<T>(in byte idx) where T : struct, ITWCellE => ref _unitTWs[idx].Get<T>();

        public CellUnitTWE(in EcsWorld gameW)
        {
            _unitTWs = new Entity[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _unitTWs[idx] = gameW.NewEntity()
                    .Add(new UnitTWCellEC(idx))
                    .Add(new ToolWeaponC())
                    .Add(new LevelTC())
                    .Add(new ShieldEC(idx))
                    .Add(new ProtectionC());
            }
        }
    }
}