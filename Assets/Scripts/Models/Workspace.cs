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
