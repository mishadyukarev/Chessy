using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct UniqButSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var uniq_sel = ref Unit<UniqAbilC>(SelIdx<IdxC>().Idx);

            var abil1 = uniq_sel.Ability(UniqButTypes.First);
            var abil2 = uniq_sel.Ability(UniqButTypes.Second);
            var abil3 = uniq_sel.Ability(UniqButTypes.Third);

            UniqButtonsUIC.SetActive(UniqButTypes.First, abil1);
            UniqButtonsUIC.SetActive(UniqButTypes.Second, abil2);
            UniqButtonsUIC.SetActive(UniqButTypes.Third, abil3);

            if (abil1 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.First, Unit<CooldownC>(abil1, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.First, Unit<CooldownC>(abil1, SelIdx<IdxC>().Idx).Cooldown.ToString());
            }
            if (abil2 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.Second, Unit<CooldownC>(abil2, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.Second, Unit<CooldownC>(abil2, SelIdx<IdxC>().Idx).ToString());
            }
            if (abil3 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.Third, Unit<CooldownC>(abil3, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.Third, Unit<CooldownC>(abil3, SelIdx<IdxC>().Idx).Cooldown.ToString());
            }
        }
    }
}