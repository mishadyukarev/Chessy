using Chessy.Model.Values;
using Photon.Pun;
using System.Collections.Generic;

namespace Chessy.Model
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
                objs.Add(_e.EnergyUnit(cell_0));
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

                //objs.Add(_e.AttackSimpleCellsC(cell_0).IdxsByteClone);
                //objs.Add(_e.AttackUniqueCellsC(cell_0).IdxsByteClone);

                //objs.Add(_e.CellsForShift(cell_0).IdxsByteClone);
                //objs.Add(_e.UnitNeedStepsForShiftC(cell_0).NeedStepsCopy);

                objs.Add(_e.UnitButtonAbilitiesC(cell_0).AbilityTypesClone);
                objs.Add(_e.UnitCooldownAbilitiesC(cell_0).CooldonwsFloat);

                objs.Add(_e.StunUnit(cell_0));
                objs.Add(_e.ShieldEffect(cell_0));
                objs.Add(_e.FrozenArrawEffect(cell_0));
                //objs.Add(_e.HaveKingEffect(cell_0));

                //objs.Add(_e.WhereUnitCanFireAdultForestC(cell_0).IdxsByteClone);


                #region Building

                objs.Add(_e.BuildingOnCellT(cell_0));
                objs.Add(_e.BuildingLevelT(cell_0));
                objs.Add(_e.BuildingPlayerT(cell_0));
                objs.Add(_e.BuildingHp(cell_0));
                objs.Add(_e.BuildingVisibleC(cell_0).IsVisibleClone);
                //objs.Add(_e.WoodcutterExtract(cell_0).Resources);
                //objs.Add(_e.FarmExtract(cell_0).Resources);

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

            objs.Add(_e.DirectWindT);
            objs.Add(_e.SpeedWind);
            objs.Add(_e.CenterCloudCellIdx);
            objs.Add(_e.SunSideT);


            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                objs.Add(_e.PlayerInfoC(playerT).IsReadyForStartOnlineGame);
                objs.Add(_e.PlayerInfoC(playerT).WoodForBuyHouse);
                objs.Add(_e.BuildingsInTownInfoC(playerT).HaveBuildingsClone);
                //objs.Add(_e.PlayerInfoE(playerT).WhereKingEffects.IdxsByteClone);

                objs.Add(_e.PlayerInfoC(playerT).HaveKingInInventor);
                //objs.Add(_e.PlayerInfoE(playerT).KingInfoE.CellKing);

                objs.Add(_e.PawnPeopleInfoC(playerT).PeopleInCity);
                objs.Add(_e.PawnPeopleInfoC(playerT).MaxAvailable);
                objs.Add(_e.PawnPeopleInfoC(playerT).AmountInGame);

                objs.Add(_e.GodInfoC(playerT).HaveGodInInventor);
                objs.Add(_e.GodInfoC(playerT).UnitT);
                objs.Add(_e.GodInfoC(playerT).Cooldown);

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        objs.Add(_e.ToolWeaponsInInventor(playerT, levelT, twT));
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    objs.Add(_e.ResourcesInInventory(playerT, resT));
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
                _e.UnitMainC(cell_0).IsArcherDirectedToRight = (bool)objects[idxCurrent++];
                for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
                    _e.UnitVisibleC(cell_0).Set(playerT, (bool)objects[idxCurrent++]);

                _e.HpUnitC(cell_0).Health = (double)objects[idxCurrent++];
                _e.EnergyUnitC(cell_0).Energy = (double)objects[idxCurrent++];
                _e.WaterUnitC(cell_0).Water = (double)objects[idxCurrent++];

                _e.UnitMainC(cell_0).DamageSimpleAttack = (double)objects[idxCurrent++];
                _e.UnitMainC(cell_0).DamageOnCell = (double)objects[idxCurrent++];

                _e.SetMainToolWeaponT(cell_0, (ToolWeaponTypes)objects[idxCurrent++]);
                _e.SetMainTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);

                _e.SetExtraToolWeaponT(cell_0, (ToolWeaponTypes)objects[idxCurrent++]);
                _e.SetExtraTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.UnitExtraTWE(cell_0).ProtectionShield = (float)objects[idxCurrent++];

                _e.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractAdultForest = (float)objects[idxCurrent++];
                _e.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractHill = (float)objects[idxCurrent++];

                _e.SetLastDiedUnitT(cell_0, (UnitTypes)objects[idxCurrent++]);
                _e.SetLastDiedLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.SetLastDiedPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);

                //_e.AttackSimpleCellsC(cell_0).Sync((byte[])objects[idxCurrent++]);
                //_e.AttackUniqueCellsC(cell_0).Sync((byte[])objects[idxCurrent++]);

                //_e.CellsForShift(cell_0).Sync((byte[])objects[idxCurrent++]);
                //_e.UnitNeedStepsForShiftC(cell_0).Sync((float[])objects[idxCurrent++]);

                _e.UnitButtonAbilitiesC(cell_0).Sync((byte[])objects[idxCurrent++]);
                _e.UnitCooldownAbilitiesC(cell_0).Sync((float[])objects[idxCurrent++]);

                _e.UnitEffectsC(cell_0).StunHowManyUpdatesNeedStay = (float)objects[idxCurrent++];
                _e.UnitEffectsC(cell_0).ProtectionRainyMagicShield = (float)objects[idxCurrent++];
                _e.UnitEffectsC(cell_0).ShootsFrozenArrawArcher = (int)objects[idxCurrent++];
                //_e.UnitEffectsC(cell_0).HaveKingEffect = (bool)objects[idxCurrent++];

                //_e.WhereUnitCanFireAdultForestC(cell_0).Sync((byte[])objects[idxCurrent++]);


                #region Building

                _e.SetBuildingOnCellT(cell_0, (BuildingTypes)objects[idxCurrent++]);
                _e.SetBuildingLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
                _e.SetBuildingPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);
                _e.BuildingHpC(cell_0).Health = (double)objects[idxCurrent++];
                _e.BuildingVisibleC(cell_0).Sync((bool[])objects[idxCurrent++]);
                //_e.WoodcutterExtract(cell_0).Resources = (float)objects[idxCurrent++];
                //_e.FarmExtract(cell_0).Resources = (float)objects[idxCurrent++];

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

            _e.DirectWindT = (DirectTypes)objects[idxCurrent++];
            _e.SpeedWind = (byte)objects[idxCurrent++];
            _e.CenterCloudCellIdx = (byte)objects[idxCurrent++];
            _e.SunSideT = (SunSideTypes)objects[idxCurrent++];

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).PlayerInfoC.IsReadyForStartOnlineGame = (bool)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).PlayerInfoC.WoodForBuyHouse = (float)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).BuildingsInTownInfoC.Sync((bool[])objects[idxCurrent++]);
                //_e.PlayerInfoE(playerT).WhereKingEffects.Sync((byte[])objects[idxCurrent++]);

                //_e.PlayerInfoE(playerT).PlayerInfoC.HaveKingInInventor = (bool)objects[idxCurrent++];
                //_e.PlayerInfoE(playerT).KingInfoE.CellKing = (byte)objects[idxCurrent++];

                _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = (int)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = (int)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = (int)objects[idxCurrent++];

                _e.PlayerInfoE(playerT).GodInfoC.HaveGodInInventor = (bool)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).GodInfoC.UnitT = (UnitTypes)objects[idxCurrent++];
                _e.PlayerInfoE(playerT).GodInfoC.Cooldown = (float)objects[idxCurrent++];

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _e.SetToolWeaponsInInventor(playerT, levelT, twT, (int)objects[idxCurrent++]);
                    }
                }

                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                {
                    _e.SetResourcesInInventory(playerT, resT, (float)objects[idxCurrent++]);
                }
            }

            _e.NeedUpdateView = true;
        }
    }
}