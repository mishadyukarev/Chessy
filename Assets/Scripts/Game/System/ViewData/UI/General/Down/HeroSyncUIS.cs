using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class HeroSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            HeroDownUIC.SetActive(HeroInvC.HaveHero(WhoseMoveC.CurPlayerI));
        }
    }
}