using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public class GetHeroMastS : IEcsRunSystem
    {
        public void Run()
        {
            InvUnitsC.AddUnit(WhoseMoveC.WhoseMove, ForGetHeroMasC.Unit, LevelUnitTypes.First);
        }
    }
}