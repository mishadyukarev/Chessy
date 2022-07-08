﻿using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {

        internal void TryFireForestWithArcherM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_e.WhereUnitCanFireAdultForestC(cell_from).Can(cell_to))
            {
                if (!_e.UnitMainC(cell_from).HaveCoolDownForAttackAnyUnit)
                {
                    _s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);
                    _e.HaveFire(cell_to) = true;

                    _e.UnitMainC(cell_from).CooldownForAttackAnyUnitInSeconds = ValuesChessy.COOLDOWN_AFTER_ATTACK;

                    if (_e.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _s.SetNextLesson();
                    }
                    
                }
            }
        }
    }
}