using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Common.Entity.View;
using Chessy.Game.Values;
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

        public AudioSourceVC SoundASC(in ClipTypes clip) => _sounds0[(byte)clip];
        public AudioSourceVC SoundASC(in AbilityTypes clip) => _sounds1[(byte)clip];


        public EntitiesViewGame(out DataFromViewC dataFromViewC, in EntitiesViewCommon eVCommon)
        {
            eVCommon.ToggleZoneGOC.GameObject = new GameObject(NameConst.GAME);

            var genZone = new GameObject("GeneralZone");
            genZone.transform.SetParent(eVCommon.ToggleZoneGOC.Transform);


            var parCells = new GameObject("Cells");
            parCells.transform.SetParent(genZone.transform);

            byte currentCellIdx = 0;

            var cells = new GameObject[StartValues.CELLS];
            var isBorder = new bool[StartValues.CELLS];

            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    var cell = GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>("CellPrefab"), eVCommon.MainGOC.Transform.position + new Vector3(x, y, eVCommon.MainGOC.Transform.position.z), eVCommon.MainGOC.Transform.rotation);
                    cell.name = "CellMain";

                    if (y == 0 || y == 10 && x >= 0 && x < 15 ||
                        y >= 1 && y < 10 && x >= 0 && x <= 2 || x >= 13 && x < 15 ||

                        y == 1 && x == 3 || y == 1 && x == 12 ||
                        y == 9 && x == 3 || y == 9 && x == 12)
                    {
                        isBorder[currentCellIdx] = true;

                        cell.SetActive(false);
                    }

                    cell.transform.SetParent(parCells.transform);

                    cells[currentCellIdx] = cell;

                    ++currentCellIdx;
                }



            _cellVEs = new CellVEs[cells.Length];

            
            var idCells = new int[StartValues.CELLS];

            var animationsCells = new Dictionary<byte, Action[]>();

            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                _cellVEs[cellIdxStart] = new CellVEs(cells[cellIdxStart]);

                idCells[cellIdxStart] = CellEs(cellIdxStart).BoxCollider2D.gameObject.GetInstanceID();


                var animations = new Action[(byte)AnimationCellTypes.End];

                animations[(byte)AnimationCellTypes.AdultForest] = CellEs(cellIdxStart).EnvironmentVEs.AnimationC.Play;
                animations[(byte)AnimationCellTypes.JumpAppearanceUnit] = CellEs(cellIdxStart).UnitEs.AnimationUnitC.Play;
                animations[(byte)AnimationCellTypes.CircularAttackKing] = CellEs(cellIdxStart).UnitEs.CircularAttackAnimC.Play;

                animationsCells.Add(cellIdxStart, animations);

                //if (isBorder[cellIdxStart])
                //{
                //    CellEs(cellIdxStart).StandartCellGO.SetActive(false);

                //    if(UnityEngine.Random.Range(0f, 1f) <= 0.5f)
                //    {
                //        CellEs(cellIdxStart).DesertCell1GOC.SetActive(true);
                //    }
                //    else
                //    {
                //        CellEs(cellIdxStart).DesertCell2GOC.SetActive(true);
                //    }

                //    CellEs(cellIdxStart).BoxCollider2D.gameObject.SetActive(false);
                //}

            }



                    var aSParent = new GameObject("AudioSource");

            aSParent.transform.SetParent(genZone.transform);


            AudioSource aS = default;
            var sounds0 = new Action[(byte)ClipTypes.End];

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
                sounds0[(byte)clipT] = aS.Play;
            }

            var sounds1 = new Action[(byte)AbilityTypes.End];

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                aS = aSParent.AddComponent<AudioSource>();
                aS.clip = UnityEngine.Resources.Load<AudioClip>("Unique/" + unique.ToString());

                _sounds1[(byte)unique] = new AudioSourceVC(aS);
                sounds1[(byte)unique] = aS.Play;

                aS.volume = StartValues.Volume(unique);
            }


            dataFromViewC = new DataFromViewC((sounds0, sounds1, isBorder, idCells, animationsCells));
        }
    }
}