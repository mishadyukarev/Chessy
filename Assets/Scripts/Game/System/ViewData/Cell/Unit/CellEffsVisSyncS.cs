using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class CellEffsVisSyncS : IEcsRunSystem
    {
        private EcsFilter<FireC> _fireF;
        private EcsFilter<FireVC> _fireVF;

        public void Run()
        {
            foreach (var idx in _fireVF)
            {
                if (_fireF.Get1(idx).HaveFire)
                {
                    _fireVF.Get1(idx).EnableSR();
                }

                else
                {
                    _fireVF.Get1(idx).DisableSR();
                }
            }
        }
    }
}