using System.Collections.Generic;

namespace Game.Game
{
    sealed class RightEffectsUIS : SystemViewAbstract, IEcsRunSystem
    {
        readonly Dictionary<EffectTypes, bool> _isFilled;

        internal RightEffectsUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
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

                if (UnitEs(Es.SelectedIdxE.IdxC.Idx).TypeE.HaveUnit)
                {
                    for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++)
                    {
                        _isFilled[effectT] = false;
                    }

                    for (byte idx_eff = 0; idx_eff < 5; idx_eff++)
                    {
                        RightUIEs.Effect(idx_eff).GO.SetActive(false);

                        if(!_isFilled[EffectTypes.Shield])
                        {
                            if (UnitEffectEs(idx_sel).ShieldE.HaveShieldEffect)
                            {
                                RightUIEs.Effect(idx_eff).GO.SetActive(true);
                                RightUIEs.Effect(idx_eff).ImageUIC.Sprite = VEs.ResourceSpriteEs.Sprite(AbilityTypes.BonusNear).SpriteC.Sprite;
                                _isFilled[EffectTypes.Shield] = true;
                            }
                        }
                    }
                }
            }
        }
    }
}