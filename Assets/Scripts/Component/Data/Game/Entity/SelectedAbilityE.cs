using ECS;

namespace Game.Game
{
    public sealed class SelectedAbilityE : EntityAbstract
    {
        ref AbilityTC AbilityTCRef => ref Ent.Get<AbilityTC>();
        public AbilityTC AbilityTC => Ent.Get<AbilityTC>();

        internal SelectedAbilityE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void SetAbility(in AbilityTypes ability, in ClickerObjectE clickerObjectE)
        {
            AbilityTCRef.Ability = ability;
            clickerObjectE.CellClickC.Click = CellClickTypes.UniqueAbility;
        }
    }
}