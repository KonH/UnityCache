using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

namespace TestScripts {
    public class AttributeCache : MonoBehaviour {
        public int Tries = 1000;

        //[Cached]
        //public Transform Trans = null;
        [Cached]
        public TestComponent Test = null;

        void Awake() {
        }

        void Update() {
            DirectLoop();
            ReflectedLoop();
            CachedLoop();
        }

        void DirectLoop() {
            GC.Collect();
            UnityEngine.Profiling.Profiler.BeginSample("Init Cache Directly");
            for (int i = 0; i < Tries; i++) {
                Direct();
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        void ReflectedLoop() {
            GC.Collect();
            UnityEngine.Profiling.Profiler.BeginSample("Init Static Attribute Cache by Reflection (first time)");
            for (int i = 0; i < Tries; i++) {
                Reflected();
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        void CachedLoop() {
            GC.Collect();
            UnityEngine.Profiling.Profiler.BeginSample("Init Static Attribute Cache with known fields (next times)");
            for (int i = 0; i < Tries; i++) {
                Cached();
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        void Direct() {
            Test = GetComponent<TestComponent>();
        }

        void Reflected() {
            CacheHelper.CacheAll(this, false);
        }

        void Cached() {
            CacheHelper.CacheAll(this, true);
        }
    }
}