using UnityEngine;
namespace Asteroids.Command.First
{
    internal sealed class MoveLeft : ICommand
    {
        private readonly float _moveDistance;
        public bool Succeeded { get; private set; }
        public MoveLeft(float moveDistance)
        {
            _moveDistance = moveDistance;
        }
        public bool Execute(Transform box)
        {
            box.Translate(-box.right * _moveDistance);
            Succeeded = true;
            return Succeeded;
        }
        public void Undo(Transform box)
        {
            box.Translate(box.right * _moveDistance);
        }
    }
}

