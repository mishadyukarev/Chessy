using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class FireArcherMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC> _effUnitF = default;

        private EcsFilter<FireC> _fireF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            FromToMC.Get(out var idx_from, out var idx_to);


            ref var unit_from = ref _unitF.Get1(idx_from);
            ref var ownUnit_from = ref _unitF.Get2(idx_from);

            ref var stepUnit_from = ref _statUnitF.Get1(idx_from);
            ref var effUnit_from = ref _effUnitF.Get1(idx_from);

            ref var toFireDatCom = ref _fireF.Get1(idx_to);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (stepUnit_from.HaveMaxSteps(unit_from.Unit, effUnit_from.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_from.Owner, unit_from.Unit)))
            {
                if (CellsArsonArcherComp.HaveIdxCell(whoseMove, idx_from, idx_to))
                {
                    RpcSys.SoundToGeneral(RpcTarget.All, UniqAbilTypes.FireArcher);

                    stepUnit_from.DefSteps();
                    toFireDatCom.Enable();
                }
            }

            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
