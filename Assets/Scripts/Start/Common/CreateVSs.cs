using Leopotam.Ecs;
using UnityEngine;

namespace Game.Common
{
    public sealed class CreateVSs
    {
        public CreateVSs(EcsSystems comSysts, GameObject main)
        {
            comSysts
                .Add(main.AddComponent<PhotonSceneSys>());
        }
    }
}
