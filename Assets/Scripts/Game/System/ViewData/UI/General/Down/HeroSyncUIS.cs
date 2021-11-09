using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class HeroSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            HeroDownUIC.SetActive(InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale, LevelUnitTypes.First));
        }
    }
}