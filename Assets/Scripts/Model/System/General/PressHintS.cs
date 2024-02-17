using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.System;
using UnityEngine;
namespace Chessy.Model
{
    public sealed class PressHintS : SystemModelAbstract
    {
        readonly PageBookTypes _neededPageBookT;
        readonly ButtonTypes _buttonT;
        readonly byte _effectButtonT;
        bool _isPressed;
        float _timer;

        const float TIMER = 0.5f;

        public PressHintS(in PageBookTypes pageBookT, in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _neededPageBookT = pageBookT;
        }
        public PressHintS(in ButtonTypes buttonT, in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _buttonT = buttonT;
        }
        public PressHintS(in byte effect, in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _effectButtonT = effect;
        }

        public void Press(in bool isPressed) => _isPressed = isPressed;

        public void Update()
        {
            if (_isPressed && !bookC.IsOpenedBook())
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    dataFromViewC.SoundAction(ClipTypes.OpenBook).Invoke();

                    if (_buttonT != ButtonTypes.None)
                    {
                        switch (UnitButtonsC(indexesCellsC.Selected).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack:
                                bookC.OpenedNowPageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                bookC.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                bookC.OpenedNowPageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                bookC.OpenedNowPageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                bookC.OpenedNowPageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                bookC.OpenedNowPageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                bookC.OpenedNowPageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.Resurrect:
                                break;
                            case AbilityTypes.SetTeleport:
                                break;
                            case AbilityTypes.Teleport:
                                break;
                            case AbilityTypes.InvokeSkeletons:
                                break;
                            default:
                                break;
                        }
                    }

                    else if (_effectButtonT != 0)
                    {
                        switch (_effectsUnitsRightBarsCs[indexesCellsC.Selected].Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                bookC.OpenedNowPageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                bookC.OpenedNowPageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                bookC.OpenedNowPageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                bookC.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if (aboutGameC.LessonT == Enum.LessonTypes.HoldPressWarrior)
                        {
                            /*if (_neededPageBookT == PageBookTypes.Town)  */
                            s.SetNextLesson();
                        }

                        bookC.OpenedNowPageBookT = _neededPageBookT;
                    }



                    updateAllViewC.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}