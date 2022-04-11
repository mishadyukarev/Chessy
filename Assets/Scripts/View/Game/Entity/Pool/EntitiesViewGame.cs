using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Common.Entity.View;
using Chessy.Game.Entity.View.Cell;
using Chessy.Game.Entity.View.Cell.Unit.Effect;
using Chessy.Game.Values;
using Chessy.Game.View.Entity;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class EntitiesViewGame
    {
        readonly AudioSourceVC[] _sounds0 = new AudioSourceVC[(byte)ClipTypes.End];
        readonly AudioSourceVC[] _sounds1 = new AudioSourceVC[(byte)AbilityTypes.End];

        readonly CellVEs[] _cellVEs;
        public CellVEs CellEs(in byte idx) => _cellVEs[idx];
        public CellBuildingVEs BuildingEs(in byte idx) => CellEs(idx).BuildingEs;
        public SpriteRendererVC BuildingE(in byte idx, in BuildingTypes buildT) => BuildingEs(idx).Main(buildT);
        public UnitVEs UnitEs(in byte idx) => CellEs(idx).UnitVEs;
        public SpriteRendererVC UnitE(in byte idx, in bool isSelected, in LevelTypes levT, in UnitTypes unitT) => UnitEs(idx).UnitE(isSelected, levT, unitT);
        public EffectVEs UnitEffectVEs(in byte idx) => UnitEs(idx).EffectVEs;
        public EnvironmentVEs EnvironmentVEs(in byte idx) => CellEs(idx).EnvironmentVEs;
        public SpriteRendererVC EnvironmentVE(in byte idx, in EnvironmentTypes envT) => EnvironmentVEs(idx).EnvironmentE(envT);

        public AudioSourceVC SoundV(in ClipTypes clip) => _sounds0[(byte)clip];
        public AudioSourceVC SoundV(in AbilityTypes clip) => _sounds1[(byte)clip];


        public EntitiesViewGame(out List<object> forData, in EntitiesViewCommon eVCommon)
        {
            eVCommon.ToggleZoneGOC.GameObject = new GameObject(NameConst.GAME);

            var genZone = new GameObject("GeneralZone");
            genZone.transform.SetParent(eVCommon.ToggleZoneGOC.Transform);


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


            var isActiveParenCells = new bool[StartValues.CELLS];
            var idCells = new int[StartValues.CELLS];

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
            {
                isActiveParenCells[idx] = CellEs(idx).CellParent.IsActiveSelf;
                idCells[idx] = CellEs(idx).CellGO.InstanceID;
            }





            var aSParent = new GameObject("AudioSource");

            aSParent.transform.SetParent(genZone.transform);


            AudioSource aS = default;
            var sounds0 = new Dictionary<ClipTypes, Action>();

            for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            {
                aS = aSParent.AddComponent<AudioSource>();
                aS.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                aS.volume = StartValues.Volume(clipT);
                if (clipT == ClipTypes.Background2)
                {
                    aS.Play();
                    aS.loop = true;
                }


                _sounds0[(byte)clipT] = new AudioSourceVC(aS);
                sounds0.Add(clipT, aS.Play);
            }

            var sounds1 = new Dictionary<AbilityTypes, Action>();

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                aS = aSParent.AddComponent<AudioSource>();
                aS.clip = Resources.Load<AudioClip>("Unique/" + unique.ToString());

                _sounds1[(byte)unique] = new AudioSourceVC(aS);
                sounds1.Add(unique, aS.Play);

                aS.volume = StartValues.Volume(unique);
            }


            forData = new List<object>();
            forData.Add(sounds0);
            forData.Add(sounds1);
            forData.Add(isActiveParenCells);
            forData.Add(idCells);
        }
    }
}