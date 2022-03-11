namespace Chessy.Game.System.Model
{
    public struct GetDataUnitOnCellS
    {
        public GetDataUnitOnCellS(in byte idx_0, in EntitiesModel e)
        {
            new PawnGetExtractAdultForestS(idx_0, e);
            new PawnExtractHillS(idx_0, e);

            new GetVisibleUnitS(idx_0, e);
            new GetEffectsForUnitsS(idx_0, e);
            new GetDamageUnitsS(idx_0, e);
            new GetAbilityUnitS(idx_0, e);

            new GetCellsForShiftUnitS(idx_0, e);
            new GetAttackMeleeCellsS(idx_0, e);
            new GetCellsForAttackArcherS(idx_0, e);
            new GetCellForArsonArcherS(idx_0, e);
        }
    }
}