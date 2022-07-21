namespace Chessy.Model.Component
{
    public struct CooldownAbilitiesInSecondsC
    {
        internal readonly int[] CooldownsInSeconds;

        public int[] CooldonwsCopy => (int[])CooldownsInSeconds.Clone();
        public ref int Cooldown(in AbilityTypes ability) => ref CooldownsInSeconds[(byte)ability];
        public bool HaveCooldown(in AbilityTypes ability) => Cooldown(ability) > 0;

        internal CooldownAbilitiesInSecondsC(in bool def) => CooldownsInSeconds = new int[(byte)AbilityTypes.End];

        internal void Set(in AbilityTypes abilityT, in int cooldown) => CooldownsInSeconds[(byte)abilityT] = cooldown;
        internal void Set(in CooldownAbilitiesInSecondsC cooldownC)
        {
            for (int i = 0; i < cooldownC.CooldownsInSeconds.Length; i++)
            {
                CooldownsInSeconds[i] = cooldownC.CooldownsInSeconds[i];
            }
        }
        internal void Take(in AbilityTypes abilityT, in int taking) => CooldownsInSeconds[(byte)abilityT] -= taking;
        internal void Add(in AbilityTypes abilityT, in int adding) => CooldownsInSeconds[(byte)abilityT] += adding;

        internal void Sync(in int[] cooldowns)
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                CooldownsInSeconds[i] = cooldowns[i];
            }
        }
    }
}