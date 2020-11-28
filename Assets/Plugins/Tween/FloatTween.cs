using System;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Object used to tween float values.
    /// </summary>
    public class FloatTween : Tween<float>
    {
        private static float LerpFloat(ITween<float> t, float start, float end, float progress) { return start + (end - start) * progress; }
        private static readonly Func<ITween<float>, float, float, float, float> LerpFunc = LerpFloat;

        /// <summary>
        /// Initializes a new FloatTween instance.
        /// </summary>
        public FloatTween() : base(LerpFunc) { }
    }

}