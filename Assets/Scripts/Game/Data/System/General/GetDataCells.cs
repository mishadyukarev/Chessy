namespace Chessy.Game.System.Model
{
    public struct GetDataCells
    {
        public GetDataCells(in EntitiesModel e)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                new PawnGetExtractAdultForestS(idx_0, e);
                new PawnExtractHillS(idx_0, e);

                new GetVisibleUnitS(idx_0, e);
                new GetEffectsForUnitsS(idx_0, e);
                new GetDamageUnitsS(idx_0, e);
                new GetAbilityUnitS(idx_0, e);

                new GetTrailsVisibleS(idx_0, e);


                new GetWoodcutterExtractCellsS(idx_0, e);
                new GetFarmExtractCellsS(idx_0, e);
                new GetBuildingVisibleS(idx_0, e);
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                new GetCellsForShiftUnitS(idx_0, e);
                new GetAttackMeleeCellsS(idx_0, e);
                new GetCellsForAttackArcherS(idx_0, e);
                new GetCellForArsonArcherS(idx_0, e);
            }
        }
    }
}