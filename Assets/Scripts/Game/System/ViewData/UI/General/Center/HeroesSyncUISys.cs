using Leopotam.Ecs;

namespace Chessy.Game
{
    public class HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!KingZoneViewUIC.IsActiveZone && !PickUpgZoneViewUIC.IsActiveZone)
            {
                HeroesViewUIC.SetActiveZone(!HeroInvC.HaveHero(WhoseMoveC.CurPlayerI));
            }
            else
            {
                HeroesViewUIC.SetActiveZone(false);
            }
        }
    }
}