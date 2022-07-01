namespace Chessy.Model.Component
{
    public struct CooldownAbilitiesC
    {
        readonly float[] _cooldowns;

        public float[] CooldonwsFloat => (float[])_cooldowns.Clone();
        public float Cooldown(in AbilityTypes ability) => _cooldowns[(byte)ability];
        public bool HaveCooldown(in AbilityTypes ability) => Cooldown(ability) > 0;

        internal CooldownAbilitiesC(in bool def) => _cooldowns = new float[(byte)AbilityTypes.End];

        internal void Set(in AbilityTypes abilityT, in float cooldown) => _cooldowns[(byte)abilityT] = cooldown;
        internal void Set(in CooldownAbilitiesC cooldownC)
        {
            for (int i = 0; i < cooldownC._cooldowns.Length; i++)
            {
                _cooldowns[i] = cooldownC._cooldowns[i];
            }
        }
        internal void Take(in AbilityTypes abilityT, in float taking) => _cooldowns[(byte)abilityT] -= taking;
        internal void Add(in AbilityTypes abilityT, in float adding) => _cooldowns[(byte)abilityT] += adding;

        internal void Sync(in float[] cooldowns)
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                _cooldowns[i] = cooldowns[i];
            }
        }
    }
}