using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct UniqButSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var abil1 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.First, SelIdx<IdxC>().Idx);
            ref var abil2 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.Second, SelIdx<IdxC>().Idx);
            ref var abil3 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.Third, SelIdx<IdxC>().Idx);

            UniqButtonsUIC.SetActive(UniqueButtonTypes.First, abil1.Ability);
            UniqButtonsUIC.SetActive(UniqueButtonTypes.Second, abil2.Ability);
            UniqButtonsUIC.SetActive(UniqueButtonTypes.Third, abil3.Ability);

            if (abil1.Ability != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqueButtonTypes.First, Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqueButtonTypes.First, Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString());
            }
            if (abil2.Ability != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqueButtonTypes.Second, Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqueButtonTypes.Second, Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).ToString());
            }
            if (abil3.Ability != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqueButtonTypes.Third, Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UniqButtonsUIC.SetTextCooldown(UniqueButtonTypes.Third, Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString());
            }
        }
    }
}