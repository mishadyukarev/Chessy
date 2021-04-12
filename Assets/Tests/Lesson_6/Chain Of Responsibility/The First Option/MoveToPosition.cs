using System.Collections;
using UnityEngine;

namespace ChainOfResponsibility
{
    public sealed class MoveToPosition : GameHandler
    {
#pragma warning disable CS0649 // Полю "MoveToPosition._positionToMove" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию .
        [SerializeField] private Vector3 _positionToMove;
#pragma warning restore CS0649 // Полю "MoveToPosition._positionToMove" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию .
#pragma warning disable CS0649 // Полю "MoveToPosition._speed" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию 0.
        [SerializeField] private float _speed;
#pragma warning restore CS0649 // Полю "MoveToPosition._speed" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию 0.
        private bool _moveToPosition;
        private IEnumerator StartMoving()
        {
            while ((transform.position - _positionToMove).sqrMagnitude > 0.0f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
               _positionToMove, Time.deltaTime * _speed);
                yield return null;
            }
            base.Handle();
        }
        public override IGameHandler Handle()
        {
            StartCoroutine(StartMoving());
            return this;
        }
    }
}

