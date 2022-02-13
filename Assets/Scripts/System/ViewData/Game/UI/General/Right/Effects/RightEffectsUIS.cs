using System.Collections.Generic;

namespace Game.Game
{
    sealed class RightEffectsUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Resources _resources;
        readonly Dictionary<EffectTypes, bool> _isFilled;

        internal RightEffectsUIS(in Resources resources, in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _resources = resources;

            _isFilled = new Dictionary<EffectTypes, bool>();
            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++)
            {
                _isFilled.Add(effectT, false);
            }
        }

        public void Run()
        {
            if (Es.SelectedIdxE.IsSelCell)
            {
                var idx_sel = Es.SelectedIdxE.IdxC.Idx;

                if (UnitEs(Es.SelectedIdxE.IdxC.Idx).UnitE.HaveUnit)
                {
                    for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++)
                    {
                        _isFilled[effectT] = false;
                    }

                    for (byte idx_eff = 0; idx_eff < 5; idx_eff++)
                    {
                        UIEs.RightEs.Effect(idx_eff).GO.SetActive(false);

                        if(!_isFilled[EffectTypes.Shield])
                        {
                            if (Es.UnitE(idx_sel).HaveShieldEffect)
                            {
                                UIEs.RightEs.Effect(idx_eff).GO.SetActive(true);
                                UIEs.RightEs.Effect(idx_eff).ImageUIC.Sprite = _resources.Sprite(AbilityTypes.BonusNear);
                                _isFilled[EffectTypes.Shield] = true;
                            }
                        }
                    }
                }
            }
        }
    }
}