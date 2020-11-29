using MZTATest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZTATest.Services
{
    public class BlocksSelectionService
    {
        private List<Block> _selectedBlocks = new List<Block>();

        public bool IsBlockSelected(Block block)
        {
            return _selectedBlocks.Contains(block);
        }

        public void AutoSelect(Block block, bool shift)
        {
            var selected = IsBlockSelected(block);
            if (shift)
            {
                if (selected)
                    _selectedBlocks.Remove(block);
                else
                    _selectedBlocks.Add(block);
            }
            else
            {
                if (selected && _selectedBlocks.Count > 1)
                    selected = false;
                _selectedBlocks.Clear();
                if (!selected)
                    _selectedBlocks.Add(block);
            }
        }

        public void Select(Block block, bool shift)
        {
            if (!shift)
                _selectedBlocks.Clear();
            if (!IsBlockSelected(block))
                _selectedBlocks.Add(block);
        }
    }
}
