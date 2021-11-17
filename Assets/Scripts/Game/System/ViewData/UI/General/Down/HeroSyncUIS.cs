using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class HeroSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            GetHeroDownUIC.SetActiveScout(InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale, LevelTypes.First), ScoutHeroCooldownC.Cooldown(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale));
        }
    }
}