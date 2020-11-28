using System;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Object used to tween Vector2 values.
    /// </summary>
    public class Vector2Tween : Tween<Vector2>
    {
        private static Vector2 LerpVector2(ITween<Vector2> t, Vector2 start, Vector2 end, float progress) { return Vector2.Lerp(start, end, progress); }
        private static readonly Func<ITween<Vector2>, Vector2, Vector2, float, Vector2> LerpFunc = LerpVector2;

        /// <summary>
        /// Initializes a new Vector2Tween instance.
        /// </summary>
        public Vector2Tween() : base(LerpFunc) { }
    }

}