using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class PressHintS : SystemModel
    {
        readonly PageBookTypes _neededPageBookT;
        readonly ButtonTypes _buttonT;
        readonly byte _effectButtonT;
        bool _isPressed;
        float _timer;

        const float TIMER = 0.5f;

        public PressHintS(in PageBookTypes pageBookT, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _neededPageBookT = pageBookT;
        }
        public PressHintS(in ButtonTypes buttonT, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _buttonT = buttonT;
        }
        public PressHintS(in byte effect, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _effectButtonT = effect;
        }

        public void Press(in bool isPressed) => _isPressed = isPressed;

        public void Update()
        {
            if (_isPressed && !_e.Com.BookC.IsOpenedBook())
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    _e.Com.SoundActionC(ClipCommonTypes.OpenBook).Invoke();

                    if (_buttonT != ButtonTypes.None)
                    {
                        switch (_e.UnitButtonAbilitiesC(_e.SelectedCellIdx).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Wind;
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
                        switch (_e.UnitEs(_e.SelectedCellIdx).Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                _e.Com.OpenedNowPageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if (_e.LessonT == Enum.LessonTypes.HoldPressReady)
                        {
                            if (_neededPageBookT == PageBookTypes.DonerReady) _e.LessonT.SetNextLesson();
                        }

                        _e.Com.OpenedNowPageBookT = _neededPageBookT;
                    }



                    _e.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}