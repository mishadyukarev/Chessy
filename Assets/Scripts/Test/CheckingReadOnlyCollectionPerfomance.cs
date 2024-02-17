using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace CheckingReadOnlyCollectionPerfomance
{
    public sealed class Customer
    {
        public int Age = 5;
    }


    public class CheckingReadOnlyCollectionPerfomance : MonoBehaviour
    {
        readonly Customer[] _customers_ar = new Customer[100];
        ReadOnlyCollection<Customer> _customer_roc;

        readonly List<int> _elapses = new();

        void Start()
        {
            _customers_ar[0] = new Customer();

            _customer_roc = new ReadOnlyCollection<Customer>(_customers_ar);
        }

        void Update()
        {
            var startTime = DateTime.Now;

            for (int j = 0; j < 1000000; j++)
            {
                //var age = _customers_ar[0].Age;
                var age = _customer_roc[0].Age;
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