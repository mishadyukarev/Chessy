using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class DonerUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (WhoseMoveC.IsMyMove)
            {
                DonerUICom.DisableWait();
                DonerUICom.SetColor(Color.white);
            }
            else
            {
                DonerUICom.EnableWait();
                DonerUICom.SetColor(Color.red);
            }
        }
    }
}