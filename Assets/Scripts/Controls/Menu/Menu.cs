using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MZTATest.Controls
{
    public class Menu : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private MenuItem[] _items;

        public event Action WantBeClosed;

        private MenuItem _menuItemWithOpenedMenu;

        private void Awake()
        {
            _items = transform.GetChildren().Select(t => t.GetComponent<MenuItem>()).Where(e => e != null).ToArray();

            foreach (var item in _items)
            {
                item.Clicked += Item_Clicked;
                item.IsPointerInsideChanged += Item_IsPointerInsideChanged;
                if (item.HasSubMenu)
                    item.SubMenu.WantBeClosed += SubMenu_WantBeClosed;
            }
        }

        private void Update()
        {
            if (WantBeClosed == null)
                if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject(gameObject))
                    CloseMenu();
        }

        private static bool IsPointerOverUIObject(GameObject gameObject)
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Any(r => r.gameObject.transform.IsChildOf(gameObject.transform));
        }

        private static IEnumerable<GameObject> IterateGameObjectParents(GameObject gameObject)
        {
            do
            {
                yield return gameObject;
                gameObject = gameObject.transform.parent.gameObject;
            }
            while (gameObject.transform.parent != null);
        }

        private void SubMenu_WantBeClosed()
        {
            CloseMenu();
        }

        private void Item_IsPointerInsideChanged(MenuItem sender, bool oldValue, bool newValue)
        {
            if (newValue)
            {
                if (_menuItemWithOpenedMenu != null && _menuItemWithOpenedMenu != sender)
                {
                    _menuItemWithOpenedMenu.SetState(MenuItem.State.Normal);
                    OpenMenu(sender);
                }
                sender.SetState(MenuItem.State.Highlighted);
            }
            else
            {
                if (_menuItemWithOpenedMenu != sender)
                    sender.SetState(MenuItem.State.Normal);
            }
        }

        private void Item_Clicked(MenuItem sender)
        {
            if (sender == _menuItemWithOpenedMenu)
            {
                CloseMenu();
                sender.SetState(MenuItem.State.Highlighted);
            }
            else
            if (sender.HasSubMenu)
            {
                OpenMenu(sender);
                sender.SetState(MenuItem.State.Highlighted);
            }
            else
            {
                CloseMenu();
                sender.InvokeCommand();
                WantBeClosed?.Invoke();
            }
        }

        private void OpenMenu(MenuItem item)
        {
            CloseMenu();
            _menuItemWithOpenedMenu = item;
            item.OpenSubMenu();
        }

        private void CloseMenu()
        {
            _menuItemWithOpenedMenu?.CloseSubMenu();
            _menuItemWithOpenedMenu?.SetState(MenuItem.State.Normal);
            _menuItemWithOpenedMenu = null;
        }
    }
}
