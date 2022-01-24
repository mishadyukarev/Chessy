using System;
using System.Collections.Generic;

namespace ECS
{
    public sealed class EcsWorld
    {
        readonly Dictionary<int, Entity> _ents;

        public EcsWorld() => _ents = new Dictionary<int, Entity>();
        ~EcsWorld()
        {
            _ents.Clear();
        }

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

        internal Entity(in int numberEnt) : this()
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
            if (ComponentPool<C>.ContainComponent(_numberEnt))
            {
                return ref ComponentPool<C>.Component(_numberEnt);
            }
            else
            {
                ComponentPool<C>.AddComponent(_numberEnt, new C());
                return ref ComponentPool<C>.Component(_numberEnt);
            }
        }
    }

    struct ComponentPool<C> where C : struct
    {
        static C[] _components;
        static Dictionary<int, int> _numbers;

        internal static bool ContainComponent(int idxEnt)
        {
            return _numbers.ContainsKey(idxEnt);
        }
        internal static ref C Component(int idxEnt) => ref _components[_numbers[idxEnt]];

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
    }
}