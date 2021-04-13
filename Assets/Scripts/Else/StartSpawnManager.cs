using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Main;

internal class StartSpawnManager
{
    private GameObject _parentScriptsGO;
    private AudioSource _audioSource;
    private GameObject[,] _cellsGO;

    internal GameObject ParentScriptsGO => _parentScriptsGO;
    internal AudioSource AudioSource => _audioSource;
    internal GameObject[,] CellsGO => _cellsGO;


    internal StartSpawnManager(SupportManager supportManager, out Transform parentTransformScrips)
    {
        _audioSource = supportManager.BuilderManager.CreateGameObject
            ("AudioSource", new Type[] { typeof(AudioSource) }).GetComponent<AudioSource>();
        _audioSource.clip = supportManager.ResourcesLoadManager.AudioClip;

        _parentScriptsGO = supportManager.BuilderManager.CreateGameObject("Scripts");
        parentTransformScrips = _parentScriptsGO.transform;

        SpawnCells(supportManager.ResourcesLoadManager, supportManager.StartValuesConfig);
    }


    public void SpawnCells(ResourcesLoadManager resourcesLoadManager, StartValuesConfig startValues)
    {
        var cellGO = resourcesLoadManager.CellGO;
        var whiteCellSR = resourcesLoadManager.WhiteCellSprite;
        var blackCellSR = resourcesLoadManager.BlackCellSprite;

        _cellsGO = new GameObject[startValues.CellCountX, startValues.CellCountY];

        GameObject supportParent = new GameObject("Cells");
        // Setting cells on the map
        for (int x = 0; x < startValues.CellCountX; x++)
        {
            for (int y = 0; y < startValues.CellCountY; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, blackCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        _cellsGO[x, y] = CreatGameObject(cellGO, whiteCellSR, x, y);
                        SetActive(_cellsGO[x, y], x, y);
                    }
                }

                _cellsGO[x, y].transform.SetParent(supportParent.transform);
            }
        }
    }

    private GameObject CreatGameObject(GameObject go, Sprite sprite, int x, int y)
    {
        var mainGO = Instance.gameObject;

        var goo = GameObject.Instantiate(go, mainGO.transform.position + new Vector3(x, y, mainGO.transform.position.z), mainGO.transform.rotation);

        var SR = goo.GetComponent<SpriteRenderer>();
        SR.sprite = sprite;

        return goo;
    }

    private void SetActive(GameObject go, int x, int y)
    {
        if (x >= 0 && y == 0 || x >= 0 && y == 11 ||
        x == 0 && y >= 0 || x == 14 && y >= 0 ||
        x == 1 && y == 1 || x == 2 && y == 1 || x == 12 && y == 1 || x == 13 && y == 1 ||
        x == 1 && y == 10 || x == 2 && y == 10 || x == 12 && y == 10 || x == 13 && y == 10)
            go.SetActive(false);
    }
}
