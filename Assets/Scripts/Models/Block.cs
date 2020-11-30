using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Models
{
    [Serializable]
    public class Block
    {
        public event Action Edited;

        [JsonProperty(nameof(Title))]
        private string _title = "Новый Блок";

        [JsonProperty(nameof(Color))]
        private Color _color = Color.white;

        [JsonProperty(nameof(Position))]
        private Vector2 _position;

        [JsonProperty(nameof(Size))]
        private Vector2 _size = new Vector2(200, 150);

        [JsonIgnore]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        [JsonIgnore]
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
        [JsonIgnore]
        public Vector2 Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        [JsonIgnore]
        public Vector2 Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private void SetProperty<T>(ref T container, T value)
        {
            if (!container.Equals(value))
            {
                container = value;
                Edited?.Invoke();
            }
        }
    }
}
