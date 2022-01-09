using System;
using System.Collections.Generic;

namespace ECS
{
    public sealed class WorldEcs
    {
        readonly Dictionary<int, Entity> _ents;

        public WorldEcs() => _ents = new Dictionary<int, Entity>();
        ~WorldEcs() => _ents.Clear();

        public Entity NewEntity()
        {
            var idx = _ents.Count;
            _ents.Add(idx, new Entity(_ents.Count));
            return _ents[idx];
        }
    }

    public readonly struct Entity
    {
        readonly int _numberEnt;

        internal Entity(in int numberEnt)
        {
            _numberEnt = numberEnt;
        }

        public Entity Add<C>(in C component) where C : struct
        {
            ComponentPool<C>.AddComponent(_numberEnt, component);
            return this;
        }
        public ref C Get<C>() where C : struct
        {
            return ref ComponentPool<C>.Component(_numberEnt);
        }
    }

    struct ComponentPool<C> where C : struct
    {
        static C[] _components;
        static Dictionary<int, int> _numbers;

        static ComponentPool()
        {
            _numbers = new Dictionary<int, int>();
        }

        internal static void AddComponent(in int idxEnt, in C component)
        {
            var key = idxEnt;

            if (_numbers.ContainsKey(key))
            {
                _components[_numbers[key]] = component;
            }

            else
            {
                var idx = 0;
                foreach (var item in _numbers) idx++;

                _numbers[key] = idx;

                if (_components == default)
                {
                    _components = new C[1];
                }
                else
                {
                    idx = _components.Length;
                    Array.Resize(ref _components, idx + 1);
                }

                _components[idx] = component;
            }
        }

        internal static ref C Component(int idxEnt) => ref _components[_numbers[idxEnt]];
    }
}