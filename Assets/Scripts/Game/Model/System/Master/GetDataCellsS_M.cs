using Chessy.Game.Entity.Model;
using Chessy.Game.Values;

namespace Chessy.Game.System.Model
{
    sealed class GetDataCellsS_M : SystemModelGameAbs, IEcsRunSystem
    {
        readonly SystemsModelGame _sMGame;

        internal GetDataCellsS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                e.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _sMGame.CellSs(cell_0).PawnGetExtractAdultForestS.Get();
                _sMGame.CellSs(cell_0).PawnExtractHillS.Get();

                _sMGame.CellSs(cell_0).GetVisibleS.Get();
                _sMGame.CellSs(cell_0).GetEffectsForUnitsS.Get(cell_0);

                _sMGame.CellSs(cell_0).GetDamageUnitsS.Get(cell_0);
                _sMGame.CellSs(cell_0).GetAbilityUnitS.Get(cell_0);

                _sMGame.CellSs(cell_0).GetTrailsVisibleS.Get(cell_0);


                _sMGame.CellSs(cell_0).GetWoodcutterExtractCellsS.Get(cell_0);
                _sMGame.CellSs(cell_0).GetFarmExtractCellsS.Get(cell_0);
                _sMGame.CellSs(cell_0).GetBuildingVisibleS.Get(cell_0);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _sMGame.CellSs(cell_0).GetCellsForShiftUnitS.Get(cell_0);
                _sMGame.CellSs(cell_0).GetAttackMeleeCellsS.Get();
                _sMGame.CellSs(cell_0).GetCellsForAttackArcherS.Get(cell_0);
                _sMGame.CellSs(cell_0).GetCellForArsonArcherS.Get(cell_0);
            }
        }
    }
}