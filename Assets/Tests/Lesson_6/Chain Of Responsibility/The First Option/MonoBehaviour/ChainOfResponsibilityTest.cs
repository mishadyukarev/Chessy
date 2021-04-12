using System.Collections.Generic;
using UnityEngine;

namespace ChainOfResponsibility
{
    public sealed class ChainOfResponsibilityTest : MonoBehaviour
    {
#pragma warning disable CS0649 // Полю "ChainOfResponsibilityTest._gameHandlers" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private List<GameHandler> _gameHandlers;
#pragma warning restore CS0649 // Полю "ChainOfResponsibilityTest._gameHandlers" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        private void Start()
        {
            _gameHandlers[0].Handle();
            for (var i = 1; i < _gameHandlers.Count; i++)
            {
                _gameHandlers[i - 1].SetNext(_gameHandlers[i]);
            }
        }
    }
}