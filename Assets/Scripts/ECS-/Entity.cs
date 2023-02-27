using System;
using System.Collections.Generic;

namespace ECS
{
    public sealed class EcsWorld
    {
        readonly List<object> _poolsComponents;
        readonly Dictionary<int, Entity> _ents;

        public EcsWorld()
        {
            _poolsComponents = new List<object>();
            _ents = new Dictionary<int, Entity>();
        }
        ~EcsWorld()
        {
            _ents.Clear();
            _poolsComponents.Clear();
        }

        public Entity NewEntity()
        {
            var idx = _ents.Count;
            _ents.Add(idx, new Entity(this, _ents.Count));
            return _ents[idx];
        }

        internal bool GetPool<C>(out ComponentPool<C> pool) where C : new()
        {
            for (int i = 0; i < _poolsComponents.Count; i++)
            {
                if(_poolsComponents[i] is ComponentPool<C> p)
                {
                    pool = p;
                    return true;
                }
            }

            pool = default;
            return false;
        }

        internal ComponentPool<C> AddPool<C>() where C : new()
        {
            var pool = new ComponentPool<C>(new Dictionary<int, int>());
            _poolsComponents.Add(pool);
            return pool;
        }
    }


    class ComponentPool<C> where C : new()
    {
        Dictionary<int, int> _numbersEntity;
        C[] _components;

        internal bool ContainComponent(int idxEnt) => _numbersEntity.ContainsKey(idxEnt);
        internal ref C Component(int idxEnt) => ref _components[_numbersEntity[idxEnt]];

        internal ComponentPool(in Dictionary<int, int> numbersEntity)
        {
            _numbersEntity = numbersEntity;
            _components = new C[0];
        }

        internal void AddComponent(in int idxEnt, in C component)
        {
            var idxE_key = idxEnt;
            var idxC_value = _components.Length;

            _numbersEntity.Add(idxE_key, idxC_value);
            Array.Resize(ref _components, _components.Length + 1);

            _components[idxC_value] = component;
        }
    }


    public readonly struct Entity
    {
        readonly EcsWorld _curWorld;
        readonly int _numberEnt;

        internal Entity(in EcsWorld world, in int numberEnt) : this()
        {
            _curWorld = world;
            _numberEnt = numberEnt;
        }

        public Entity Add<C>(in C component) where C : new()
        {
            if (_curWorld.GetPool<C>(out var pool))
            {
                pool.AddComponent(_numberEnt, component);
            }
            else
            {
                pool = _curWorld.AddPool<C>();
                pool.AddComponent(_numberEnt, component);
            }
            
            return this;
        }

        public ref C Get<C>() where C : new()
        {
            if (_curWorld.GetPool<C>(out var pool))
            {
                if (pool.ContainComponent(_numberEnt))
                {
                    return ref pool.Component(_numberEnt);
                }
                else
                {
                    pool.AddComponent(_numberEnt, new C());
                    return ref pool.Component(_numberEnt);
                }
            }
            else
            {
                pool = _curWorld.AddPool<C>();
                pool.AddComponent(_numberEnt, new C());

                return ref pool.Component(_numberEnt);
            }
        }
    }
}