using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Chessy.Game.Model.System
{
    public sealed class FirePawnS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public FirePawnS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Fire(in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.FIRE_PAWN)
            {
                if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                {
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    _cellEs.EffectEs.HaveFire = true;
                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.FIRE_PAWN;
                }

                else
                {
                    throw new Exception();
                }
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}