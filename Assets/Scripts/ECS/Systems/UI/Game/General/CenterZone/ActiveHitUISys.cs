using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.UI.Game.General.Center;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.CenterZone
{
    internal sealed class ActiveHitUISys : IEcsRunSystem
    {

        private bool _isStartHit = true;

        public void Run()
        {
            //ref var hintsUIDataCom = ref _hitUIFilt.Get1(0);
            //ref var hintsUIViewCom = ref _hitUIFilt.Get2(0);


            //switch (hintsUIDataCom.HintUIType)
            //{
            //    case HintUITypes.None:
            //        {

            //        }
            //        break;
            //    case HintUITypes.Farm:
            //        {

            //        }
            //        break;

            //    default:
            //        break;
            //}

            //if(hintsUIDataCom.HintUIType != HintUITypes.None)
            //{
            //    if (_isStartHit)
            //    {
            //        hintsUIViewCom.SetRandAncPos();
            //        _isStartHit = false;
            //    }
            //}
        }
    }
}
