using Chessy.Common.Entity.View;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.View;
using Chessy.Game.Values;

namespace Chessy.Game
{
    public struct UpdateViewS : IEcsRunSystem
    {
        readonly SystemsViewGame _sVGame;
        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;

        public UpdateViewS(in SystemsViewGame sVGame, in EntitiesViewGame eVGame, in EntitiesModelGame eMGame, in EntitiesViewCommon eVCommon)
        {
            _sVGame = sVGame;
            _eVGame = eVGame;
            _eMGame = eMGame;
            _eVCommon = eVCommon;
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                SyncUnitVS.Sync(idx_0, _eVGame, _eMGame);
                SyncBuildingVS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncStatsS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncEnvironmentS.Run(idx_0, _eVGame, _eMGame);
                _sVGame.SyncFireS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncRiverS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncBarsEnvironmentS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncTrailS.Sync(idx_0, _eVGame, _eMGame);
                _sVGame.SyncNoneVisionS.Sync(idx_0, _eVGame.CellEs(idx_0).SupportCellEs.NoneSRC, _eMGame);
                _sVGame.SyncNeedFoodS.Sync(idx_0, _eVGame.CellEs(idx_0).UnitVEs.NeedFoodSRC, _eMGame);
                _sVGame.SyncBuildingFlagS.Sync(_eVGame.BuildingEs(idx_0).FlagSRC, idx_0, _eMGame);


                _eVGame.CellEs(idx_0).UnitVEs.EffectVEs.SyncVision(_eMGame.UnitEs(idx_0), idx_0 == _eMGame.CellsC.Selected, _eMGame);
                SyncStunVS.Sync(idx_0, _eVGame, _eMGame);
                ShieldVS.Run(idx_0, _eVGame, _eMGame);
            }

            SoundVS.Sync(_eVGame);
            SupportVS.Sync(_eMGame, _eVGame);
            CloudVS.Run(_eVGame, _eMGame);
            RotateAllVS.Rotate(_eVGame, _eMGame, _eVCommon);
        }
    }
}