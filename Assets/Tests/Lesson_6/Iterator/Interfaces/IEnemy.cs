using System.Collections;
using System.Collections.Generic;
namespace Asteroids.Iterator
{
    internal interface IEnemy
    {
        IAbility this[int index] { get; }
        string this[Target index] { get; }
        int MaxDamage { get; }
        IEnumerable<IAbility> GetAbility();
        IEnumerator GetEnumerator();
        IEnumerable<IAbility> GetAbility(DamageType index);
    }
}
