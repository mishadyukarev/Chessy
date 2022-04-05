using Chessy.Common;
using Chessy.Game.Model.Component;

namespace Chessy.Game.Model.Entity
{
    public struct AbilityUnitE
    {
        public readonly UniqueButtonsC UniqueButtonsC;
        public readonly CooldownAbilitiesC CooldownsC;

        internal AbilityUnitE(in float[] cooldowns)
        {
            UniqueButtonsC = new UniqueButtonsC(new AbilityTypes[(byte)ButtonTypes.End]);
            CooldownsC = new CooldownAbilitiesC(default);
        }
    }
}