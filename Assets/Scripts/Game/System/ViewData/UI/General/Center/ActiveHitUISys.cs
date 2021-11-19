using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ActiveHitUISys : IEcsRunSystem
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
