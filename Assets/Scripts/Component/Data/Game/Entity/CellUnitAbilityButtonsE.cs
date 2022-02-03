using ECS;

namespace Game.Game
{
    public sealed class CellUnitAbilityButtonsE : EntityAbstract
    {
        public ref AbilityC AbilityC => ref Ent.Get<AbilityC>();

        public CellUnitAbilityButtonsE(in EcsWorld gameW) : base(gameW) { }

        public void Reset() => AbilityC.Ability = AbilityTypes.None;
    }
}