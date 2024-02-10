using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public sealed class Customer
    {
        public int Age = 5;
    }


    public class CheckingMethodPerfomance : MonoBehaviour
    {
        Customer[] _customer = new Customer[100];

        readonly List<int> _elapses = new();

        Customer GetCustomer(byte i) => _customer[i];


        void Start()
        {
            _customer[0] = new Customer();


        }

        void Update()
        {
            var startTime = DateTime.Now;

            for (int j = 0; j < 1000000; j++)
            {
                //var age = _customer[0].Age;
                var age = GetCustomer(0).Age;
            }


            _elapses.Add((DateTime.Now - startTime).Milliseconds);

            //Debug.Log(startTime);

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