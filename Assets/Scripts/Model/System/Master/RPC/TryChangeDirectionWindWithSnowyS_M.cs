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
            var canChange = false;

            foreach (var item in _e.IdxsCellsAround(idx_to, DistanceFromCellTypes.First))
            {
                if (_e.IsCenterCloud(item))
                {
                    _e.DirectWindT = _e.DirectionAround(idx_to, item).Invert();
                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (_e.IsCenterCloud(cellIdx))
                        {
                            _e.CloudShiftingC(cellIdx).WhereNeedShiftIdxCell = _e.GetIdxCellByDirect(cellIdx, DistanceFromCellTypes.First, _e.DirectWindT);
                        }
                    }

                    canChange = true;

                    break;
                }
            }


            if (canChange)
            {
                _e.UnitCooldownAbilitiesC(snowyCellFrom_0).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);
            }
        }
    }
}