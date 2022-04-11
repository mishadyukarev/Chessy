using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.System.View
{
    public sealed class SystemsViewGame : IUpdate
    {
        readonly SyncNoneVisionS SyncNoneVisionS;
        readonly NeedFoodS SyncNeedFoodS;
        readonly BuildingFlagVS SyncBuildingFlagS;
        readonly SyncTrailVS SyncTrailS = new SyncTrailVS();
        readonly SyncBarsEnvironmentVS SyncBarsEnvironmentS;
        readonly SyncRiverVS SyncRiverS;
        readonly SyncFireVS SyncFireS;
        readonly SyncEnvironmentVS SyncEnvironmentS;
        readonly SyncStatsVS SyncStatsS;
        readonly SyncUnitVS[] _syncUnitVS = new SyncUnitVS[StartValues.CELLS];
        readonly SyncBuildingVS[] _syncBuildingSs = new SyncBuildingVS[StartValues.CELLS];
        readonly SyncFrozenArrawVS _syncFrozenArrawVS;

        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;


        public SystemsViewGame(in EntitiesViewGame eVG, in EntitiesModelGame eMG, in EntitiesViewCommon eVC, in EntitiesModelCommon eMC)
        {
            _eVGame = eVG;
            _eMGame = eMG;
            _eVCommon = eVC;


            for (byte cell = 0; cell < StartValues.CELLS; cell++)
            {
                _syncUnitVS[cell] = new SyncUnitVS(cell, eVG, eMG);
                _syncBuildingSs[cell] = new SyncBuildingVS(cell);
            }


            
            _syncFrozenArrawVS = new SyncFrozenArrawVS(eMG, eVG);
        }

        public void Update()
        {
            for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
            {
                if (_eMGame.UnitNeedUpdateViewC(cell_start).NeedUpdateView)
                {
                    _syncUnitVS[cell_start].Sync();
                    _eMGame.UnitNeedUpdateViewC(cell_start).NeedUpdateView = false;
                }
            }

            if (_eMGame.NeedUpdateView)
            {
                SoundVS.Sync(_eVGame);

                for (byte cell_start = 0; cell_start < StartValues.CELLS; cell_start++)
                {
                    if (_eMGame.IsActiveParentSelf(cell_start))
                    {
                        _syncUnitVS[cell_start].Sync();

                        _syncBuildingSs[cell_start].Sync(_eVGame, _eMGame);
                        SyncStatsS.Sync(cell_start, _eVGame, _eMGame);
                        SyncEnvironmentS.Run(cell_start, _eVGame, _eMGame);
                        SyncFireS.Sync(cell_start, _eVGame, _eMGame);
                        SyncRiverS.Sync(cell_start, _eVGame, _eMGame);
                        SyncBarsEnvironmentS.Sync(cell_start, _eVGame, _eMGame);
                        SyncTrailS.Sync(cell_start, _eVGame, _eMGame);
                        SyncNoneVisionS.Sync(cell_start, _eVGame.CellEs(cell_start).SupportCellEs.NoneSRC, _eMGame);
                        SyncNeedFoodS.Sync(cell_start, _eVGame.CellEs(cell_start).UnitVEs.NeedFoodSRC, _eMGame);
                        SyncBuildingFlagS.Sync(_eVGame.BuildingEs(cell_start).FlagSRC, cell_start, _eMGame);


                        _syncFrozenArrawVS.SyncVision(cell_start);
                        new SyncStunVS().Sync(cell_start, _eVGame, _eMGame);
                        ShieldVS.Run(cell_start, _eVGame, _eMGame);
                    }
                }


                SupportVS.Sync(_eMGame, _eVGame);
                CloudVS.Run(_eVGame, _eMGame);
                RotateAllVS.Rotate(_eVGame, _eMGame, _eVCommon);
            }
        }
    }
}