using MZTATest.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Services
{
    public class CursorService : MonoBehaviour
    {
        [Serializable]
        private class CursorData
        {
            [SerializeField]
            private string _name;
            [SerializeField]
            private Texture2D _texture;
            [SerializeField]
            private Vector2 _hotSpot;

            public string Name => _name;
            public Texture2D Texture => _texture;
            public Vector2 Hotspot => _hotSpot;
        }

        [SerializeField]
        private CursorData[] _cursors;

        private List<(object, CursorData)> _cursorStack = new List<(object, CursorData)>();
        private List<(object, CursorData)> _dirtyStack = new List<(object, CursorData)>();

        private List<(object, CursorData)> _cursorForcedStack = new List<(object, CursorData)>();
        private List<(object, CursorData)> _dirtyForcedStack = new List<(object, CursorData)>();

        private List<(object, CursorData)> GetCursorStack(bool forcedCursor) => forcedCursor ? _cursorForcedStack : _cursorStack;
        private List<(object, CursorData)> GetDirtyStack(bool forcedCursor) => forcedCursor ? _dirtyForcedStack : _dirtyStack;

        public void SetCursor(object key, string cursorName, bool forcedCursor = false)
        {
            if (TryGetCursor(cursorName, out var cursor))
                GetDirtyStack(forcedCursor).Add((key, cursor));
        }

        public void DropCursor(object key, bool forcedCursor = false)
        {
            var cursorIndex = GetCursorStack(forcedCursor).FindLastIndex(c => c.Item1 == key);
            if (cursorIndex >= 0)
                GetCursorStack(forcedCursor).RemoveAt(cursorIndex);
            UpdateCursor();
        }

        private void LateUpdate()
        {
            if (ApplyDirtyStack(GetDirtyStack(false), GetCursorStack(false)) ||
                ApplyDirtyStack(GetDirtyStack(true), GetCursorStack(true)))
                UpdateCursor();
        }

        private bool ApplyDirtyStack(List<(object, CursorData)> dirtyStack, List<(object, CursorData)> cursorStack)
        {
            if (dirtyStack.Count > 0)
            {
                cursorStack.AddRange(((IEnumerable<(object, CursorData)>)dirtyStack).Reverse());
                dirtyStack.Clear();
                return true;
            }
            return false;
        }

        private void UpdateCursor()
        {
            if (_cursorForcedStack.Count > 0)
                UpdateCursor(_cursorForcedStack);
            else if (_cursorStack.Count > 0)
                UpdateCursor(_cursorStack);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private void UpdateCursor(List<(object, CursorData)> cursorStack)
        {
            var cursor = cursorStack.Last().Item2;
            Cursor.SetCursor(cursor.Texture, cursor.Hotspot, CursorMode.ForceSoftware);
        }

        private bool TryGetCursor(string cursorName, out CursorData cursor)
        {
            foreach (var cur in _cursors)
                if (cur.Name == cursorName)
                {
                    cursor = cur;
                    return true;
                }
            cursor = null;
            return false;
        }
    }
}
