using System;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Object used to tween Color values.
    /// </summary>
    public class ColorTween : Tween<Color>
    {
        private static Color LerpColor(ITween<Color> t, Color start, Color end, float progress) { return Color.Lerp(start, end, progress); }
        private static readonly Func<ITween<Color>, Color, Color, float, Color> LerpFunc = LerpColor;

        /// <summary>
        /// Initializes a new ColorTween instance.
        /// </summary>
        public ColorTween() : base(LerpFunc) { }
    }

}