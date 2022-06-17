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
            if (_isPressed && !eMG.Common.IsOpenedBook)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    eMG.Common.SoundActionC(ClipCommonTypes.OpenBook).Invoke();


                    eMG.Common.IsOpenedBook = true;

                    if(_buttonT != ButtonTypes.None)
                    {
                        switch (eMG.UnitButtonAbilitiesC(eMG.SelectedCell).Ability(_buttonT))
                        {
                            case AbilityTypes.CircularAttack: 
                                eMG.Common.PageBookT = PageBookTypes.CircularAttackKing;
                                break;

                            case AbilityTypes.KingPassiveNearBonus:
                                eMG.Common.PageBookT = PageBookTypes.PassiveKing;
                                break;

                            case AbilityTypes.FirePawn:
                                eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.PutOutFirePawn:
                                eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.Seed:
                                eMG.Common.PageBookT = PageBookTypes.SeedForest;
                                break;

                            case AbilityTypes.SetFarm:
                                eMG.Common.PageBookT = PageBookTypes.BuildFarm;
                                break;

                            case AbilityTypes.DestroyBuilding:
                                break;

                            case AbilityTypes.FireArcher:
                                eMG.Common.PageBookT = PageBookTypes.Fire;
                                break;

                            case AbilityTypes.ChangeCornerArcher:
                                eMG.Common.PageBookT = PageBookTypes.ToggleUniqueAttackArcher;
                                break;

                            case AbilityTypes.GrowAdultForest:
                                eMG.Common.PageBookT = PageBookTypes.GrowAdultForest;
                                break;

                            case AbilityTypes.StunElfemale:
                                eMG.Common.PageBookT = PageBookTypes.StunElfemale;
                                break;

                            case AbilityTypes.IncreaseWindSnowy:
                                eMG.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.DecreaseWindSnowy:
                                eMG.Common.PageBookT = PageBookTypes.Wind;
                                break;

                            case AbilityTypes.ChangeDirectionWind:
                                eMG.Common.PageBookT = PageBookTypes.Wind;
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
                        switch (eMG.UnitEs(eMG.SelectedCell).Effect((ButtonTypes)_effectButtonT))
                        {
                            case EffectTypes.Shield:
                                eMG.Common.PageBookT = PageBookTypes.FrozenShield;
                                break;

                            case EffectTypes.Stun:
                                eMG.Common.PageBookT = PageBookTypes.Stun;
                                break;

                            case EffectTypes.Arraw:
                                eMG.Common.PageBookT = PageBookTypes.FrozenArraw;
                                break;

                            case EffectTypes.DamageAdd:
                                eMG.Common.PageBookT = PageBookTypes.PassiveKing;
                                break;

                            default:
                                break;
                        }


                    }

                    else
                    {
                        if(eMG.LessonT == Enum.LessonTypes.HoldPressReady)
                        {
                            if (_neededPageBookT == PageBookTypes.DonerReady) eMG.LessonTC.SetNextLesson();
                        }

                        eMG.Common.PageBookT = _neededPageBookT;
                    }

  

                    eMG.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}