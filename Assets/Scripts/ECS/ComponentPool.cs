//using System;

//namespace ECS
//{
//    public struct ComponentPool<C> where C : struct
//    {
//        static C[] _components;

//        public static void AddComponent(in int idx, in C component)
//        {
//            if(_components == default) _components = new C[0];

//            Array.Resize(ref _components, _components.Length + 1);

//            _components[idx] = component;
//        }

//        public static ref C Component(int idx)
//        {
//            return ref _components[idx];
//        }
//    }
//}