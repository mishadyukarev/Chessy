﻿using Chessy.Common;
using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class EffectsUIS : SystemUIAbstract
    {
        readonly Resources _resourcesE;
        readonly EntitiesViewUI _eUI;

        readonly bool[] _needActiveButton = new bool[(byte)ButtonTypes.End];

        internal EffectsUIS(in Resources resources, in EntitiesViewUI eUI, in EntitiesModel eMG) : base(eMG)
        {
            _resourcesE = resources;
            _eUI = eUI;
        }

        internal override void Sync()
        {
            var needActiveZone = false;

            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.ThatsYourEffects)
            {
                if (_e.CellsC.IsSelectedCell)
                {
                    var idx_sel = _e.CellsC.Selected;

                    if (_e.UnitT(_e.CellsC.Selected).HaveUnit())
                    {
                        needActiveZone = true;

                        for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                        {
                            _needActiveButton[(byte)buttonT] = _e.UnitEs(idx_sel).Effect(buttonT) != EffectTypes.None;

                            if (_needActiveButton[(byte)buttonT])
                            {
                                _eUI.RightEs.Effect(buttonT).ImageC.Image.sprite = _resourcesE.Sprite(_e.UnitEs(idx_sel).Effect(buttonT));
                            }

                            _eUI.RightEs.Effect(buttonT).GO.SetActive(_needActiveButton[(byte)buttonT]);
                        }
                    }
                }
            }

            _eUI.RightEs.Effect(ButtonTypes.First).GO.Transform.parent.gameObject.SetActive(needActiveZone);
        }
    }
}