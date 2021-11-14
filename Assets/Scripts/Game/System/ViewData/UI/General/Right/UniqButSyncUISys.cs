using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class UniqButSyncUISys : IEcsRunSystem
    {
        private EcsFilter<UniqAbilC, CdownUniqC> _unitAbilFilt = default;

        public void Run()
        {
            ref var uniq_sel = ref _unitAbilFilt.Get1(SelectorC.IdxSelCell);
            ref var cdUniq_sel = ref _unitAbilFilt.Get2(SelectorC.IdxSelCell);

            var abil1 = uniq_sel.Ability(UniqButtonTypes.First);
            var abil2 = uniq_sel.Ability(UniqButtonTypes.Second);
            var abil3 = uniq_sel.Ability(UniqButtonTypes.Third);

            UniqButtonsViewC.SetActive(UniqButtonTypes.First, abil1);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Second, abil2);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Third, abil3);

            if(abil1 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.First, cdUniq_sel.HaveCooldown(abil1));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.First, cdUniq_sel.Cooldown(abil1).ToString());
            }
            if(abil2 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.Second, cdUniq_sel.HaveCooldown(abil2));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.Second, cdUniq_sel.Cooldown(abil2).ToString());
            }
            if (abil3 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.Third, cdUniq_sel.HaveCooldown(abil3));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.Third, cdUniq_sel.Cooldown(abil3).ToString());
            }
        }
    }
}