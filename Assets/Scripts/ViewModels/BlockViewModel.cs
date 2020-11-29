using MZTATest.Models;
using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MZTATest.ViewModels
{
    public class BlockViewModel
    {
        public class Factory : PlaceholderFactory<Block, BlockViewModel> { }

        public string Title => _block.Title;
        public Color Color => _block.Color;
        public Vector2 Position => _block.Position - Offset;
        public Vector2 Size => _block.Size;
        public bool IsGripping => _gripTop || _gripBottom || _gripLeft || _gripRight;
        public bool Selected => _blockSelectionService.IsBlockSelected(_block);

        public Vector2 Offset { get; set; }

        private Block _block;
        private BlocksSelectionService _blockSelectionService;

        private bool _gripTop;
        private bool _gripBottom;
        private bool _gripLeft;
        private bool _gripRight;
        private Vector2 _gripStartMousePos;
        private Vector2 _gripStartBlockPos;
        private Vector2 _gripStartBlockSize;

        public BlockViewModel(Block block, BlocksSelectionService blockSelectionService)
        {
            _block = block;
            _blockSelectionService = blockSelectionService;
        }

        public void BeginGrip(bool top, bool bottom, bool left, bool right, Vector2 position)
        {
            _gripTop = top;
            _gripBottom = bottom;
            _gripLeft = left;
            _gripRight = right;
            _gripStartMousePos = position;
            _gripStartBlockPos = _block.Position;
            _gripStartBlockSize = _block.Size;
        }

        public void ContinueGrip(Vector2 position)
        {
            var offset = position - _gripStartMousePos;
            _block.Position = _gripStartBlockPos + Vector2.Scale(offset, new Vector2(_gripLeft ? 1 : 0, _gripBottom ? 1 : 0));
            _block.Size = _gripStartBlockSize + Vector2.Scale(offset, new Vector2((_gripRight ^ _gripLeft) ? (_gripLeft ? -1 : 1) : 0, (_gripTop ^ _gripBottom) ? (_gripBottom ? -1 : 1) : 0));
        }

        public void EndGrip()
        {
            _gripTop = false;
            _gripBottom = false;
            _gripLeft = false;
            _gripRight = false;
        }

        public void Select(bool shift)
        {
            _blockSelectionService.AutoSelect(_block, shift);
        }

        public void SetTitle(string text)
        {
            _block.Title = text;
        }
    }
}
