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
    public class Workspace
    {
        [JsonIgnore]
        public IEnumerable<Block> Blocks => _blocks;
        [JsonIgnore]
        public string Path { get; set; }
        [JsonIgnore]
        public bool Edited { get; set; }

        public event Action<Block> BlockAdded;
        public event Action<Block> BlockRemoved;

        [JsonProperty(nameof(Blocks))]
        private List<Block> _blocks = new List<Block>();

        public Workspace()
        {
            AddBlock(new Block()
            {
                Title = "TestBlock",
                Color = Color.red,
                Position = new Vector2(200, 200)
            });
            AddBlock(new Block()
            {
                Title = "TestBlock2",
                Color = Color.green,
                Position = new Vector2(500, 200)
            });
            AddBlock(new Block()
            {
                Title = "TestBlock3",
                Color = Color.cyan,
                Position = new Vector2(800, 200)
            });
            AddBlock(new Block()
            {
                Title = "TestBlock4",
                Color = Color.yellow,
                Position = new Vector2(500, 500)
            });
        }

        public void AddBlock(Block block)
        {
            if (!_blocks.Contains(block))
            {
                _blocks.Add(block);
                block.Edited += () => Edited = true;
                BlockAdded?.Invoke(block);
            }
        }

        public void RemoveBlock(Block block)
        {
            if (_blocks.Remove(block))
                BlockRemoved?.Invoke(block);
        }
    }
}
