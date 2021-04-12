using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Command.Second
{
    internal abstract class BaseUi : MonoBehaviour
    {
        public abstract void Execute();
        public abstract void Cancel();
        //public abstract void Repeat();
        //....
    }
}
