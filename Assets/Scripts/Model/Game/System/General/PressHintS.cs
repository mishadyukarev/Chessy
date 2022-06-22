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
            if (_isPressed && !_e.Common.IsOpenedBook)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    _e.Common.SoundActionC(ClipCommonTypes.OpenBook).Invoke();

                    if (_buttonT != ButtonTypes.None)
                    {
                        switch (_e.UnitButtonAbilitiesC(_e.SelectedCellIdx).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack:
                                _e.Common.PageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                _e.Common.PageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                _e.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _e.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                _e.Common.PageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                _e.Common.PageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                _e.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _e.Common.PageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _e.Common.PageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                _e.Common.PageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                _e.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _e.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                _e.Common.PageBookT = PageBookTypes.Wind;
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
                                _e.Common.PageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                _e.Common.PageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                _e.Common.PageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                _e.Common.PageBookT = PageBookTypes.PassiveKing;
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

                        _e.Common.PageBookT = _neededPageBookT;
                    }



                    _e.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}