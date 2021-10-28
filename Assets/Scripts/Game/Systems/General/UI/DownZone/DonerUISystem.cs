using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class DonerUISystem : IEcsRunSystem
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