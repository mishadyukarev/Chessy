using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class UniqButSyncUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitAbilFilt = default;

        public void Run()
        {
            ref var uniq_sel = ref _unitAbilFilt.Get2(SelectorC.IdxSelCell);

            var abil1 = uniq_sel.Ability(UniqButtonTypes.First);
            var abil2 = uniq_sel.Ability(UniqButtonTypes.Second);
            var abil3 = uniq_sel.Ability(UniqButtonTypes.Third);

            UniqButtonsViewC.SetActive(UniqButtonTypes.First, abil1);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Second, abil2);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Third, abil3);

            if(abil1 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.First, uniq_sel.HaveCooldown(abil1));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.First, uniq_sel.Cooldown(abil1).ToString());
            }
            if(abil2 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.Second, uniq_sel.HaveCooldown(abil2));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.Second, uniq_sel.Cooldown(abil2).ToString());
            }
            if (abil3 != default)
            {
                UniqButtonsViewC.SetActiveCooldownZone(UniqButtonTypes.Third, uniq_sel.HaveCooldown(abil3));
                UniqButtonsViewC.SetTextCooldown(UniqButtonTypes.Third, uniq_sel.Cooldown(abil3).ToString());
            }
        }
    }
}