﻿using Chessy.Common.Entity.View;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Game.System.View
{
    public sealed class SystemsViewGame : IUpdate
    {
        readonly List<Action> _updates;

        readonly NeedFoodVS[] _syncNeedFoodSs = new NeedFoodVS[StartValues.CELLS];
        readonly BuildingFlagVS[] _syncBuildingFlagSs = new BuildingFlagVS[StartValues.CELLS];
        readonly Dictionary<DirectTypes, SyncTrailVS[]> _syncTrailSs = new Dictionary<DirectTypes, SyncTrailVS[]>();
        readonly SyncBarsEnvironmentVS[] _syncBarsEnvironmentSs = new SyncBarsEnvironmentVS[StartValues.CELLS];
        readonly SyncRiverVS[] _syncRiverSs = new SyncRiverVS[StartValues.CELLS];
        readonly SyncFireVS[] _syncFireSs = new SyncFireVS[StartValues.CELLS];
        readonly SyncEnvironmentVS[] _syncEnvironmentSs = new SyncEnvironmentVS[StartValues.CELLS];
        readonly SyncBlocksVS[] _syncStatsSs = new SyncBlocksVS[StartValues.CELLS];
        readonly SyncUnitVS[] _syncUnitSs = new SyncUnitVS[StartValues.CELLS];
        readonly SyncBuildingVS[] _syncBuildingSs = new SyncBuildingVS[StartValues.CELLS];
        readonly SyncUnitBarHpVS[] _syncUnitBarHpSs = new SyncUnitBarHpVS[StartValues.CELLS];
        readonly SyncFrozenArrawVS[] _syncFrozenArrawSs = new SyncFrozenArrawVS[StartValues.CELLS];
        readonly SyncStunVS[] _syncStunSs = new SyncStunVS[StartValues.CELLS];
        readonly SyncShieldVS[] _syncShieldSs = new SyncShieldVS[StartValues.CELLS];
        readonly SyncCloudVS[] _syncCloudSs = new SyncCloudVS[StartValues.CELLS];
        readonly SyncRotationVS[] _syncRotationSs = new SyncRotationVS[StartValues.CELLS];
        readonly SyncIdxAndXyInfoVS[] _syncIdxAndXyInfoSs = new SyncIdxAndXyInfoVS[StartValues.CELLS];

        readonly SyncSupportVS _syncSupportS;
        readonly SyncSoundVS _syncSoundS;
        readonly SyncCameraVS _syncCameraS;

        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;


        public SystemsViewGame(in EntitiesViewGame eVG, in EntitiesModelGame eMG, in EntitiesViewCommon eVC)
        {
            _eVGame = eVG;
            _eMGame = eMG;
            _eVCommon = eVC;


            _updates = new List<Action>();

            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                _syncTrailSs.Add(dirT, new SyncTrailVS[StartValues.CELLS]);
            }

            var blackVisisionSrCs = new SpriteRendererVC[StartValues.CELLS];
            var redCircularSRCs = new SpriteRendererVC[StartValues.CELLS];
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                blackVisisionSrCs[cellIdxCurrent] = eVG.CellEs(cellIdxCurrent).SupportCellEs.NoneSRC;
                redCircularSRCs[cellIdxCurrent] = _eVGame.CellEs(cellIdxCurrent).RedCircularSRC;
            }

            _updates.Add(new SyncBlackVisionVS(blackVisisionSrCs, eMG).Sync);
            _updates.Add(new SyncRedCircularVS(redCircularSRCs, eMG).Sync);

            for (byte startCell = 0; startCell < StartValues.CELLS; startCell++)
            {
                _updates.Add(new KingPassiveVS(_eVGame.CellEs(startCell).UnitEs.EffectE.KingPassiveGOC, startCell, _eMGame).Sync);


                _syncUnitSs[startCell] = new SyncUnitVS(eVG.CellEs(startCell).UnitEs, startCell, eMG);
                _syncBuildingSs[startCell] = new SyncBuildingVS(eVG.CellEs(startCell).BuildingEs, startCell, eMG);
                _syncStatsSs[startCell] = new SyncBlocksVS(eVG, startCell, eMG);
                _syncUnitBarHpSs[startCell] = new SyncUnitBarHpVS(eVG.CellEs(startCell).UnitEs.UnitHpBarSRC, startCell, eMG);
                _syncFrozenArrawSs[startCell] = new SyncFrozenArrawVS(eVG.CellEs(startCell).UnitEs.EffectE, startCell, eMG);
                _syncEnvironmentSs[startCell] = new SyncEnvironmentVS(eVG.CellEs(startCell).EnvironmentVEs, startCell, eMG);
                _syncFireSs[startCell] = new SyncFireVS(eVG.CellEs(startCell).FireVE.SRC, startCell, eMG);
                _syncRiverSs[startCell] = new SyncRiverVS(eVG.CellEs(startCell).RiverE, startCell, eMG);
                _syncBarsEnvironmentSs[startCell] = new SyncBarsEnvironmentVS(eVG, startCell, eMG);
                
                _syncNeedFoodSs[startCell] = new NeedFoodVS(_eVGame.CellEs(startCell).UnitEs.Block(CellBlockTypes.NeedFood), startCell, _eMGame);
                _syncBuildingFlagSs[startCell] = new BuildingFlagVS(_eVGame.CellEs(startCell).BuildingEs.FlagSRC, startCell, _eMGame);
                _syncStunSs[startCell] = new SyncStunVS(_eVGame.CellEs(startCell).UnitEs.EffectE.StunSRC, startCell, _eMGame);
                _syncShieldSs[startCell] = new SyncShieldVS(_eVGame.CellEs(startCell).UnitEs.EffectE.ShieldSRC, startCell, _eMGame);
                
                _syncCloudSs[startCell] = new SyncCloudVS(_eVGame.CellEs(startCell).CloudSRC, startCell, _eMGame);
                _syncRotationSs[startCell] = new SyncRotationVS(_eVGame.CellEs(startCell), startCell, _eMGame);

                _syncIdxAndXyInfoSs[startCell] = new SyncIdxAndXyInfoVS(_eVGame.CellEs(startCell).IdxAndXyInfoTMPC, startCell, _eMGame);



                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                {
                    _syncTrailSs[dirT][startCell] = new SyncTrailVS(dirT, eVG.CellEs(startCell).TrailCellVC(dirT), startCell, eMG);
                }
            }

            _updates.Add(new SyncSunSideVS(_eVGame, _eMGame).Sync);


            _syncSupportS = new SyncSupportVS(_eVGame, _eMGame);
            _syncSoundS = new SyncSoundVS(_eVGame);
            _syncCameraS = new SyncCameraVS(_eVCommon, _eMGame);
        }

        public void Update()
        {
            for (byte startCell = 0; startCell < StartValues.CELLS; startCell++)
            {
                if (_eMGame.UnitNeedUpdateViewC(startCell).NeedUpdateView)
                {
                    _syncUnitSs[startCell].Sync();
                    _eMGame.UnitNeedUpdateViewC(startCell).NeedUpdateView = false;
                }
            }

            if (_eMGame.NeedUpdateView)
            {
                _updates.ForEach((Action action) => action.Invoke());

                for (byte currentCellIdx = 0; currentCellIdx < StartValues.CELLS; currentCellIdx++)
                {
                    if (!_eMGame.IsBorder(currentCellIdx))
                    {
                        _syncUnitSs[currentCellIdx].Sync();
                        _syncBuildingSs[currentCellIdx].Sync();
                        _syncStatsSs[currentCellIdx].Sync();
                        _syncUnitBarHpSs[currentCellIdx].Sync();
                        _syncFrozenArrawSs[currentCellIdx].Sync();
                        _syncEnvironmentSs[currentCellIdx].Sync();
                        _syncFireSs[currentCellIdx].Sync();
                        _syncRiverSs[currentCellIdx].Sync();
                        _syncBarsEnvironmentSs[currentCellIdx].Sync();
                        _syncNeedFoodSs[currentCellIdx].Sync();
                        _syncBuildingFlagSs[currentCellIdx].Sync();
                        _syncStunSs[currentCellIdx].Sync();
                        _syncStunSs[currentCellIdx].Sync();
                        _syncShieldSs[currentCellIdx].Sync();
                        _syncCloudSs[currentCellIdx].Sync();
                        _syncRotationSs[currentCellIdx].Sync();
                        _syncIdxAndXyInfoSs[currentCellIdx].Sync();

                        for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++) _syncTrailSs[dirT][currentCellIdx].Sync();
                    }
                }

                _syncSupportS.Sync();
                _syncSoundS.Sync();
                _syncCameraS.Sync();
            }
        }
    }
}