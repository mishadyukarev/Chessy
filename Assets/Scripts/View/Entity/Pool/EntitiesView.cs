using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Model
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
        public readonly GameObjectVC ToggleZoneGOC;
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

            //for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            //{
            //    var aS = parent.AddComponent<AudioSource>();
            //    aS.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

            //    var volume = 1f;

            //    switch (clipT)
            //    {
            //        case ClipTypes.Music: volume = testMode == TestModes.Standart ? 0 : 0.2f; break;
            //        case ClipTypes.Click: volume = 0.25f; break;
            //    }

            //    aS.volume = volume;

            //    if (clipT == ClipTypes.Music)
            //    {
            //        aS.Play();
            //        aS.loop = true;
            //    }


            //    _sound.Add(clipT, new AudioSourceVC(aS));
            //    sound.Add(clipT, aS.Play);
            //}

            var goES = new GameObject("EventSystem");
            UnityEventC = new UnityEventC(goES.AddComponent<EventSystem>(), goES.AddComponent<StandaloneInputModule>());


            ToggleZoneGOC = new GameObjectVC(new GameObject());







            var photonView_Rpc = new GameObject("PhotonView_Rpc");

            var photonV = photonView_Rpc.AddComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient) PhotonNetwork.AllocateViewID(photonV);
            else photonV.ViewID = 1001;
            PhotonC = new PhotonVC(photonV, out actions);



            var backGroundGO = GameObject.Instantiate(UnityEngine.Resources.Load<GameObject>("Background+"), main.position + new Vector3(7, 5.5f, 2), main.rotation);
            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            Background = new GameObjectVC(backGroundGO);





            ToggleZoneGOC.GameObject = new GameObject(NameConst.GAME);

            var parCells = new GameObject("Cells");
            parCells.transform.SetParent(ToggleZoneGOC.Transform);

            byte currentCellIdx = 0;

            var cells = new GameObject[StartValues.CELLS];
            var isBorder = new bool[StartValues.CELLS];

            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
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

            aSParent.transform.SetParent(ToggleZoneGOC.Transform);


            AudioSource aS2 = default;
            var sounds0 = new Action[(byte)ClipTypes.End];

            for (var clipT = ClipTypes.None + 1; clipT < ClipTypes.End; clipT++)
            {
                aS2 = aSParent.AddComponent<AudioSource>();
                aS2.clip = UnityEngine.Resources.Load<AudioClip>(clipT.ToString());

                aS2.volume = StartValues.Volume(clipT, testModeT);
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

                aS2.volume = StartValues.Volume(unique);
            }


            dataFromViewC = new DataFromViewC((sounds0, sounds1, isBorder, idCells, animationsCells));
        }
    }
}