using UnityEngine;

namespace BehavioralPatterns.Strategy.ExampleFirst
{
    public abstract class BaseBehaviour : ScriptableObject
    {
        [SerializeField] protected float _speed;
        public abstract void Behaviour(Transform value);
    }
}
