using System.Collections.Generic;

namespace Chessy.Game
{
    public struct EffectsUIS
    {
        readonly Dictionary<EffectTypes, bool> _isFilled;

        internal EffectsUIS(in Dictionary<EffectTypes, bool> dict)
        {
            _isFilled = dict;
            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
        }

        public void Run(in ResourcesE resources, in EntitiesViewUIGame eUI, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (e.CellsC.IsSelectedCell)
            {
                var idx_sel = e.CellsC.Selected;

                if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                {
                    for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled[effectT] = false;

                    for (byte idx_eff = 0; idx_eff < 5; idx_eff++)
                    {
                        eUI.RightEs.Effect(idx_eff).GO.SetActive(false);

                        if (!_isFilled[EffectTypes.Shield] && e.UnitEffectShield(idx_sel).HaveAnyProtection)
                        {
                            eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                            eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = resources.Sprite(EffectTypes.Shield);
                            _isFilled[EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[EffectTypes.Arraw] && e.UnitEffectFrozenArrawC(idx_sel).HaveShoots)
                        {
                            eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                            eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = resources.Sprite(EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && e.UnitEffectStunC(idx_sel).IsStunned)
                        {
                            eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                            eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = resources.Sprite(EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && e.UnitEffectsE(idx_sel).HaveKingEffect)
                        {
                            eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                            eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = resources.Sprite(EffectTypes.DamageAdd);
                            _isFilled[EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}