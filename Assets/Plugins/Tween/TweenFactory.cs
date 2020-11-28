using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.Tween
{

    /// <summary>
    /// Tween manager - do not add directly as a script, instead call the static methods in your other scripts.
    /// </summary>
    public class TweenFactory : MonoBehaviour
    {
        private static bool needsInitialize = true;
        private static GameObject root;
        private static readonly List<ITween> tweens = new List<ITween>();

        private static void EnsureCreated()
        {
            if (needsInitialize)
            {
                needsInitialize = false;
                root = new GameObject();
                root.name = "DigitalRubyTween";
                root.hideFlags = HideFlags.HideAndDontSave;
                root.AddComponent<TweenFactory>();
                GameObject.DontDestroyOnLoad(root);
            }
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerSceneLoaded;
        }

        private void SceneManagerSceneLoaded(UnityEngine.SceneManagement.Scene s, UnityEngine.SceneManagement.LoadSceneMode m)
        {
            tweens.Clear();
        }

        private void Update()
        {
            ITween t;

            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                t = tweens[i];

                if (t.Update(Time.deltaTime) && i < tweens.Count && tweens[i] == t)
                {
                    tweens.RemoveAt(i);
                }
                i = Math.Min(i, tweens.Count - 1);
            }
        }

        /// <summary>
        /// Start and add a float tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>FloatTween</returns>
        public static FloatTween Tween(object key, float start, float end, float duration, Func<float, float> scaleFunc, Action<ITween<float>> progress, Action<ITween<float>> completion)
        {
            FloatTween t = new FloatTween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector2 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector2Tween</returns>
        public static Vector2Tween Tween(object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector2>> progress, Action<ITween<Vector2>> completion)
        {
            Vector2Tween t = new Vector2Tween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector3 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector3Tween</returns>
        public static Vector3Tween Tween(object key, Vector3 start, Vector3 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector3>> progress, Action<ITween<Vector3>> completion)
        {
            Vector3Tween t = new Vector3Tween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector4 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector4Tween</returns>
        public static Vector4Tween Tween(object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, Action<ITween<Vector4>> progress, Action<ITween<Vector4>> completion)
        {
            Vector4Tween t = new Vector4Tween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Color tween
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>ColorTween</returns>
        public static ColorTween Tween(object key, Color start, Color end, float duration, Func<float, float> scaleFunc, Action<ITween<Color>> progress, Action<ITween<Color>> completion)
        {
            ColorTween t = new ColorTween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Quaternion tween
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>QuaternionTween</returns>
        public static QuaternionTween Tween(object key, Quaternion start, Quaternion end, float duration, Func<float, float> scaleFunc, Action<ITween<Quaternion>> progress, Action<ITween<Quaternion>> completion)
        {
            QuaternionTween t = new QuaternionTween();
            t.Key = key;
            t.Start(start, end, duration, scaleFunc, progress, completion);
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Add a tween
        /// </summary>
        /// <param name="tween">Tween to add</param>
        public static void AddTween(ITween tween)
        {
            EnsureCreated();
            if (tween.Key != null)
            {
                RemoveTweenKey(tween.Key, AddKeyStopBehavior);
            }
            tweens.Add(tween);
        }

        /// <summary>
        /// Remove a tween
        /// </summary>
        /// <param name="tween">Tween to remove</param>
        /// <param name="stopBehavior">Stop behavior</param>
        /// <returns>True if removed, false if not</returns>
        public static bool RemoveTween(ITween tween, TweenStopBehavior stopBehavior)
        {
            tween.Stop(stopBehavior);
            return tweens.Remove(tween);
        }

        /// <summary>
        /// Remove a tween by key
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <param name="stopBehavior">Stop behavior</param>
        /// <returns>True if removed, false if not</returns>
        public static bool RemoveTweenKey(object key, TweenStopBehavior stopBehavior)
        {
            if (key == null)
            {
                return false;
            }

            bool foundOne = false;
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                ITween t = tweens[i];
                if (key.Equals(t.Key))
                {
                    t.Stop(stopBehavior);
                    tweens.RemoveAt(i);
                    foundOne = true;
                }
            }
            return foundOne;
        }

        public static bool PauseTweenKey(object key)
        {
            if (key == null)
            {
                return false;
            }

            bool foundOne = false;
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                ITween t = tweens[i];
                if (key.Equals(t.Key))
                {
                    t.Pause();
                    foundOne = true;
                }
            }
            return foundOne;
        }

        public static bool UnpauseTweenKey(object key)
        {
            if (key == null)
            {
                return false;
            }

            bool foundOne = false;
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                ITween t = tweens[i];
                if (key.Equals(t.Key))
                {
                    t.Resume();
                    foundOne = true;
                }
            }
            return foundOne;
        }


        /// <summary>
        /// Stop behavior if you add a tween with a key and tweens already exist with the key
        /// </summary>
        public static TweenStopBehavior AddKeyStopBehavior = TweenStopBehavior.DoNotModify;
    }

}