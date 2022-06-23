using Chessy.Model.Values;
using Photon.Pun;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        public void SyncDataM()
        {
            var objs = new List<object>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                objs.Add(_e.UnitT(cell_0));
                objs.Add(_e.UnitLevelT(cell_0));
                objs.Add(_e.UnitPlayerT(cell_0));
                objs.Add(_e.UnitConditionT(cell_0));
                objs.Add(_e.IsRightArcherUnit(cell_0));
                for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
                    objs.Add(_e.UnitVisibleC(cell_0).IsVisible(playerT));

                objs.Add(_e.HpUnit(cell_0));
                objs.Add(_e.StepUnit(cell_0));
                objs.Add(_e.WaterUnit(cell_0));

                objs.Add(_e.DamageAttack(cell_0));
                objs.Add(_e.DamageOnCell(cell_0));

                objs.Add(_e.MainToolWeaponT(cell_0));
                objs.Add(_e.MainTWLevelT(cell_0));

                objs.Add(_e.ExtraToolWeaponT(cell_0));
                objs.Add(_e.ExtraTWLevelT(cell_0));
                objs.Add(_e.ExtraTWProtection(cell_0));

                objs.Add(_e.PawnExtractAdultForest(cell_0));
                objs.Add(_e.PawnExtractHill(cell_0));

                objs.Add(_e.LastDiedUnitT(cell_0));
                objs.Add(_e.LastDiedLevelT(cell_0));
                objs.Add(_e.LastDiedPlayerT(cell_0));

                objs.Add(_e.AttackSimpleCellsC(cell_0).IdxsByteClone);
                objs.Add(_e.AttackUniqueCellsC(cell_0).IdxsByteClone);

                objs.Add(_e.CellsForShift(cell_0).IdxsByteClone);
                objs.Add(_e.UnitNeedStepsForShiftC(cell_0).NeedStepsCopy);

                objs.Add(_e.UnitButtonAbilitiesC(cell_0).AbilityTypesClone);
                objs.Add(_e.UnitCooldownAbilitiesC(cell_0).CooldonwsFloat);

                objs.Add(_e.StunUnit(cell_0));
                objs.Add(_e.ShieldEffect(cell_0));
                objs.Add(_e.FrozenArrawEffect(cell_0));
                objs.Add(_e.HaveKingEffect(cell_0));

                objs.Add(_e.UnitForArsonC(cell_0).IdxsByteClone);


                #region Building

                objs.Add(_e.BuildingOnCellT(cell_0));
                objs.Add(_e.BuildingLevelT(cell_0));
                objs.Add(_e.BuildingPlayerT(cell_0));
                objs.Add(_e.BuildingHp(cell_0));
                objs.Add(_e.BuildingVisibleC(cell_0).IsVisibleClone);
                objs.Add(_e.WoodcutterExtractC(cell_0).Resources);
                objs.Add(_e.FarmExtractFertilizeC(cell_0).Resources);

                #endregion


                objs.Add(_e.YoungForestC(cell_0).Resources);
                objs.Add(_e.AdultForestC(cell_0).Resources);
                objs.Add(_e.MountainC(cell_0).Resources);
                objs.Add(_e.HillC(cell_0).Resources);
                objs.Add(_e.FertilizeC(cell_0).Resources);

                objs.Add(_e.RiverT(cell_0));
                objs.Add(_e.HaveRiverC(cell_0).HaveRives);

                objs.Add(_e.HaveFire(cell_0));

                objs.Add(_e.TrailVisibleC(cell_0).IsVisibleClone);
                objs.Add(_e.HealthTrail(cell_0).Healths);
            }

            objs.Add(_e.IsStartedGame);
            objs.Add(_e.Motions);
            objs.Add(_e.WhereTeleportC.StartIdxCell);
            objs.Add(_e.WhereTeleportC.EndIdxCell);
            objs.Add(_e.WinnerPlayerT);
            objs.Add(_e.WhoseMovePlayerT);

            objs.Add(_e.WeatherE.WindC.DirectT);
            objs.Add(_e.WeatherE.WindC.Speed);
            objs.Add(_e.WeatherE.CellIdxCenterCloud);
            objs.Add(_e.WeatherE.SunSideT);


            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                objs.Add(_e.PlayerInfoE(playerT).IsReadyForStartOnlineGame);
                objs.Add(_e.PlayerInfoE(playerT).WoodForBuyHouse);
                objs.Add(_e.PlayerInfoE(playerT).BuildingsInfoC.HaveBuildingsClone);
                objs.Add(_e.PlayerInfoE(playerT).WhereKingEffects.IdxsByteClone);

                objs.Add(_e.PlayerInfoE(playerT).KingInfoE.HaveInInventor);
                objs.Add(_e.PlayerInfoE(playerT).KingInfoE.CellKing);

                objs.Add(_e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity);
                objs.Add(_e.PlayerInfoE(playerT).PawnInfoC.MaxAvailable);
                objs.Add(_e.PlayerInfoE(playerT).PawnInfoC.AmountInGame);

                objs.Add(_e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor);
                objs.Add(_e.PlayerInfoE(playerT).GodInfoE.UnitT);
                objs.Add(_e.PlayerInfoE(playerT).GodInfoE.Cooldown);

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        objs.Add(_e.PlayerInfoE(playerT).LevelE(levelT).ToolWeapons(twT));
                    }

                    for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                    {
                        objs.Add(_e.PlayerInfoE(playerT).LevelE(levelT).BuildingInfoE(buildingT).IdxC.IdxsByteClone);
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    objs.Add(_e.PlayerInfoE(playerT).ResourcesC(resT).Resources);
                }
            }



            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _e.RpcC.Action0(nameof(SyncData), RpcTarget.Others, objects);
        }

        internal void SyncData(in object[] objects)
        {
            if (PhotonNetwork.IsMasterClient) return;

            var idxCurrent = 0;

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _e.SetUnitOnCellT(cell_0, (UnitTypes)objects[idxCurrent++]);
                _e.SetUnitLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.SetUnitPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);
                _e.SetUnitConditionT(cell_0, (ConditionUnitTypes)objects[idxCurrent++]);
                _e.UnitIsRightArcherC(cell_0).IsRight = (bool)objects[idxCurrent++];
                for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
                    _e.UnitVisibleC(cell_0).Set(playerT, (bool)objects[idxCurrent++]);

                _e.HpUnitC(cell_0).Health = (double)objects[idxCurrent++];
                _e.StepUnitC(cell_0).Steps = (double)objects[idxCurrent++];
                _e.WaterUnitC(cell_0).Water = (double)objects[idxCurrent++];

                _e.DamageAttackC(cell_0).Damage = (double)objects[idxCurrent++];
                _e.DamageOnCellC(cell_0).Damage = (double)objects[idxCurrent++];

                _e.SetMainToolWeaponT(cell_0, (ToolWeaponTypes)objects[idxCurrent++]);
                _e.SetMainTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);

                _e.SetExtraToolWeaponT(cell_0, (ToolWeaponTypes)objects[idxCurrent++]);
                _e.SetExtraTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.ExtraTWProtectionC(cell_0).Protection = (float)objects[idxCurrent++];

                _e.PawnExtractAdultForestC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.PawnExtractHillC(cell_0).Resources = (float)objects[idxCurrent++];

                _e.SetLastDiedUnitT(cell_0, (UnitTypes)objects[idxCurrent++]);
                _e.SetLastDiedLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.SetLastDiedPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);

                _e.AttackSimpleCellsC(cell_0).Sync((byte[])objects[idxCurrent++]);
                _e.AttackUniqueCellsC(cell_0).Sync((byte[])objects[idxCurrent++]);

                _e.CellsForShift(cell_0).Sync((byte[])objects[idxCurrent++]);
                _e.UnitNeedStepsForShiftC(cell_0).Sync((float[])objects[idxCurrent++]);

                _e.UnitButtonAbilitiesC(cell_0).Sync((byte[])objects[idxCurrent++]);
                _e.UnitCooldownAbilitiesC(cell_0).Sync((float[])objects[idxCurrent++]);

                _e.StunUnitC(cell_0).Stun = (float)objects[idxCurrent++];
                _e.ShieldUnitEffectC(cell_0).Protection = (float)objects[idxCurrent++];
                _e.FrozenArrawEffectC(cell_0).Shoots = (int)objects[idxCurrent++];
                _e.HaveKingEffect(cell_0) = (bool)objects[idxCurrent++];

                _e.UnitForArsonC(cell_0).Sync((byte[])objects[idxCurrent++]);


                #region Building

                _e.SetBuildingOnCellT(cell_0, (BuildingTypes)objects[idxCurrent++]);
                _e.SetBuildingLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.SetBuildingPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);
                _e.BuildingHpC(cell_0).Health = (double)objects[idxCurrent++];
                _e.BuildingVisibleC(cell_0).Sync((bool[])objects[idxCurrent++]);
                _e.WoodcutterExtractC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.FarmExtractFertilizeC(cell_0).Resources = (float)objects[idxCurrent++];

                #endregion


                _e.YoungForestC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.AdultForestC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.MountainC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.HillC(cell_0).Resources = (float)objects[idxCurrent++];
                _e.FertilizeC(cell_0).Resources = (float)objects[idxCurrent++];

                _e.SetRiverT(cell_0, (RiverTypes)objects[idxCurrent++]);
                _e.HaveRiverC(cell_0).Sync((bool[])objects[idxCurrent++]);

                _e.HaveFire(cell_0) = (bool)objects[idxCurrent++];

                _e.TrailVisibleC(cell_0).Sync((bool[])objects[idxCurrent++]);
                _e.HealthTrail(cell_0).Sync((float[])objects[idxCurrent++]);
            }

            _e.IsStartedGame = (bool)objects[idxCurrent++];
            _e.Motions = (int)objects[idxCurrent++];
            _e.WhereTeleportC.StartIdxCell = (byte)objects[idxCurrent++];
            _e.WhereTeleportC.EndIdxCell = (byte)objects[idxCurrent++];
            _e.WinnerPlayerT = (PlayerTypes)objects[idxCurrent++];
            _e.WhoseMovePlayerT = (PlayerTypes)objects[idxCurrent++];

            _e.WeatherE.WindC.DirectT = (DirectTypes)objects[idxCurrent++];
            _e.WeatherE.WindC.Speed = (float)objects[idxCurrent++];
            _e.WeatherE.CellIdxCenterCloud = (byte)objects[idxCurrent++];
            _e.WeatherE.SunSideT = (SunSideTypes)objects[idxCurrent++];

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).IsReadyForStartOnlineGame = (bool)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).WoodForBuyHouse = (float)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).BuildingsInfoC.Sync((bool[])objects[idxCurrent++]);
                _e.PlayerInfoE(playerT).WhereKingEffects.Sync((byte[])objects[idxCurrent++]);

                _e.PlayerInfoE(playerT).KingInfoE.HaveInInventor = (bool)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).KingInfoE.CellKing = (byte)objects[idxCurrent++];

                _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = (int)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = (int)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = (int)objects[idxCurrent++];

                _e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = (bool)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).GodInfoE.UnitT = (UnitTypes)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).GodInfoE.Cooldown = (float)objects[idxCurrent++];

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _e.PlayerInfoE(playerT).LevelE(levelT).ToolWeapons(twT) = (int)objects[idxCurrent++];
                    }

                    for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                    {
                        _e.PlayerInfoE(playerT).LevelE(levelT).BuildingInfoE(buildingT).IdxC.Sync((byte[])objects[idxCurrent++]);
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    _e.PlayerInfoE(playerT).ResourcesC(resT).Resources = (float)objects[idxCurrent++];
                }
            }

            _e.NeedUpdateView = true;
        }
    }
}