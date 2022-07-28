namespace Chessy.Model.Component
{
    public struct CooldownAbilitiesInSecondsC
    {
        internal readonly int[] CooldownsInSeconds;

        internal ref int CooldownRef(in AbilityTypes ability) => ref CooldownsInSeconds[(byte)ability];

        public int[] CooldonwsCopy => (int[])CooldownsInSeconds.Clone(); 
        public bool HaveCooldown(in AbilityTypes ability) => CooldownRef(ability) > 0;
        public int Cooldown(in AbilityTypes ability) => CooldownsInSeconds[(byte)ability];

        internal CooldownAbilitiesInSecondsC(in bool def) => CooldownsInSeconds = new int[(byte)AbilityTypes.End];

        internal void Set(in AbilityTypes abilityT, in int cooldown) => CooldownsInSeconds[(byte)abilityT] = cooldown;
        internal void Copy(in CooldownAbilitiesInSecondsC cooldownC)
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