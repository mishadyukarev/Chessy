namespace Game.Game
{
    public sealed class ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            GetScoutUIC.SetActiveScout(InvUnitsC.Have(UnitTypes.Scout, LevelTypes.First, curPlayer),
                ScoutHeroCooldownC.Cooldown(UnitTypes.Scout, curPlayer));
        }
    }
}