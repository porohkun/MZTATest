using System;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Object used to tween Vector3 values.
    /// </summary>
    public class Vector3Tween : Tween<Vector3>
    {
        private static Vector3 LerpVector3(ITween<Vector3> t, Vector3 start, Vector3 end, float progress) { return Vector3.Lerp(start, end, progress); }
        private static readonly Func<ITween<Vector3>, Vector3, Vector3, float, Vector3> LerpFunc = LerpVector3;

        /// <summary>
        /// Initializes a new Vector3Tween instance.
        /// </summary>
        public Vector3Tween() : base(LerpFunc) { }
    }

}