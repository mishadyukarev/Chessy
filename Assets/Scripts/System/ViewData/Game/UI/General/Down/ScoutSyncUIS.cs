using static Game.Game.UIEntDownScout;

namespace Game.Game
{
    struct ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            var isActive = InvUnitsC.Have(UnitTypes.Scout, LevelTypes.First, curPlayer);
            var cooldown = ScoutHeroCooldownC.Cooldown(UnitTypes.Scout, curPlayer);


            Scout<ButtonUIC>().SetActive(isActive);

            if (isActive && cooldown > 0)
            {
                Cooldown<TextUIC>().SetActiveParent(true);
                Cooldown<TextUIC>().Text = cooldown.ToString();
            }
            else
            {
                Cooldown<TextUIC>().SetActiveParent(false);
            }
        }
    }
}