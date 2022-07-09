using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryChangeDirectWindWithSnowyM(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            for (byte curCellIdx = 0; curCellIdx < IndexCellsValues.CELLS; curCellIdx++)
            {
                if (_e.HaveCloud(curCellIdx) && _e.IsCenterCloud(curCellIdx))
                {
                    //for (var nextCellIdx = 0; nextCellIdx < IndexCellsValues.CELLS; nextCellIdx++)
                    //{
                    //    _e.DirectWindT = _e.AroundCellE(curCellIdx).Direct(idx_to);

                    //    break;
                    //}

                    

                   
                }
            }

            
            _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

            _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);

            if (_e.LessonT == LessonTypes.ChangeDirectionWind)
            {
                _s.SetNextLesson();
            }
        }
    }
}