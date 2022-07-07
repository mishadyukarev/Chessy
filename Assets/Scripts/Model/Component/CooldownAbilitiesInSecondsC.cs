namespace Chessy.Model.Component
{
    public struct CooldownAbilitiesInSecondsC
    {
        readonly int[] _cooldownsInSeconds;

        public int[] CooldonwsCopy => (int[])_cooldownsInSeconds.Clone();
        public int Cooldown(in AbilityTypes ability) => _cooldownsInSeconds[(byte)ability];
        public bool HaveCooldown(in AbilityTypes ability) => Cooldown(ability) > 0;

        internal CooldownAbilitiesInSecondsC(in bool def) => _cooldownsInSeconds = new int[(byte)AbilityTypes.End];

        internal void Set(in AbilityTypes abilityT, in int cooldown) => _cooldownsInSeconds[(byte)abilityT] = cooldown;
        internal void Set(in CooldownAbilitiesInSecondsC cooldownC)
        {
            for (int i = 0; i < cooldownC._cooldownsInSeconds.Length; i++)
            {
                _cooldownsInSeconds[i] = cooldownC._cooldownsInSeconds[i];
            }
        }
        internal void Take(in AbilityTypes abilityT, in int taking) => _cooldownsInSeconds[(byte)abilityT] -= taking;
        internal void Add(in AbilityTypes abilityT, in int adding) => _cooldownsInSeconds[(byte)abilityT] += adding;

        internal void Sync(in int[] cooldowns)
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                _cooldownsInSeconds[i] = cooldowns[i];
            }
        }
    }
}