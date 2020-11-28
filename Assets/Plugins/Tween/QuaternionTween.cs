using System;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Object used to tween Quaternion values.
    /// </summary>
    public class QuaternionTween : Tween<Quaternion>
    {
        private static Quaternion LerpQuaternion(ITween<Quaternion> t, Quaternion start, Quaternion end, float progress) { return Quaternion.Lerp(start, end, progress); }
        private static readonly Func<ITween<Quaternion>, Quaternion, Quaternion, float, Quaternion> LerpFunc = LerpQuaternion;

        /// <summary>
        /// Initializes a new QuaternionTween instance.
        /// </summary>
        public QuaternionTween() : base(LerpFunc) { }
    }

}