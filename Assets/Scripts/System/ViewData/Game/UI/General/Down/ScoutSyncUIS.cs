using static Game.Game.UIEntDownScout;

namespace Game.Game
{
    struct ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = Entities.WhoseMove.CurPlayerI;

            var isActive = InventorUnitsE.Units(UnitTypes.Scout, LevelTypes.First, curPlayer).Have;
            var cooldown = Entities.ScoutHeroCooldownE(UnitTypes.Scout, curPlayer).Cooldown.Amount;


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