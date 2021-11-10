using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class CellStunViewS : IEcsRunSystem
    {
        private EcsFilter<CellStunViewC, StunC> _stunFilt = default;
        private EcsFilter<CellUnitDataC, VisibleC> _unitVisFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _stunFilt)
            {
                ref var stunView_0 = ref _stunFilt.Get1(idx_0);
                ref var stun_0 = ref _stunFilt.Get2(idx_0);
                ref var visUnit_0 = ref _unitVisFilt.Get2(idx_0);

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