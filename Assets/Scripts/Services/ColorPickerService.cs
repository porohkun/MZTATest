using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MZTATest.Services
{
    public class ColorPickerService : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _panel;
        [SerializeField]
        private Image _prefab;

        private Action<Color> _selectColorCallback;

        private void Awake()
        {
            gameObject.SetActive(false);

            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 12; x++)
                {
                    var color = GetColor(x, y);
                    var rect = Instantiate(_prefab, _panel);
                    rect.gameObject.SetActive(true);
                    rect.color = color;
                    rect.GetComponent<Button>().onClick.AddListener(() => ColorSelected(color));
                }
        }

        public Color GetRandomColor()
        {
            return GetColor(UnityEngine.Random.Range(0, 12), UnityEngine.Random.Range(0, 5));
        }

        private Color GetColor(int x, int y)
        {
            var clearColor = Highlight(GetClearColor(x), y);
            return new Color(clearColor.x, clearColor.y, clearColor.z);
        }

        private Vector3 GetClearColor(float x)
        {
            if (x < 0f)
                throw new ArgumentException();
            if (x < 2f)
                return new Vector3(1f, Mathf.Lerp(0f, 1f, x / 2f), 0f);
            if (x < 4f)
                return new Vector3(Mathf.Lerp(1f, 0f, x / 2f - 1f), 1f, 0f);
            if (x < 6f)
                return new Vector3(0f, 1f, Mathf.Lerp(0f, 1f, x / 2f - 2f));
            if (x < 8f)
                return new Vector3(0f, Mathf.Lerp(1f, 0f, x / 2f - 3f), 1f);
            if (x < 10f)
                return new Vector3(Mathf.Lerp(0f, 1f, x / 2f - 4f), 0f, 1f);
            if (x < 12f)
                return new Vector3(1f, 0f, Mathf.Lerp(1f, 0f, x / 2f - 5f));
            return Vector3.one;
        }

        private Vector3 Highlight(Vector3 color, float y)
        {
            if (y < 0f)
                throw new ArgumentException();
            if (y < 2f)
                return Vector3.Lerp(Vector3.one, color, (y + 1) / 3f);
            if (y < 5f)
                return Vector3.Lerp(color, Vector3.zero, (y + 1) / 3f - 1f);
            return Vector3.one;
        }

        public void ShowColorPicker(Action<Color> callback)
        {
            _selectColorCallback = callback;

            gameObject.SetActive(true);

            var pos = Input.mousePosition.ToVector2XY() - (_panel.sizeDelta * 0.5f).Ceil();
            var rt = transform as RectTransform;
            _panel.anchoredPosition = new Vector2(Mathf.Clamp(pos.x, 0f, rt.sizeDelta.x - _panel.sizeDelta.x), Mathf.Clamp(pos.y, 0f, rt.sizeDelta.y - _panel.sizeDelta.y));
        }

        public void CloseColorPicker()
        {
            _selectColorCallback = null;
            gameObject.SetActive(false);
        }

        private void ColorSelected(Color color)
        {
            _selectColorCallback?.Invoke(color);
            CloseColorPicker();
        }
    }
}
