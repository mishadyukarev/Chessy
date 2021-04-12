using UnityEngine;
namespace BehavioralPatterns.Strategy.ExampleFirst
{
    public sealed class StrategyTest : MonoBehaviour
    {
#pragma warning disable CS0649 // Полю "StrategyTest._behaviour" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private BaseBehaviour _behaviour;
#pragma warning restore CS0649 // Полю "StrategyTest._behaviour" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        private void Update()
        {
            _behaviour.Behaviour(transform);
        }
    }
}
