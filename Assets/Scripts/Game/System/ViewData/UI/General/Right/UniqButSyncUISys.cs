using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class UniqButSyncUISys : IEcsRunSystem
    {
        private EcsFilter<UniqAbilC, CooldownUniqC> _unitAbilFilt = default;

        public void Run()
        {
            ref var uniq_sel = ref _unitAbilFilt.Get1(SelIdx<IdxC>().Idx);
            ref var cdUniq_sel = ref _unitAbilFilt.Get2(SelIdx<IdxC>().Idx);

            var abil1 = uniq_sel.Ability(UniqButTypes.First);
            var abil2 = uniq_sel.Ability(UniqButTypes.Second);
            var abil3 = uniq_sel.Ability(UniqButTypes.Third);

            UniqButtonsUIC.SetActive(UniqButTypes.First, abil1);
            UniqButtonsUIC.SetActive(UniqButTypes.Second, abil2);
            UniqButtonsUIC.SetActive(UniqButTypes.Third, abil3);

            if(abil1 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.First, cdUniq_sel.HaveCooldown(abil1));
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.First, cdUniq_sel.Cooldown(abil1).ToString());
            }
            if(abil2 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.Second, cdUniq_sel.HaveCooldown(abil2));
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.Second, cdUniq_sel.Cooldown(abil2).ToString());
            }
            if (abil3 != default)
            {
                UniqButtonsUIC.SetActiveCooldownZone(UniqButTypes.Third, cdUniq_sel.HaveCooldown(abil3));
                UniqButtonsUIC.SetTextCooldown(UniqButTypes.Third, cdUniq_sel.Cooldown(abil3).ToString());
            }
        }
    }
}