using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MZTATest.Controls
{
    [RequireComponent(typeof(RawImage))]
    [ExecuteInEditMode]
    public class GridController : MonoBehaviour
    {
        public Vector2 Offset { get; set; }

        private RectTransform _rectTransform;
        private RawImage _image;
        private Vector2 _scale;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
            _image = GetComponent<RawImage>();
        }

        private void Update()
        {
            if (_image == null)
                _image = GetComponent<RawImage>();
            if (_image != null)
            {
                _scale = new Vector2(_image.texture.width, _image.texture.height);
                _scale = new Vector2(1f / _image.texture.width, 1f / _image.texture.height);
                _image.uvRect = new Rect(Vector2.Scale(Offset, _scale), Vector2.Scale(_rectTransform.sizeDelta, _scale));
            }
        }
    }
}
