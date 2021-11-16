using Leopotam.Ecs;

namespace Chessy.Game
{
    public class HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!KingZoneViewUIC.IsActiveZone && !PickUpgUIC.IsActiveZone && !WhereUnitsC.HaveMyHeroInGame)
            {
                HeroesViewUIC.SetActiveZone(!InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale, LevelUnitTypes.First));
            }
            else
            {
                HeroesViewUIC.SetActiveZone(false);
            }
        }
    }
}