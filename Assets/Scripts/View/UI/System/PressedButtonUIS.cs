using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    sealed class PressedButtonUIS : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        Action<bool> _action;
        internal void SetAction(in Action<bool> action) => _action = action;

        public void OnPointerDown(PointerEventData eventData) => _action(true);
        public void OnPointerUp(PointerEventData eventData) => _action(false);
    }
}