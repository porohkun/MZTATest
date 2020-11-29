using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Models
{
    public class Block
    {
        public string Title { get; set; } = "Новый Блок";
        public Color Color { get; set; } = Color.white;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; } = new Vector2(200, 150);

    }
}
