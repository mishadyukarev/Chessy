using Leopotam.Ecs;

namespace Game.Game
{
    public class HeroesSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (!KingZoneUIC.IsActiveZone && !PickUpgUIC.IsActiveZone && !WhereUnitsC.HaveMyHeroInGame)
            {
                HeroesViewUIC.SetActiveZone(!InvUnitsC.Have(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI));
            }
            else
            {
                HeroesViewUIC.SetActiveZone(false);
            }
        }
    }
}