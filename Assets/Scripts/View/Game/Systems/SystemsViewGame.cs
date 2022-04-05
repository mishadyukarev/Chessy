using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;
using UnityEngine;

namespace Chessy.Game.System.View
{
    public sealed class SystemsViewGame : IEcsRunSystem
    {
        float _timer;

        readonly SyncNoneVisionS SyncNoneVisionS;
        readonly NeedFoodS SyncNeedFoodS;
        readonly BuildingFlagVS SyncBuildingFlagS;
        readonly SyncTrailVS SyncTrailS;
        readonly SyncBarsEnvironmentVS SyncBarsEnvironmentS;
        readonly SyncRiverVS SyncRiverS;
        readonly SyncFireVS SyncFireS;
        readonly SyncEnvironmentVS SyncEnvironmentS;
        readonly SyncStatsVS SyncStatsS;
        readonly SyncUnitVS SyncUnitVS;
        readonly SyncPawnVS SyncPawnS;
        readonly SyncFrozenArrawVS _syncFrozenArrawVS;

        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;
        readonly EntitiesModelCommon _eMCommon;



        public SystemsViewGame(in EntitiesViewGame eVG, in EntitiesModelGame eMG, in EntitiesViewCommon eVC, in EntitiesModelCommon eMC)
        {
            _eVGame = eVG;
            _eMGame = eMG;
            _eVCommon = eVC;
            _eMCommon = eMC;


            SyncUnitVS = new SyncUnitVS(eVG, eMG);
            _syncFrozenArrawVS = new SyncFrozenArrawVS(eMG, eVG);
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMGame.UnitNeedUpdateViewC(cell_0).NeedUpdateView)
                {
                    SyncUnitVS.Sync(cell_0);
                    _eMGame.UnitNeedUpdateViewC(cell_0).NeedUpdateView = false;
                }
            }

            _timer += Time.deltaTime;

            if (_eMGame.NeedUpdateView || _timer >= 0.5f)
            {
                SoundVS.Sync(_eVGame);
                _timer = 0;

                if (_eMGame.NeedUpdateView)
                {
                    for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                    {
                        if (_eMGame.IsActiveParentSelf(cell_0))
                        {
                            SyncUnitVS.Sync(cell_0);

                            SyncBuildingVS.Sync(cell_0, _eVGame, _eMGame);
                            SyncStatsS.Sync(cell_0, _eVGame, _eMGame);
                            SyncEnvironmentS.Run(cell_0, _eVGame, _eMGame);
                            SyncFireS.Sync(cell_0, _eVGame, _eMGame);
                            SyncRiverS.Sync(cell_0, _eVGame, _eMGame);
                            SyncBarsEnvironmentS.Sync(cell_0, _eVGame, _eMGame);
                            SyncTrailS.Sync(cell_0, _eVGame, _eMGame);
                            SyncNoneVisionS.Sync(cell_0, _eVGame.CellEs(cell_0).SupportCellEs.NoneSRC, _eMGame);
                            SyncNeedFoodS.Sync(cell_0, _eVGame.CellEs(cell_0).UnitVEs.NeedFoodSRC, _eMGame);
                            SyncBuildingFlagS.Sync(_eVGame.BuildingEs(cell_0).FlagSRC, cell_0, _eMGame);


                            _syncFrozenArrawVS.SyncVision(cell_0);
                            SyncStunVS.Sync(cell_0, _eVGame, _eMGame);
                            ShieldVS.Run(cell_0, _eVGame, _eMGame);
                        }
                    }

                   
                    SupportVS.Sync(_eMGame, _eVGame);
                    CloudVS.Run(_eVGame, _eMGame);
                    RotateAllVS.Rotate(_eVGame, _eMGame, _eVCommon);
                }
            }
        }
    }
}