using System;
using System.Collections.Generic;

namespace ECS
{
    public sealed class WorldEcs
    {
        readonly Dictionary<int, Entity> _ents;

        public WorldEcs(in Dictionary<int, Entity> ents) => _ents = ents;

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
        readonly Dictionary<int, int> _keysForComponents;

        public Entity(in int numberEnt)
        {
            _numberEnt = numberEnt;
            _keysForComponents = new Dictionary<int, int>();
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

    public struct ComponentPool<C> where C : struct
    {
        static C[] _components;

        public static void AddComponent(int idx, in C component)
        {
            if (_components == default) _components = new C[0];

            Array.Resize(ref _components, _components.Length + 1);

            _components[idx] = component;
        }

        public static ref C Component(int idx)
        {
            return ref _components[idx];
        }
    }
}