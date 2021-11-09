using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class StunElfemaleMS : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, StunUnitC> _unitStunFilt = default;

        public void Run()
        {
            ForStunElfemaleMC.Get(out var idx_from, out var idx_to);

            _unitStunFilt.Get2(idx_to).SetNewStun();
        }
    }
}