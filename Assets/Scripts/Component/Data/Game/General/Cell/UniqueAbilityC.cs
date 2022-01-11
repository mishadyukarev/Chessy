namespace Game.Game
{
    public struct UniqueAbilityC : IUnitUniqueButtonCellE
    {
        public UniqueAbilityTypes Ability;

        public void Reset() => Ability = default;
    }
}