using Chessy.Common;
using Chessy.Game.Model.Component;

namespace Chessy.Game.Model.Entity
{
    public struct AbilityUnitE
    {
        readonly float[] _abilities;
        public ref float Cooldown(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public bool HaveCooldown(in AbilityTypes ability) => Cooldown(ability) > 0;

        public readonly UniqueButtonsC UniqueButtonsC;

        internal AbilityUnitE(in float[] cooldowns)
        {
            _abilities = cooldowns;
            UniqueButtonsC = new UniqueButtonsC(new AbilityTypes[(byte)ButtonTypes.End - 1]);
        }
    }
}