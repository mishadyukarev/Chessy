using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.System.Model
{
    public sealed class SystemsModelGame : IToggleScene, IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesModelGame _eMGame;

        Ray _ray;
        const float RAY_DISTANCE = 100;


        public readonly SelectorS SelectorS;
        public readonly AttackUnitS AttackUnitS;
        public readonly KillUnitS KillUnitS;
        public readonly AttackShieldS AttackShieldS;
        public readonly SetLastDiedS SetLastDiedS;
        public readonly MistakeS MistakeS;
        public readonly SetNewUnitS SetNewUnitS;


        #region UI

        //Down
        public readonly DoneClickS DoneClickS;
        public readonly OpenCityClickS OpenCityClickS;
        public readonly GetHeroDownS GetHeroClickDownS;
        public readonly GetPawnS GetPawnClickS;
        public readonly ToggleToolWeaponS ToggleToolWeaponClickS;
        //Left
        public readonly EnvironmentInfoS EnvironmentInfoClickS;
        public readonly ReadyS ReadyClickS;
        public readonly GetKingClickS GetKingClickS;
        public readonly BuildBuildingClickS BuildBuildingClickS;
        //Center
        public readonly GetHeroClickCenterS GetHeroClickCenterS;
        //Right
        public readonly AbilityClickS AbilityClickS;
        public readonly ConditionClickS ConditionClickS;

        #endregion


        #region Master

        public readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        public readonly UpdateS_M UpdateS_M;
        public readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        public readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        public readonly AttackUnit_M AttackUnit_M;
        public readonly UnitEatFoodUpdateS_M UnitEatFoodUpdateS_M;
        public readonly BuyS_M BuyS_M;
        public readonly MeltS_M MeltS_M;
        public readonly BuyBuildingS_M BuyBuildingS_M;
        public readonly SetUnitS_M SetUnitS_M;
        public readonly GetHeroS_M GetHeroS_M;

        public readonly WorldMeltIceWallUpdateMS WorldMeltIceWallUpdateS_M;

        #endregion


        public SystemsModelGame(in EntitiesModelGame eMGame, in EntitiesModelCommon eMCommon)
        {
            _eMGame = eMGame;
            _eMCommon = eMCommon;

            UpdateS_M = new UpdateS_M(this, eMGame);
            SetUnitS_M = new SetUnitS_M(SetNewUnitS, eMGame, eMCommon);
            GetHeroS_M = new GetHeroS_M(_eMGame);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;


            _eMGame.ZoneInfoC.IsActiveFriend = _eMCommon.GameModeTC.Is(GameModes.WithFriendOff);
            _eMGame.WhoseMove = new PlayerTC(StartValues.WHOSE_MOVE);
            _eMGame.CellClickTC = new CellClickC(StartValues.CELL_CLICK);
            _eMGame.IsSelectedCity = false;
            _eMGame.HaveTreeUnit = false;
            _eMGame.MistakeC.MistakeT = MistakeTypes.None;
            _eMGame.WinnerC.Player = PlayerTypes.None;
            _eMGame.ZoneInfoC = default;
            _eMGame.CellsC = default;

            _eMGame.WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.STRENGTH_WIND, StartValues.MAX_STREANGTH_WIND, StartValues.MIN_SNREANGTH_WIND);
            _eMGame.WeatherE.SunC = new SunC(StartValues.SUN_SIDE);
            _eMGame.WeatherE.CloudC = new CloudC(StartValues.START_CLOUD);


            _eMGame.SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);



            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                _eMGame.FertilizeC(idx_0).Resources = 0;
                _eMGame.AdultForestC(idx_0).Resources = 0;
                _eMGame.YoungForestC(idx_0).Resources = 0;
                _eMGame.HillC(idx_0).Resources = 0;
                _eMGame.MountainC(idx_0).Resources = 0;

                _eMGame.UnitTC(idx_0).Unit = UnitTypes.None;
                _eMGame.BuildingTC(idx_0).Building = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    _eMGame.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _eMGame.PlayerInfoE(playerT).ToggleScene(newSceneT);


                if (_eMCommon.GameModeTC.GameMode == GameModes.TrainingOff) _eMGame.LessonTC.LessonT = (LessonTypes)1;


                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMGame.PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }
            }


            switch (_eMCommon.GameModeTC.GameMode)
            {
                case GameModes.TrainingOff:
                    _eMGame.CurPlayerITC.Player = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    _eMGame.CurPlayerITC.Player = _eMGame.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    _eMGame.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    _eMGame.CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    var xy_0 = _eMGame.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (_eMGame.CellEs(idx_0).IsActiveParentSelf)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                _eMGame.MountainC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    _eMGame.AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                _eMGame.AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            _eMGame.RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            _eMGame.RiverEs(idx_0).SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            _eMGame.RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (_eMGame.RiverEs(idx_0).HaveRive(dir))
                            {
                                var idx_next = _eMGame.CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                                _eMGame.RiverEs(idx_next).RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = _eMGame.CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                            _eMGame.RiverEs(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }


                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    new MountainThrowHillsUpdMS(idx_0, _eMGame);
                }

            }


            if (_eMCommon.GameModeTC.Is(GameModes.TrainingOff))
            {
                _eMGame.PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    var xy_0 = _eMGame.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        _eMGame.MountainC(idx_0).Resources = 0;

                        if (_eMGame.AdultForestC(idx_0).HaveAnyResources)
                        {
                            TakeAdultForestResourcesS.TakeAdultForestResources(1f, idx_0, _eMGame);
                        }
                        _eMGame.UnitTC(idx_0).Unit = UnitTypes.King;
                        _eMGame.UnitLevelTC(idx_0).Level = LevelTypes.First;
                        _eMGame.UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        _eMGame.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        _eMGame.UnitHpC(idx_0).Health = HpValues.MAX;
                        _eMGame.UnitStepC(idx_0).Steps = StepValues.MAX;
                        _eMGame.UnitWaterC(idx_0).Water = WaterValues.MAX;
                    }

                    //else if (x == 8 && y == 8)
                    //{
                    //    MountainC(idx_0).Resources = 0;

                    //    if (AdultForestC(idx_0).HaveAnyResources)
                    //    {
                    //        TakeAdultForestResourcesS.TakeAdultForestResources(1f, idx_0, this);
                    //    }

                    //    //BuildingMainE(idx_0).Set(BuildingTypes.City, LevelTypes.First, Building_Values.HELTH_CITY, PlayerTypes.Second);
                    //}

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
                    {
                        _eMGame.MountainC(idx_0).Resources = 0;

                        _eMGame.UnitTC(idx_0).Unit = UnitTypes.Pawn;
                        _eMGame.UnitLevelTC(idx_0).Level = LevelTypes.First;
                        _eMGame.UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        _eMGame.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        _eMGame.UnitHpC(idx_0).Health = HpValues.MAX;
                        _eMGame.UnitStepC(idx_0).Steps = StepValues.MAX;
                        _eMGame.UnitWaterC(idx_0).Water = WaterValues.MAX;

                        _eMGame.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                        _eMGame.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                        var rand = UnityEngine.Random.Range(0f, 1f);

                        if (rand >= 0.5f)
                        {
                            _eMGame.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Sword;
                            _eMGame.UnitExtraLevelTC(idx_0).Level = LevelTypes.Second;
                        }
                        else
                        {
                            _eMGame.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Shield;
                            _eMGame.UnitExtraLevelTC(idx_0).Level = LevelTypes.First;
                            _eMGame.UnitExtraProtectionTC(idx_0).Protection = ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST;
                        }
                    }
                }
            }



            new GetDataCells(_eMGame);
        }

        public void Run()
        {
            if (_eMCommon.SceneTC.Scene == SceneTypes.Game)
            {
                _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

                //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

                //            if (EventSystem.current.IsPointerOverGameObject())
                //            {
                //                raycastC.Raycast = RaycastTypes.UI;
                //                return;
                //            }

                //#endif


                var rayCastT = RaycastTypes.None;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    rayCastT = RaycastTypes.UI;
                }
                else if (raycast)
                {
                    for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                    {
                        int one = _eMGame.CellEs(idx_0).CellE.InstanceIDC;
                        int two = raycast.transform.gameObject.GetInstanceID();

                        if (one == two)
                        {
                            if (_eMGame.CellsC.Current != _eMGame.CellsC.PreviousVision)
                            {
                                _eMGame.CellsC.PreviousVision = _eMGame.CellsC.Current;
                            }

                            _eMGame.CellsC.Current = idx_0;
                            rayCastT = RaycastTypes.Cell;
                        }
                    }

                    if (rayCastT == RaycastTypes.None) rayCastT = RaycastTypes.Background;
                }


                SelectorS.Run(rayCastT, _eMGame);


#if UNITY_ANDROID
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    {
            //        RayCastC.Set(RaycastTypes.UI);
            //    }
            //}
#endif
            }
        }
    }
}