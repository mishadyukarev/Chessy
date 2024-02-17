using System;
using System.Collections.Generic;
using UnityEngine;


namespace Test.StaticClassSpeed
{
    //internal static class Entities
    //{
    //    public static readonly Customer Customer1 = new();
    //    public static readonly Customer Customer2 = new();
    //    public static readonly Customer Customer3 = new();
    //}

    internal class Entities
    {
        public readonly Customer Customer1 = new();
        public readonly Customer Customer2 = new();
        public readonly Customer Customer3 = new();
    }



    internal class CheckingStaticClassSpeed : MonoBehaviour
    {
        const int AMOUNT = 1000000;
        readonly Entities _entities = new();

        readonly List<int> _elapses = new();


        void Start()
        {
        }

        void Update()
        {
            var startTime = DateTime.Now;

            for (int j = 0; j < AMOUNT; j++)
            {
                var customer1 = _entities.Customer1;
                //var customer1 = Entities.Customer1;
            }


            _elapses.Add((DateTime.Now - startTime).Milliseconds);

            Debug.Log(_elapses.Count);
        }

        private void OnDestroy()
        {
            float sum = 0;
            foreach (var a in _elapses)
            {
                sum += a;
            }

            var result = sum / _elapses.Count;

            Debug.Log(result);
        }
    }
}