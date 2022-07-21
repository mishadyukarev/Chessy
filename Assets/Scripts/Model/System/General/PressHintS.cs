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
            if (_isPressed && !_bookC.IsOpenedBook())
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    _dataFromViewC.SoundAction(ClipTypes.OpenBook).Invoke();

                    if (_buttonT != ButtonTypes.None)
                    {
                        switch (_buttonsAbilitiesUnitCs[_cellsC.Selected].Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack:
                                _bookC.OpenedNowPageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                _bookC.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                _bookC.OpenedNowPageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                _bookC.OpenedNowPageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _bookC.OpenedNowPageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _bookC.OpenedNowPageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                _bookC.OpenedNowPageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Wind;
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
                        switch (_effectsUnitsRightBarsCs[_cellsC.Selected].Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                _bookC.OpenedNowPageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                _bookC.OpenedNowPageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                _bookC.OpenedNowPageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                _bookC.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if (_aboutGameC.LessonT == Enum.LessonTypes.HoldPressWarrior)
                        {
                            /*if (_neededPageBookT == PageBookTypes.Town)  */_s.SetNextLesson();
                        }

                        _bookC.OpenedNowPageBookT = _neededPageBookT;
                    }



                    _updateAllViewC.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}