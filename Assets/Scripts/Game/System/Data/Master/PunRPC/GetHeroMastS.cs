using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public class GetHeroMastS : IEcsRunSystem
    {
        public void Run()
        {
            HeroInvC.SetHero(WhoseMoveC.WhoseMove, ForGetHeroMasC.Unit);
        }
    }
}