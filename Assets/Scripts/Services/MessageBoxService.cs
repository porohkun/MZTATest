using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MZTATest.Services
{
    public class MessageBoxService : MonoBehaviour
    {
        [Serializable]
        private class ButtonData
        {
            [SerializeField]
            private Button _button;
            [SerializeField]
            private Text _text;

            public Button Button => _button;
            public Text Text => _text;
        }

        [SerializeField]
        private Text _title;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Button[] _closeButtons;
        [SerializeField]
        private ButtonData[] _buttons;

        private Action _closeButtonCallback;
        private Action[] _buttonsCallbacks;

        private void Awake()
        {
            gameObject.SetActive(false);

            foreach (var closeButton in _closeButtons)
                closeButton.onClick.AddListener(CloseButtonClicked);
            for (int i = 0; i < _buttons.Length; i++)
            {
                var ii = i;
                _buttons[i].Button.onClick.AddListener(() => ButtonClicked(ii));
            }
        }

        private void CloseButtonClicked()
        {
            gameObject.SetActive(false);
            _closeButtonCallback?.Invoke();
        }

        private void ButtonClicked(int index)
        {
            gameObject.SetActive(false);
            _buttonsCallbacks[index]?.Invoke();
        }

        public void ShowYesNoMessageBox(string title, string text, Action yesCallback, Action noCallback = null)
        {
            ShowMessageBox(title, text, noCallback, ("Да", yesCallback), ("Нет", noCallback));
        }

        public void ShowYesNoCancelMessageBox(string title, string text, Action yesCallback, Action noCallback = null, Action cancelCallback = null)
        {
            ShowMessageBox(title, text, cancelCallback, ("Да", yesCallback), ("Нет", noCallback), ("Отмена", cancelCallback));
        }

        public void ShowOkCancelMessageBox(string title, string text, Action okCallback, Action cancelCallback = null)
        {
            ShowMessageBox(title, text, cancelCallback, ("ОК", okCallback), ("Отмена", cancelCallback));
        }

        public void ShowMessageBox(string title, string text, Action closeBtnClicked, params (string, Action)[] buttons)
        {
            if (buttons.Length == 0)
                throw new ArgumentException("must be at least 1 button");
            if (buttons.Length > 3)
                throw new ArgumentException("must be 3 or less buttons");

            _title.text = title;
            _text.text = text;
            _closeButtonCallback = CloseButtonClicked;
            _buttonsCallbacks = buttons.Select(b => b.Item2).ToArray();
            for (int i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                if (buttons.Length > i)
                {
                    button.Text.text = buttons[i].Item1;
                    button.Button.gameObject.SetActive(true);
                }
                else
                    button.Button.gameObject.SetActive(false);
            }
            gameObject.SetActive(true);
        }
    }
}
