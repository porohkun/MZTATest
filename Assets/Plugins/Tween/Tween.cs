/*
The MIT License (MIT)
Copyright (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com
Created by Jeff Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

using UnityEngine;

namespace DigitalRuby.Tween
{
    /// <summary>
    /// An implementation of a tween object.
    /// </summary>
    /// <typeparam name="T">The type to tween.</typeparam>
    public class Tween<T> : ITween<T> where T : struct
    {
        private readonly Func<ITween<T>, T, T, float, T> lerpFunc;

        private float currentTime;
        private float duration;
        private Func<float, float> scaleFunc;
        private Action<ITween<T>> progressCallback;
        private Action<ITween<T>> completionCallback;
        private TweenState state;

        private T start;
        private T end;
        private T previewValue;
        private T value;

        /// <summary>
        /// The key that identifies this tween - can be null
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// Gets the current time of the tween.
        /// </summary>
        public float CurrentTime { get { return currentTime; } }

        /// <summary>
        /// Gets the duration of the tween.
        /// </summary>
        public float Duration { get { return duration; } }

        /// <summary>
        /// Gets the current state of the tween.
        /// </summary>
        public TweenState State { get { return state; } }

        /// <summary>
        /// Gets the starting value of the tween.
        /// </summary>
        public T StartValue { get { return start; } }

        /// <summary>
        /// Gets the ending value of the tween.
        /// </summary>
        public T EndValue { get { return end; } }

        /// <summary>
        /// Gets the current value of the tween.
        /// </summary>
        public T CurrentValue { get { return value; } }
        public T PreviewValue { get { return previewValue; } }

        /// <summary>
        /// The game object - null if none
        /// </summary>
        public GameObject GameObject;

        /// <summary>
        /// The renderer - null if none
        /// </summary>
        public Renderer Renderer;

        /// <summary>
        /// Gets the current progress of the tween (0 - 1).
        /// </summary>
        public float CurrentProgress { get; private set; }
        public float LinearProgress { get; private set; }

        /// <summary>
        /// Initializes a new Tween with a given lerp function.
        /// </summary>
        /// <remarks>
        /// C# generics are good but not good enough. We need a delegate to know how to
        /// interpolate between the start and end values for the given type.
        /// </remarks>
        /// <param name="lerpFunc">The interpolation function for the tween type.</param>
        public Tween(Func<ITween<T>, T, T, float, T> lerpFunc)
        {
            this.lerpFunc = lerpFunc;
            state = TweenState.Stopped;
        }

        /// <summary>
        /// Starts a tween.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="end">The end value.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="scaleFunc">A function used to scale progress over time.</param>
        /// <param name="progress">Progress callback</param>
        /// <param name="completion">Called when the tween completes</param>
        public void Start(T start, T end, float duration, Func<float, float> scaleFunc, Action<ITween<T>> progress, Action<ITween<T>> completion)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("duration must be greater than 0");
            }
            if (scaleFunc == null)
            {
                throw new ArgumentNullException("scaleFunc");
            }

            currentTime = 0;
            this.duration = duration;
            this.scaleFunc = scaleFunc;
            this.progressCallback = progress;
            this.completionCallback = completion;
            state = TweenState.Running;

            this.start = start;
            this.previewValue = start;
            this.value = start;
            this.end = end;

            UpdateValue();
        }

        /// <summary>
        /// Pauses the tween.
        /// </summary>
        public void Pause()
        {
            if (state == TweenState.Running)
            {
                state = TweenState.Paused;
            }
        }

        /// <summary>
        /// Resumes the paused tween.
        /// </summary>
        public void Resume()
        {
            if (state == TweenState.Paused)
            {
                state = TweenState.Running;
            }
        }

        /// <summary>
        /// Stops the tween.
        /// </summary>
        /// <param name="stopBehavior">The behavior to use to handle the stop.</param>
        public void Stop(TweenStopBehavior stopBehavior)
        {
            if (state != TweenState.Stopped)
            {
                state = TweenState.Stopped;
                if (stopBehavior == TweenStopBehavior.Complete)
                {
                    currentTime = duration;
                    UpdateValue();
                    if (completionCallback != null)
                    {
                        completionCallback.Invoke(this);
                        completionCallback = null;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the tween.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time to add to the tween.</param>
        /// <returns>True if done, false if not</returns>
        public bool Update(float elapsedTime)
        {
            if (state == TweenState.Running)
            {
                currentTime += elapsedTime;
                if (currentTime >= duration)
                {
                    Stop(TweenStopBehavior.Complete);
                    return true;
                }
                else
                {
                    UpdateValue();
                    return false;
                }
            }
            return (state == TweenState.Stopped);
        }

        /// <summary>
        /// Helper that uses the current time, duration, and delegates to update the current value.
        /// </summary>
        private void UpdateValue()
        {

#if UNITY || UNITY_5_3_OR_NEWER

            if (Renderer == null || Renderer.isVisible)
            {

#endif
                LinearProgress = currentTime / duration;
                CurrentProgress = scaleFunc(LinearProgress);
                previewValue = value;
                value = lerpFunc(this, start, end, CurrentProgress);
                if (progressCallback != null)
                {
                    progressCallback.Invoke(this);
                }

#if UNITY || UNITY_5_3_OR_NEWER

            }

#endif

        }
    }

}