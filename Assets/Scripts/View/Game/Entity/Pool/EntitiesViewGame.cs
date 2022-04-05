using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Common.Entity.View;
using Chessy.Game.Entity.View.Cell;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using Chessy.Game.Values;
using Chessy.Game.View.Entity;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class EntitiesViewGame
    {
        readonly Dictionary<PlayerTypes, UnitVE> _kings = new Dictionary<PlayerTypes, UnitVE>();
        readonly UnitVE[] _pawnEs = new UnitVE[StartValues.CELLS]; 

        readonly CellVEs[] _cellVEs;
        public CellVEs CellEs(in byte idx) => _cellVEs[idx];
        public CellBuildingVEs BuildingEs(in byte idx) => CellEs(idx).BuildingEs;
        public SpriteRendererVC BuildingE(in byte idx, in BuildingTypes buildT) => BuildingEs(idx).Main(buildT);
        public UnitVEs UnitEs(in byte idx) => CellEs(idx).UnitVEs;
        public SpriteRendererVC UnitE(in byte idx, in bool isSelected, in LevelTypes levT, in UnitTypes unitT) => UnitEs(idx).UnitE(isSelected, levT, unitT);
        public EffectVEs UnitEffectVEs(in byte idx) => UnitEs(idx).EffectVEs;
        public EnvironmentVEs EnvironmentVEs(in byte idx) => CellEs(idx).EnvironmentVEs;
        public SpriteRendererVC EnvironmentVE(in byte idx, in EnvironmentTypes envT) => EnvironmentVEs(idx).EnvironmentE(envT);


        public readonly EntityVPool EntityVPool;

        public UnitVE KingE(in PlayerTypes playerT) => _kings[playerT];
        public UnitVE PawnE(in byte idxUnit) => _pawnEs[idxUnit];


        public EntitiesViewGame(out List<object> forData, in EntitiesViewCommon eVCommon)
        {
            eVCommon.ToggleZoneGOC.GameObject = new GameObject(NameConst.GAME);

            var genZone = new GameObject("GeneralZone");
            genZone.transform.SetParent(eVCommon.ToggleZoneGOC.Transform);



            //genZone = new Chessy.Common.Component.GameObjectVC(genZone);


            //SoundC.SavedVolume = SoundC.Volume;



            EntityVPool = new EntityVPool(out var sounds0, out var sounds1, genZone.transform);



            var parent = new GameObject("Kings").transform;
            parent.SetParent(genZone.transform);

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                var kingTran = GameObject.Instantiate(Resources.Load<Transform>("King+"));
                kingTran.SetParent(parent);
                kingTran.name = "King" + playerT;

                _kings.Add(playerT, new UnitVE(kingTran.gameObject, kingTran.Find("Selected_SR+").GetComponent<SpriteRenderer>(), kingTran.Find("NotSelected_SR+").GetComponent<SpriteRenderer>()));
            }


            parent = new GameObject("Pawns").transform;
            parent.SetParent(genZone.transform);

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                var pawnTran = GameObject.Instantiate(Resources.Load<Transform>("Pawn+"));
                pawnTran.SetParent(parent);
                pawnTran.name = "Pawn" + cell_0;

                _pawnEs[cell_0] = new UnitVE(pawnTran.gameObject, pawnTran.Find("Selected_SR+").GetComponent<SpriteRenderer>(), pawnTran.Find("NotSelected_SR+").GetComponent<SpriteRenderer>());
            }







            var parCells = new GameObject("Cells");
            parCells.transform.SetParent(genZone.transform);

            byte idx_cur = 0;

            var cells = new GameObject[StartValues.CELLS];


            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {

                    //    if(y % 2 == 0 && x % 2 != 0 || y % 2 != 0 && x % 2 == 0)
                    //{
                    //    cells[idx_cur].transform.Find("Black").gameObject.SetActive(false);
                    //}
                    //else
                    //{
                    //    cells[idx_cur].transform.Find("White").gameObject.SetActive(false);
                    //}

                    //? 
                    //: ResourceSpriteEs.Sprite(false).SpriteC.Sprite;


                    var cell = GameObject.Instantiate(Resources.Load<GameObject>("CellPrefab"), eVCommon.MainGOC.Transform.position + new Vector3(x, y, eVCommon.MainGOC.Transform.position.z), eVCommon.MainGOC.Transform.rotation);
                    cell.name = "CellMain";
                    //cell.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                            y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                            y == 1 && x == 3 || y == 1 && x == 12 ||
                            y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);

                    cells[idx_cur] = cell;

                    ++idx_cur;
                }



            _cellVEs = new CellVEs[cells.Length];
            for (byte idx_0 = 0; idx_0 < _cellVEs.Length; idx_0++)
            {
                _cellVEs[idx_0] = new CellVEs(cells[idx_0]);
            }



            new CellRiverVEs(cells);


            var isActiveParenCells = new bool[StartValues.CELLS];
            var idCells = new int[StartValues.CELLS];

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
            {
                isActiveParenCells[idx] = CellEs(idx).CellParent.IsActiveSelf;
                idCells[idx] = CellEs(idx).CellGO.InstanceID;
            }

            forData = new List<object>();
            forData.Add(sounds0);
            forData.Add(sounds1);
            forData.Add(isActiveParenCells);
            forData.Add(idCells);
        }
    }
}