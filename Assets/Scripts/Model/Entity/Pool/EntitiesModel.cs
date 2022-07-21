using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Model.Entity
{
    public sealed class EntitiesModel
    {
        readonly ResourcesC[] _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];
        readonly PlayerInfoE[] _forPlayerEs = new PlayerInfoE[(byte)PlayerTypes.End];
        readonly CellEs[] _cellEs;
        readonly byte[][] _cellsAround = new byte[IndexCellsValues.CELLS][];
        readonly byte[,] _cellsByDirect = new byte[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly byte[,] _idxs = new byte[IndexCellsValues.X_AMOUNT, IndexCellsValues.Y_AMOUNT];


        public readonly CommonGameE CommonGameE;
        public readonly WeatherE WeatherE = new();


        public bool IsSelectedCity { get; internal set; }
        public bool HaveTreeUnit { get; internal set; }
        public bool IsActivatedIdxAndXyInfoCells { get; internal set; }
        public int AmountPlantedYoungForests { get; internal set; }


        public ref PlayerInfoE PlayerInfoE(in PlayerTypes player) => ref _forPlayerEs[(byte)player];
        public ResourcesInInventoryC ResourcesInInventoryC(in PlayerTypes playerT) => PlayerInfoE(playerT).ResourcesInInventoryC;
        public float ResourcesInInventory(in PlayerTypes playerT, in ResourceTypes resT) => ResourcesInInventoryC(playerT).Resources(resT);

        public PawnPeopleInfoC PawnPeopleInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).PawnInfoC;

        public GodInfoC GodInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).GodInfoC;

        public PlayerInfoC PlayerInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).PlayerInfoC;

        public BuildingsInTownInfoC BuildingsInTownInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).BuildingsInTownInfoC;

        public HowManyToolWeaponsInInventoryC HowManyToolWeaponsInInventoryC(in PlayerTypes playerT) => PlayerInfoE(playerT).HowManyToolWeaponsInInventoryC;
        public int ToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT) => HowManyToolWeaponsInInventoryC(playerT).ToolWeapons(twT, levT);
        public Action SoundAction(in ClipTypes clipT) => CommonGameE.DataFromViewC.SoundAction(clipT);
        public Action SoundAction(in AbilityTypes abilityT) => CommonGameE.DataFromViewC.SoundAction(abilityT);
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];


        #region Cells

        public CellEs CellEs(in byte idx) => _cellEs[idx];


        #region Around

        public CellAroundE AroundCellE(in byte cell, in byte cellIdx) => CellEs(cell).AroundCellE(cellIdx);

        public CellAroundC CellAroundC(in byte cellIdx, in byte nextCellIdx) => AroundCellE(cellIdx, nextCellIdx).CellAroundC;

        public byte[] IdxsCellsAround(in byte startCellIdx) => (byte[])_cellsAround[startCellIdx].Clone();
        public byte GetIdxCellByDirectAround(in byte startCellIdx, in DirectTypes dirT) => _cellsByDirect[startCellIdx, (byte)dirT];

        #endregion


        public CloudOnCellE CloudOnCellE(in byte cellIdx) => CellEs(cellIdx).CloudE;
        public WhereViewIdxCellC CloudWhereViewDataOnCellC(in byte cellIdx) => CloudOnCellE(cellIdx).WhereSkinAndWhereDataInfoC;
        public PositionC CloudPossitionC(in byte cellIdx) => CloudOnCellE(cellIdx).PositionC;


        #region Unit

        public UnitE UnitE(in byte idx) => CellEs(idx).UnitE;
        public UnitTypes UnitT(in byte idx) => UnitE(idx).MainC.UnitT;

        #endregion


        public EnvironmentE EnvironmentE(in byte idx) => CellEs(idx).EnvironmentE;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentE(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentE(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentE(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentE(idx).HillC;
        public ref ResourcesC WaterOnCellC(in byte idx) => ref EnvironmentE(idx).FertilizeC;

        public byte GetIdxCellByXy(params byte[] xy) => _idxs[xy[0], xy[1]];


        #endregion


        public EntitiesModel(in DataFromViewC dataFromViewC, in string nameRpcMethod, in List<object> actions, in TestModeTypes testModeT)
        {
            CommonGameE = new CommonGameE(dataFromViewC, testModeT, DateTime.Now, actions, nameRpcMethod);

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT] = new PlayerInfoE();
            }


            _cellEs = new CellEs[IndexCellsValues.CELLS];
            var xys = new List<byte[]>();

            byte idxCell = 0;
            for (byte x = 0; x < IndexCellsValues.X_AMOUNT; x++)
                for (byte y = 0; y < IndexCellsValues.Y_AMOUNT; y++)
                {
                    _idxs[x, y] = idxCell;
                    xys.Add(new byte[] { x, y });
                    idxCell++;
                }

            for (idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
            {
                _cellEs[idxCell] = new CellEs(dataFromViewC, dataFromViewC.IdCell(idxCell), idxCell, this, xys[idxCell]);
            }




            for (byte startCellIdx_0 = 0; startCellIdx_0 < IndexCellsValues.CELLS; startCellIdx_0++)
            {
                if (CellEs(startCellIdx_0).CellE.CellC.IsBorder) continue;

                _cellsAround[startCellIdx_0] = new byte[(byte)DirectTypes.End - 1];

                for (byte currentCellIdx_1 = 0; currentCellIdx_1 < IndexCellsValues.CELLS; currentCellIdx_1++)
                {
                    if (CellEs(currentCellIdx_1).CellE.CellC.IsBorder) continue;

                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                    {
                        if (CellAroundC(startCellIdx_0, currentCellIdx_1).DirectT == directT)
                        {
                            if (CellAroundC(startCellIdx_0, currentCellIdx_1).LevelFromCellT == DistanceFromCellTypes.First)
                            {
                                _cellsAround[startCellIdx_0][(byte)directT - 1] = currentCellIdx_1;
                                _cellsByDirect[startCellIdx_0, (byte)directT] = currentCellIdx_1;
                            }
                        }
                    }
                }
            }
        }


        internal void SetResourcesInInventory(in PlayerTypes playerT, in ResourceTypes resT, in float resources) => ResourcesInInventoryC(playerT).Set(resT, resources);

        internal void SetToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int amountToolWeapons) => HowManyToolWeaponsInInventoryC(playerT).Set(twT, levT, amountToolWeapons);
        internal void AddToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int adding = 1) => HowManyToolWeaponsInInventoryC(playerT).Add(twT, levT, adding);
        internal void SubtractToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int subtraction = 1) => HowManyToolWeaponsInInventoryC(playerT).Subtract(twT, levT, subtraction);


        internal void Dispose()
        {
            CommonGameE.Dispose();

            IsSelectedCity = default;
            HaveTreeUnit = default;
            AmountPlantedYoungForests = default;

            WeatherE.Dispose();

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                CellEs(cellIdxCurrent).Dispose();
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                PlayerInfoE(playerT).Dispose();
            }
        }
    }
}