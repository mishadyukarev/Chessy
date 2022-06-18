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
            if (_isPressed && !_eMG.Common.IsOpenedBook)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    _eMG.Common.SoundActionC(ClipCommonTypes.OpenBook).Invoke();


                    _eMG.Common.IsOpenedBook = true;

                    if(_buttonT != ButtonTypes.None)
                    {
                        switch (_eMG.UnitButtonAbilitiesC(_eMG.SelectedCell).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack: 
                                _eMG.Common.PageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                _eMG.Common.PageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                _eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                _eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                _eMG.Common.PageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                _eMG.Common.PageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                _eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                _eMG.Common.PageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                _eMG.Common.PageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                _eMG.Common.PageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                _eMG.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                _eMG.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                _eMG.Common.PageBookT = PageBookTypes.Wind;
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
                        switch (_eMG.UnitEs(_eMG.SelectedCell).Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                _eMG.Common.PageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                _eMG.Common.PageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                _eMG.Common.PageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                _eMG.Common.PageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if(_eMG.LessonT == Enum.LessonTypes.HoldPressReady)
                        {
                            if (_neededPageBookT == PageBookTypes.DonerReady) _eMG.LessonTC.SetNextLesson();
                        }

                        _eMG.Common.PageBookT = _neededPageBookT;
                    }

  

                    _eMG.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}