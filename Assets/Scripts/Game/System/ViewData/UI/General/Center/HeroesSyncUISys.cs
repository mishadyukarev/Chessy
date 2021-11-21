using Leopotam.Ecs;

namespace Game.Game
{
    public class HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!KingZoneUIC.IsActiveZone && !PickUpgUIC.IsActiveZone && !WhereUnitsC.HaveMyHeroInGame)
            {
                HeroesViewUIC.SetActiveZone(!InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale, LevelTypes.First));
            }
            else
            {
                HeroesViewUIC.SetActiveZone(false);
            }
        }
    }
}