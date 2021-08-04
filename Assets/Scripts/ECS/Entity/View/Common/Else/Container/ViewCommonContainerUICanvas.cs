//using Leopotam.Ecs;
//using System;
//using UnityEngine;

//namespace Assets.Scripts.Workers.Common
//{
//    internal struct SysViewCommonInit.CanvasEnt_CanvasCom
//    {
//        private static EcsEntity _canvasEnt;

//        internal SysViewCommonInit.CanvasEnt_CanvasCom(EcsWorld commonWorld, Canvas CanvasFromResources)
//        {
//            var canvas = GameObject.Instantiate(CanvasFromResources);
//            canvas.name = "Canvas";
//            _canvasEnt = commonWorld.NewEntity()
//                .Replace(new CanvasComponent(canvas));
//        } 
//    }
//}
