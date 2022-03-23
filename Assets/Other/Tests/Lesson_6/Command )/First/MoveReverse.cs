using UnityEngine;

namespace Asteroids.Command.First
{
    internal sealed class MoveReverse : ICommand
    {
        private readonly float _moveDistance;
        public bool Succeeded { get; private set; }
        public MoveReverse(float moveDistance)
        {
            _moveDistance = moveDistance;
        }
        public bool Execute(Transform box)
        {
            box.Translate(-box.forward * _moveDistance);
            Succeeded = true;
            return Succeeded;
        }
        public void Undo(Transform box)
        {
            box.Translate(box.forward * _moveDistance);
        }
    }
}
