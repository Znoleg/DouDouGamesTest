using Assets.Scripts.UI.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Elements.Base
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField]
        private List<CloseWindowButton> _defaultCloseButtons;

        public event Action OnOpen;
        public event Action OnClose;

        protected virtual void Awake() => AssignButtonsHandler();

        public virtual void Open()
        {
            gameObject.SetActive(true);
            OnOpen?.Invoke();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            OnClose?.Invoke();
        }

        private void AssignButtonsHandler()
        {
            foreach (CloseWindowButton closeButton in _defaultCloseButtons)
            {
                closeButton.Button.onClick.AddListener(Close);
                closeButton.Button.onClick.AddListener(closeButton.NextOpenWindow.Open);
            }
        }
    }
}