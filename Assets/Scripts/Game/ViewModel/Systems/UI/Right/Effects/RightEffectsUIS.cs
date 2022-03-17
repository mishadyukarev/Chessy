using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class RightEffectsUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly ResourcesE _resources;
        readonly Dictionary<EffectTypes, bool> _isFilled;

        internal RightEffectsUIS(in ResourcesE resources,  in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
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
            if (E.CellsC.Selected > 0)
            {
                var idx_sel = E.CellsC.Selected;

                if (E.UnitTC(E.CellsC.Selected).HaveUnit)
                {
                    for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++)
                    {
                        _isFilled[effectT] = false;
                    }

                    for (byte idx_eff = 0; idx_eff < 5; idx_eff++)
                    {
                        UIE.RightEs.Effect(idx_eff).GO.SetActive(false);

                        if(!_isFilled[EffectTypes.Shield])
                        {
                            if (E.UnitEffectShield(idx_sel).HaveAnyProtection)
                            {
                                UIE.RightEs.Effect(idx_eff).GO.SetActive(true);
                                UIE.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = _resources.Sprite(AbilityTypes.KingPassiveNearBonus).Sprite;
                                _isFilled[EffectTypes.Shield] = true;
                            }
                        }
                    }
                }
            }
        }
    }
}