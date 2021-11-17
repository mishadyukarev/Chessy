using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC> _effUnitF = default;

        private EcsFilter<FireC> _fireF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToDoingMC.Get(out var idx_from, out var idx_to);


            ref var unit_from = ref _unitF.Get1(idx_from);
            ref var level_from = ref _unitF.Get2(idx_from);
            ref var own_from = ref _unitF.Get3(idx_from);

            ref var step_from = ref _statUnitF.Get1(idx_from);
            ref var eff_from = ref _effUnitF.Get1(idx_from);

            ref var fire_to = ref _fireF.Get1(idx_to);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (step_from.HaveMaxSteps(unit_from.Unit, eff_from.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_from.Unit, level_from.Level, own_from.Owner)))
            {
                if (CellsArsonArcherComp.HaveIdxCell(whoseMove, idx_from, idx_to))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.FireArcher);

                    step_from.DefSteps();
                    fire_to.Enable();
                }
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
