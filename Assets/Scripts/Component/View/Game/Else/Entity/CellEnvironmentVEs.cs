using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct CellEnvironmentVEs
    {
        readonly Dictionary<EnvironmentTypes, Entity> _envs;

        public ref SpriteRendererVC SR(in EnvironmentTypes env) => ref _envs[env].Get<SpriteRendererVC>();

        public CellEnvironmentVEs(in GameObject cell, in EcsWorld gameW)
        {
            _envs = new Dictionary<EnvironmentTypes, Entity>();

            var parentGO = cell.transform.Find("Environments").gameObject;

            _envs[EnvironmentTypes.Fertilizer] = gameW.NewEntity()
                .Add(new SpriteRendererVC(parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>()));

            _envs[EnvironmentTypes.YoungForest] = gameW.NewEntity()
                .Add(new SpriteRendererVC(parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>()));

            _envs[EnvironmentTypes.AdultForest] = gameW.NewEntity()
                .Add(new SpriteRendererVC(parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>()));

            _envs[EnvironmentTypes.Hill] = gameW.NewEntity()
               .Add(new SpriteRendererVC(parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>()));

            _envs[EnvironmentTypes.Mountain] = gameW.NewEntity()
                .Add(new SpriteRendererVC(parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>()));
        }
    }
}