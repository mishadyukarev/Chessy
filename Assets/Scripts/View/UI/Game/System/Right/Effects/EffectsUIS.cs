using Chessy.Game.Model.Entity;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class EffectsUIS : SystemUIAbstract
    {
        readonly Resources _resourcesE;
        readonly EntitiesViewUIGame _eUI;

        readonly Dictionary<EffectTypes, bool> _isFilled = new Dictionary<EffectTypes, bool>();

        internal EffectsUIS(in Resources resources, in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _resourcesE = resources;
            _eUI = eUI;

            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
        }

        internal override void Sync()
        {
            var needActiveZone = false;


            if (e.CellsC.IsSelectedCell)
            {
                var idx_sel = e.CellsC.Selected;

                if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                {
                    for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled[effectT] = false;

                    for (byte idx_eff = 0; idx_eff < 5; idx_eff++)
                    {
                        _eUI.RightEs.Effect(idx_eff).GO.SetActive(false);

                        if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.ThatsYourEffects)
                        {


                            if (!_isFilled[EffectTypes.Shield] && e.ShieldUnitEffectC(idx_sel).HaveAnyProtection)
                            {
                                _eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                                _eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = _resourcesE.Sprite(EffectTypes.Shield);
                                _isFilled[EffectTypes.Shield] = true;

                                needActiveZone = true;
                            }
                            else if (!_isFilled[EffectTypes.Arraw] && e.FrozenArrawEffectC(idx_sel).HaveShoots)
                            {
                                _eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                                _eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = _resourcesE.Sprite(EffectTypes.Arraw);
                                _isFilled[EffectTypes.Arraw] = true;
                                needActiveZone = true;
                            }
                            else if (!_isFilled[EffectTypes.Stun] && e.StunUnitC(idx_sel).IsStunned)
                            {
                                _eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                                _eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = _resourcesE.Sprite(EffectTypes.Stun);
                                _isFilled[EffectTypes.Stun] = true;
                                needActiveZone = true;
                            }
                            else if (!_isFilled[EffectTypes.DamageAdd] && e.HaveKingEffect(idx_sel))
                            {
                                _eUI.RightEs.Effect(idx_eff).GO.SetActive(true);
                                _eUI.RightEs.Effect(idx_eff).ImageUIC.Image.sprite = _resourcesE.Sprite(EffectTypes.DamageAdd);
                                _isFilled[EffectTypes.DamageAdd] = true;
                                needActiveZone = true;
                            }
                        }
                    }
                }
            }

            _eUI.RightEs.Effect(0).GO.Transform.parent.gameObject.SetActive(needActiveZone);

        }
    }
}