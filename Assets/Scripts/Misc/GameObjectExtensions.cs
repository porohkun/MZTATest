using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static bool IsPointerOverUIObject(this GameObject gameObject)
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Any(r => r.gameObject.transform.IsChildOf(gameObject.transform));
        }
    }
}
