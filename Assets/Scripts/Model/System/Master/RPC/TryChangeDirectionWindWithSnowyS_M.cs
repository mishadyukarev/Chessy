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

            foreach (var item in idxsAroundCellCs[idx_to].IdxCellsAroundArray)
            {
                if (cloudCs[item].IsCenterP)
                {
                    windC.DirectT = cellAroundCs[idx_to, item].DirectT.Invert();
                    for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
                    {
                        if (cloudCs[cellIdx].IsCenterP)
                        {
                            shiftCloudCs[cellIdx].WhereNeedShiftIdxCell = cellsByDirectAroundC[cellIdx].Get(windC.DirectT);
                        }
                    }

                    canChange = true;

                    break;
                }
            }


            if (canChange)
            {
                _cooldownAbilityCs[snowyCellFrom_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);
            }
        }
    }
}