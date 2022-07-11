using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryChangeDirectWindWithSnowyM(in byte snowyCellFrom_0, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            foreach (var item in _e.IdxsCellsAround(idx_to, DistanceFromCellTypes.First))
            {
                if (_e.IsCenterCloud(item))
                {
                    _e.DirectWindT = _e.DirectionAround(idx_to, item).Invert();
                    break;
                }
            }

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                if (_e.IsCenterCloud(cellIdx))
                {
                    _e.CloudShiftingC(cellIdx).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cellIdx, DistanceFromCellTypes.First, _e.DirectWindT);
                }
            }


            //for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            //{
            //    if (_e.IsCenterCloud(curCellIdx))
            //    {
            //        _e.DirectWindT = _e.DirectionAround(curCellIdx, idx_to);
            //    }
            //}


            _e.UnitCooldownAbilitiesC(snowyCellFrom_0).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

            _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);

            if (_e.LessonT == LessonTypes.ChangeDirectionWind)
            {
                _s.SetNextLesson();
            }
        }
    }
}