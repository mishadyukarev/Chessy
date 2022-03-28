using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class GetAttackMeleeCellsS : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        internal GetAttackMeleeCellsS(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        internal void Get()
        {
            _cellEs.UnitEs.SimpleAttack.Clear();
            _cellEs.UnitEs.UniqueAttack.Clear();

            if (!_cellEs.UnitEs.EffectsE.StunC.IsStunned)
            {
                if (_cellEs.UnitEs.MainE.UnitTC.HaveUnit && _cellEs.UnitEs.MainE.UnitTC.IsMelee(_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon) && !_cellEs.UnitEs.MainE.UnitTC.IsAnimal)
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = _cellEs.AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!eMGame.MountainC(idx_1).HaveAnyResources)
                        {
                            var haveMaxSteps = _cellEs.UnitStatsE.StepC.Steps >= StepValues.MAX;

                            if (_cellEs.UnitStatsE.StepC.Steps >= _cellEs.UnitEs.NeedSteps(idx_1).Steps || haveMaxSteps)
                            {
                                if (eMGame.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!eMGame.UnitPlayerTC(idx_1).Is(_cellEs.UnitPlayerTC.Player))
                                    {
                                        if (_cellEs.UnitTC.Is(UnitTypes.Pawn))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                           || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                _cellEs.UnitEs.SimpleAttack.Add(idx_1);
                                            }
                                            else _cellEs.UnitEs.UniqueAttack.Add(idx_1);
                                        }
                                        else
                                        {
                                            _cellEs.UnitEs.SimpleAttack.Add(idx_1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
