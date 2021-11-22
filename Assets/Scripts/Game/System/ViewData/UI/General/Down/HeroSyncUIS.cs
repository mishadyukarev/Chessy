using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class HeroSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            GetHeroDownUIC.SetActiveScout(InvUnitsC.Have(UnitTypes.Elfemale, LevelTypes.First, WhoseMoveC.CurPlayerI), 
                ScoutHeroCooldownC.Cooldown(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale));
        }
    }
}