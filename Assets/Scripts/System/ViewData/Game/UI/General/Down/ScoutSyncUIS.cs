using static Game.Game.UIEntDownScout;

namespace Game.Game
{
    sealed class ScoutSyncUIS : SystemViewAbstract, IEcsRunSystem
    {
        public ScoutSyncUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMove.CurPlayerI;

            var isActive = Es.InventorUnitsEs.Units(UnitTypes.Scout, LevelTypes.First, curPlayer).HaveUnits;
            var cooldown = Es.ScoutHeroCooldownE(UnitTypes.Scout, curPlayer).Cooldown.Amount;


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