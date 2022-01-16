﻿using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellEnvVEs
    {
        static Dictionary<EnvTypes, Entity[]> _envs;

        public static ref C EnvCellVC<C>(in EnvTypes env, in byte idx) where C : struct, IEnvCellV => ref _envs[env][idx].Get<C>();

        public CellEnvVEs(in EcsWorld gameW, in GameObject[] cells)
        {
            _envs = new Dictionary<EnvTypes, Entity[]>();

            for (var env = EnvTypes.First; env < EnvTypes.End; env++)
            {
                _envs.Add(env, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }

            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                var parentGO = cells[idx].transform.Find("Environments").gameObject;

                _envs[EnvTypes.Fertilizer][idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>()));

                _envs[EnvTypes.YoungForest][idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>()));

                _envs[EnvTypes.AdultForest][idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>()));

                _envs[EnvTypes.Hill][idx] = gameW.NewEntity()
                   .Add(new SpriteRendererVC(parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>()));

                _envs[EnvTypes.Mountain][idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>()));



                //_cells[CellEntTypes.Env][idx] = gameW.NewEntity()
                //.Add(new EnvVC(cells[idx]));
            }
        }
    }

    public interface IEnvCellV { }
}