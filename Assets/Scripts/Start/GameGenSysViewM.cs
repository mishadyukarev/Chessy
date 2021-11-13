using Leopotam.Ecs;
using Chessy.Common;
using System;
using UnityEngine.Events;

namespace Chessy.Game
{
    public sealed class GameGenSysViewM : SystemAbstManager
    {
        public GameGenSysViewM(EcsWorld gameWorld, EcsSystems allSystems) : base(gameWorld, allSystems)
        {
            
        }
    }
}