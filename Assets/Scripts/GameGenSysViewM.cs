using Leopotam.Ecs;
using Scripts.Common;
using System;
using UnityEngine.Events;

namespace Scripts.Game
{
    public sealed class GameGenSysViewM : SystemAbstManager
    {
        public GameGenSysViewM(EcsWorld gameWorld, EcsSystems allSystems) : base(gameWorld, allSystems)
        {
            
        }
    }
}