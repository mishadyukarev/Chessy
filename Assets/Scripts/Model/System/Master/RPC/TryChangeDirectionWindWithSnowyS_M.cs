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

            foreach (var item in _e.IdxsCellsAround(idx_to))
            {
                if (_e.IsCenterCloud(item))
                {
                    _windC.DirectT = _e.DirectionAround(idx_to, item).Invert();
                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (_e.IsCenterCloud(cellIdx))
                        {
                            _e.CloudShiftingC(cellIdx).WhereNeedShiftIdxCell = _e.GetIdxCellByDirectAround(cellIdx, _windC.DirectT);
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