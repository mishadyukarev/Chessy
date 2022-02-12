namespace Game.Game
{
    public readonly struct WhereWorker
    {
        readonly CellEs[] _cellEs;

        public bool TryGetBuilding(in BuildingTypes build, in PlayerTypes owner, out byte idx)
        {
            for (idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (_cellEs[idx].BuildEs.BuildingE.Is(build) && _cellEs[idx].BuildEs.BuildingE.Is(owner))
                {
                    return true;
                }
            }
            return false;
        }

        public int AmountPaws(in PlayerTypes owner)
        {
            var amount = 0;
            for (var idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                if (_cellEs[idx].UnitEs.UnitE.Is(UnitTypes.Pawn) && _cellEs[idx].UnitEs.UnitE.Is(owner))
                {
                    amount++;
                }
            }
            return amount;
        }


        public WhereWorker(in CellEs[] cellEs)
        {
            _cellEs = cellEs;
        }
    }
}