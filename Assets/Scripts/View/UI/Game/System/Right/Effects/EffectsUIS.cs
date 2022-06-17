using Chessy.Common;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class EffectsUIS : SystemUIAbstract
    {
        readonly Resources _resourcesE;
        readonly EntitiesViewUIGame _eUI;

        readonly bool[] _needActiveButton = new bool[(byte)ButtonTypes.End];

        internal EffectsUIS(in Resources resources, in EntitiesViewUIGame eUI, in EntitiesModelGame eMG) : base(eMG)
        {
            _resourcesE = resources;
            _eUI = eUI;
        }

        internal override void Sync()
        {
            var needActiveZone = false;

            if (!e.LessonTC.HaveLesson || e.LessonT >= Enum.LessonTypes.ThatsYourEffects)
            {
                if (e.CellsC.IsSelectedCell)
                {
                    var idx_sel = e.CellsC.Selected;

                    if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                    {
                        needActiveZone = true;

                        for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                        {
                            _needActiveButton[(byte)buttonT] = e.UnitEs(idx_sel).Effect(buttonT) != EffectTypes.None;

                            if (_needActiveButton[(byte)buttonT])
                            {
                                _eUI.RightEs.Effect(buttonT).ImageC.Image.sprite = _resourcesE.Sprite(e.UnitEs(idx_sel).Effect(buttonT));
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