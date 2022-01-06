using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class CellStunViewS : IEcsRunSystem
    {
        private EcsFilter<UnitC, VisibleC> _unitVisF = default;
        private EcsFilter<StunC> _effUnitF = default;

        private EcsFilter<StunVC> _stunVF = default;

        public void Run()
        {
            foreach (byte idx_0 in _stunVF)
            {
                ref var stunView_0 = ref _stunVF.Get1(idx_0);
                ref var stun_0 = ref _effUnitF.Get1(idx_0);
                ref var visUnit_0 = ref _unitVisF.Get2(idx_0);

                if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                {
                    stunView_0.SetEnabled(stun_0.IsStunned);
                }
                else
                {
                    stunView_0.SetEnabled(false);
                }
            }
        }
    }
}