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
            if (_isPressed && !_e.BookC.IsOpenedBook())
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    _e.SoundAction(ClipTypes.OpenBook).Invoke();

                    if (_buttonT != ButtonTypes.None)
                    {
                        switch (_e.UnitButtonAbilitiesC(_e.SelectedCellIdx).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack:
                                _e.OpenedNowPageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                _e.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                _e.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _e.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                _e.OpenedNowPageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                _e.OpenedNowPageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                _e.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _e.OpenedNowPageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _e.OpenedNowPageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                _e.OpenedNowPageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                _e.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _e.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                _e.OpenedNowPageBookT = PageBookTypes.Wind;
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
                        switch (_e.EffectsUnitsRightBarsC(_e.SelectedCellIdx).Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                _e.OpenedNowPageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                _e.OpenedNowPageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                _e.OpenedNowPageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                _e.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if (_e.LessonT == Enum.LessonTypes.HoldPressReady)
                        {
                            if (_neededPageBookT == PageBookTypes.DonerReady)  _s.SetNextLesson();
                        }

                        _e.OpenedNowPageBookT = _neededPageBookT;
                    }



                    _e.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}