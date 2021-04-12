//using Leopotam.Ecs;
//using UnityEngine;

//class InputAndroidSystem : IEcsRunSystem
//{
//    EcsFilter<ForInput> inputEventFilter = null;
//    public void Run()
//    {

//        foreach (var i in inputEventFilter)
//        {
//            ref var inputComponent = ref inputEventFilter.Get1(i);

//            if (Input.GetMouseButton(0)) // here must be for android
//            {
//                inputComponent.isClick = true;
//            }
//            else inputComponent.isClick = false;

//        }
//    }
//}
