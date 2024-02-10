using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Test
{
    internal class Customers : MonoBehaviour
    {
        public readonly Customer Customer1 = new();
        public readonly Customer Customer2 = new();
        public readonly Customer Customer3 = new();
    }



    internal class CheckingLoopPerfomance : MonoBehaviour
    {
        const int _ = 200000;
        readonly Customers _customers = new();
        readonly Customer _customer = new();

        readonly List<int> _elapses = new();


        void Start()
        {
        }

        void Update()
        {
            var startTime = DateTime.Now;

            for (int j = 0; j < _; j++)
            {
                //var age = _customers.Customer1.Age;
                var age = _customer.Age;
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