using Chessy.Model;
using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.Entity;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.View.UI.Entity
{
    public sealed class EntitiesView
    {
        readonly CellVEs[] _cellVEs;
        readonly AudioSourceVC[] _sounds0 = new AudioSourceVC[(byte)ClipTypes.End];
        readonly AudioSourceVC[] _sounds1 = new AudioSourceVC[(byte)AbilityTypes.End];

        public readonly GameObjectVC MainGOC;
        public readonly GameObjectVC Background;
        public readonly UnityEventC UnityEventC;
        public readonly CameraVC CameraVC;
        public readonly GameObjectVC GameZoneGOC;
        public readonly PhotonVC PhotonC;

        public CellVEs CellEs(in byte idx) => _cellVEs[idx];
        public AudioSourceVC SoundASC(in ClipTypes clip) => _sounds0[(byte)clip];
        public AudioSourceVC SoundASC(in AbilityTypes clip) => _sounds1[(byte)clip];


        public EntitiesView(out DataFromViewC dataFromViewC, in Transform main, in TestModeTypes testModeT, out List<object> actions)
        {

            MainGOC = new GameObjectVC(main.gameObject);

            var camera = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<Camera>("Camera+"), main.position, main.rotation);
            camera.name = "Camera";
            camera.orthographicSize = 5.7f;

            CameraVC = new CameraVC(camera);






            var parent = new GameObject("AudioSources");

            var goES = new GameObject("EventSystem");
            UnityEventC = new UnityEventC(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>());


            GameZoneGOC = new GameObjectVC(new GameObject());







            var photonView_Rpc = new GameObject("PhotonView_Rpc");

            var photonV = photonView_Rpc.AddComponent<PhotonView>();

            /*if (PhotonNetwork.IsMasterClient)*/ //PhotonNetwork.AllocateViewID(photonV);
            photonV.ViewID = 1001;
            PhotonC = new PhotonVC(photonV, out actions);



            var backGroundGO = GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>("Background+"), main.position + new Vector3(7, 5.5f, 2), main.rotation);
            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            Background = new GameObjectVC(backGroundGO);





            GameZoneGOC.GameObject = new GameObject(Names.GAME);

            var parCells = new GameObject("Cells");
            parCells.transform.SetParent(GameZoneGOC.Transform);

            byte currentCellIdx = 0;

            var cells = new GameObject[IndexCellsValues.CELLS];
            var isBorder = new bool[IndexCellsValues.CELLS];

            for (byte x = 0; x < IndexCellsValues.X_AMOUNT; x++)
                for (byte y = 0; y < IndexCellsValues.Y_AMOUNT; y++)
                {
                    var cell = GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>("CellPrefab+"), MainGOC.Transform.position + new Vector3(x, y, MainGOC.Transform.position.z), MainGOC.Transform.rotation);
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


            var idCells = new int[IndexCellsValues.CELLS];
            var possitionsCells = new Vector3[IndexCellsValues.CELLS];

            var animationsCells = new Dictionary<byte, Action[]>();
            var animationsCellsDirected = new Action<byte>[(byte)CellAnimationDirectlyTypes.End];

            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                _cellVEs[cellIdxStart] = new CellVEs(cells[cellIdxStart]);

                idCells[cellIdxStart] = CellEs(cellIdxStart).BoxCollider2D.gameObject.GetInstanceID();
                possitionsCells[cellIdxStart] = CellEs(cellIdxStart).CellParentGOC.Transform.position;

                var animations = new Action[(byte)AnimationCellTypes.End];

                animations[(byte)AnimationCellTypes.AdultForest] = CellEs(cellIdxStart).EnvironmentVEs.AnimationC.Play;
                animations[(byte)AnimationCellTypes.JumpAppearanceUnit] = CellEs(cellIdxStart).UnitEs.AnimationUnitC.Play;
                animations[(byte)AnimationCellTypes.CircularAttackKing] = CellEs(cellIdxStart).UnitEs.CircularAttackAnimC.Play;

                animationsCellsDirected[(byte)CellAnimationDirectlyTypes.AddingWaterUnit] = (byte cellIdx) => { CellEs(cellIdx).UnitEs.AddingWaterAnimationC.Play(); };

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

            aSParent.transform.SetParent(GameZoneGOC.Transform);


            AudioSource aS2 = default;
            var sounds0 = new Action[(byte)ClipTypes.End];

            for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            {
                aS2 = aSParent.AddComponent<AudioSource>();
                aS2.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                aS2.volume = VolumesSounds.Volume(clipT, testModeT);
                if (clipT == ClipTypes.Background2)
                {
                    aS2.Play();
                    aS2.loop = true;
                }


                _sounds0[(byte)clipT] = new AudioSourceVC(aS2);
                sounds0[(byte)clipT] = aS2.Play;
            }

            var sounds1 = new Action[(byte)AbilityTypes.End];

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                aS2 = aSParent.AddComponent<AudioSource>();
                aS2.clip = UnityEngine.Resources.Load<AudioClip>("Unique/" + unique.ToString());

                _sounds1[(byte)unique] = new AudioSourceVC(aS2);
                sounds1[(byte)unique] = aS2.Play;

                aS2.volume = VolumesSounds.Volume(unique);
            }


            dataFromViewC = new DataFromViewC((sounds0, sounds1, isBorder, idCells, possitionsCells, animationsCells, animationsCellsDirected));
        }
    }
}