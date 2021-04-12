using UnityEngine;

public class ECStest : MonoBehaviour
{

    #region

    //struct Component1 { }

    //class System1 : IEcsInitSystem
    //{
    //    EcsWorld _world = null;

    //    public void Init()
    //    {
    //        _world.NewEntity().Get<Component1>();
    //    }
    //}

    //class System2 : IEcsInitSystem
    //{
    //    EcsFilter<Component1> _filter = null;

    //    public void Init()
    //    {
    //        Debug.Log(_filter.GetEntitiesCount());
    //    }
    //}
    //private void Start()
    //{
    //    EcsWorld world = new EcsWorld();
    //    Debug.Log(world.ToString());

    //    var systems1 = new EcsSystems(world);
    //    var systems2 = new EcsSystems(world);
    //    systems1.Add(new System1());
    //    systems2.Add(new System2());
    //    systems1.ProcessInjects();
    //    systems2.ProcessInjects();
    //    systems1.Init();
    //    systems2.Init();
    //}

    #endregion


    //struct Struct1
    //{
    //    public string S;
    //}

    //class WeaponSystem : IEcsInitSystem, IEcsRunSystem
    //{
    //    // auto-injected fields: EcsWorld instance and EcsFilter.
    //    EcsWorld _world = null;
    //    // We wants to get entities with "WeaponComponent" and without "HealthComponent".
    //    EcsFilter<Struct1>/*.Exclude<HealthComponent>*/ _filter = null;

    //    public void Init()
    //    {
    //        _world.NewEntity().Get<Struct1>();
    //    }

    //    public void Run()
    //    {
    //        foreach (var i in _filter)
    //        {
    //            // entity that contains WeaponComponent.
    //            ref var entity = ref _filter.GetEntity(i);

    //            // Get1 will return link to attached "WeaponComponent".
    //            ref var weapon = ref _filter.Get1(i);
    //            weapon.S = "g";
    //        }
    //    }
    //}
    //private void Start()
    //{
    //    EcsWorld world = new EcsWorld();
    //    Debug.Log(world.ToString());

    //    var systems1 = new EcsSystems(world);
    //    systems1.Add(new WeaponSystem());
    //    systems1.ProcessInjects();
    //    systems1.Init();
    //    systems1.Run();

    //    EcsFilter ecsFilter = world.GetFilter(typeof(Struct1));
    //    EcsEntity ecsEntity = ecsFilter.GetEntity(0);
    //    Debug.Log(ecsEntity.Get<Struct1>().S);
    //}
}
