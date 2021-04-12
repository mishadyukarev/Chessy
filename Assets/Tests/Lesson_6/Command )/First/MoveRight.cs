using UnityEngine;
namespace Asteroids.Command.First
{
    internal sealed class MoveRight : ICommand
    {
        private readonly float _moveDistance;
        public bool Succeeded { get; private set; }
        public MoveRight(float moveDistance)
        {
            _moveDistance = moveDistance;
        }
        public bool Execute(Transform box)
        {
            box.Translate(box.right * _moveDistance);
            Succeeded = true;
            return Succeeded;
        }
        public void Undo(Transform box)
        {
            box.Translate(-box.right * _moveDistance);
        }
    }
}
