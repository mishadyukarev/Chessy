using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Command.Second
{
    internal sealed class UserInterface : MonoBehaviour
    {
#pragma warning disable CS0649 // Полю "UserInterface._panelOne" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private PanelOne _panelOne;
#pragma warning restore CS0649 // Полю "UserInterface._panelOne" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
#pragma warning disable CS0649 // Полю "UserInterface._panelTwo" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private PanelTwo _panelTwo;
#pragma warning restore CS0649 // Полю "UserInterface._panelTwo" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        private readonly Stack<StateUI> _stateUi = new Stack<StateUI>();
        private BaseUi _currentWindow;
        private void Start()
        {
            _panelOne.Cancel();
            _panelTwo.Cancel();
        }
        private void Execute(StateUI stateUI, bool isSave = true)
        {
            if (_currentWindow != null)
            {
                _currentWindow.Cancel();
            }
            switch (stateUI)
            {
                case StateUI.PanelOne:
                    _currentWindow = _panelOne;
                    break;
                case StateUI.PanelTwo:
                    _currentWindow = _panelTwo;
                    break;
                default:
                    break;
            }
            _currentWindow.Execute();
            if (isSave)
            {
                _stateUi.Push(stateUI);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Execute(StateUI.PanelOne);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Execute(StateUI.PanelTwo);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_stateUi.Count > 0)
                {
                    Execute(_stateUi.Pop(), false);
                }
            }
        }
    }
}
