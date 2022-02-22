using ECS;

namespace Game.Game
{
    public abstract class CellAbstractEs
    {
        protected Entity[] Cells;

        public CellAbstractEs(in EcsWorld gameW)
        {
            Cells = new Entity[Start_Values.ALL_CELLS_AMOUNT];
            for (byte idx = 0; idx < Cells.Length; idx++) Cells[idx] = gameW.NewEntity();
        }
    }
}