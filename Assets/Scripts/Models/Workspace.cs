using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Models
{
    public class Workspace
    {
        public IEnumerable<Block> Blocks => _blocks;

        private List<Block> _blocks = new List<Block>();

        public Workspace()
        {
            _blocks.Add(new Block()
            {
                Title = "TestBlock",
                Color = Color.red,
                Position = new Vector2(200, 200)
            });
            _blocks.Add(new Block()
            {
                Title = "TestBlock2",
                Color = Color.green,
                Position = new Vector2(500, 200)
            });
            _blocks.Add(new Block()
            {
                Title = "TestBlock3",
                Color = Color.cyan,
                Position = new Vector2(800, 200)
            });
            _blocks.Add(new Block()
            {
                Title = "TestBlock4",
                Color = Color.yellow,
                Position = new Vector2(500, 500)
            });
        }
    }
}
