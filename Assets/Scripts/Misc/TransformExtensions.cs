﻿using System.Collections.Generic;

namespace UnityEngine
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
                yield return transform.GetChild(i);
        }
    }
}
