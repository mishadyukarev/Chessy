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

            foreach (var item in _idxsAroundCellCs[idx_to].IdxCellsAroundArray)
            {
                if (CloudC(item).IsCenterP)
                {
                    WindC.DirectT = _cellAroundCs[idx_to, item].DirectT.Invert();
                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (CloudC(cellIdx).IsCenterP)
                        {
                            CloudShiftC(cellIdx).WhereNeedShiftIdxCell = _cellsByDirectAroundC[cellIdx].Get(WindC.DirectT);
                        }
                    }

                    canChange = true;

                    break;
                }
            }


            if (canChange)
            {
                _cooldownAbilityCs[snowyCellFrom_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);
            }
        }
    }
}