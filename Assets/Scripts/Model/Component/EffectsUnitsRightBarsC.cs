namespace Chessy.Model
{
    public struct EffectsUnitsRightBarsC
    {
        internal readonly EffectTypes[] EffectsArray;

        public ref EffectTypes Effect(in ButtonTypes buttonT) => ref EffectsArray[(byte)buttonT];

        internal EffectsUnitsRightBarsC(in bool def)
        {
            EffectsArray = new EffectTypes[(byte)ButtonTypes.End];
        }

        internal void Set(in ButtonTypes buttonT, in EffectTypes effectT) => EffectsArray[(byte)buttonT] = effectT;
    }
}