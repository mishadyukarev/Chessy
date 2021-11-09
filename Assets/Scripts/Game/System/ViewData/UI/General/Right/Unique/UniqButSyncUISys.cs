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

            UniqButtonsViewC.SetActive(UniqButtonTypes.First, abil1);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Second, abil2);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Third, default);

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
            
            
        }
    }
}