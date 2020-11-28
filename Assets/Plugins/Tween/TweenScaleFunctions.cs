using System;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Tween scale functions
    /// Implementations based on http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c, which are based on http://www.robertpenner.com/easing/
    /// </remarks>
    public static class TweenScaleFunctions
    {
        private const float halfPi = Mathf.PI * 0.5f;

        /// <summary>
        /// A linear progress scale function.
        /// </summary>
        public static readonly Func<float, float> Linear = linear;
        private static float linear(float progress) { return progress; }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseIn = quadraticEaseIn;
        private static float quadraticEaseIn(float progress) { return EaseInPower(progress, 2); }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseOut = quadraticEaseOut;
        private static float quadraticEaseOut(float progress) { return EaseOutPower(progress, 2); }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseInOut = quadraticEaseInOut;
        private static float quadraticEaseInOut(float progress) { return EaseInOutPower(progress, 2); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> CubicEaseIn = cubicEaseIn;
        private static float cubicEaseIn(float progress) { return EaseInPower(progress, 3); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> CubicEaseOut = cubicEaseOut;
        private static float cubicEaseOut(float progress) { return EaseOutPower(progress, 3); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> CubicEaseInOut = cubicEaseInOut;
        private static float cubicEaseInOut(float progress) { return EaseInOutPower(progress, 3); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseIn = quarticEaseIn;
        private static float quarticEaseIn(float progress) { return EaseInPower(progress, 4); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseOut = quarticEaseOut;
        private static float quarticEaseOut(float progress) { return EaseOutPower(progress, 4); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseInOut = quarticEaseInOut;
        private static float quarticEaseInOut(float progress) { return EaseInOutPower(progress, 4); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseIn = quinticEaseIn;
        private static float quinticEaseIn(float progress) { return EaseInPower(progress, 5); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseOut = quinticEaseOut;
        private static float quinticEaseOut(float progress) { return EaseOutPower(progress, 5); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseInOut = quinticEaseInOut;
        private static float quinticEaseInOut(float progress) { return EaseInOutPower(progress, 5); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> OcticEaseIn = octicEaseIn;
        private static float octicEaseIn(float progress) { return EaseInPower(progress, 8); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> OcticEaseOut = octicEaseOut;
        private static float octicEaseOut(float progress) { return EaseOutPower(progress, 8); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> OcticEaseInOut = octicEaseInOut;
        private static float octicEaseInOut(float progress) { return EaseInOutPower(progress, 8); }

        /// <summary>
        /// A sine progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> SineEaseIn = sineEaseIn;
        private static float sineEaseIn(float progress) { return Mathf.Sin(progress * halfPi - halfPi) + 1; }

        /// <summary>
        /// A sine progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> SineEaseOut = sineEaseOut;
        private static float sineEaseOut(float progress) { return Mathf.Sin(progress * halfPi); }

        /// <summary>
        /// A sine progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> SineEaseInOut = sineEaseInOut;
        private static float sineEaseInOut(float progress) { return (Mathf.Sin(progress * Mathf.PI - halfPi) + 1) / 2; }

        private static float EaseInPower(float progress, int power)
        {
            return Mathf.Pow(progress, power);
        }

        private static float EaseOutPower(float progress, int power)
        {
            int sign = power % 2 == 0 ? -1 : 1;
            return (sign * (Mathf.Pow(progress - 1, power) + sign));
        }

        private static float EaseInOutPower(float progress, int power)
        {
            progress *= 2.0f;
            if (progress < 1)
            {
                return Mathf.Pow(progress, power) / 2.0f;
            }
            else
            {
                int sign = power % 2 == 0 ? -1 : 1;
                return (sign / 2.0f * (Mathf.Pow(progress - 2, power) + sign * 2));
            }
        }
    }

}