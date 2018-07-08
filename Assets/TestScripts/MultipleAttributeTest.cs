using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace TestScripts {
	public class MultipleAttributeTest : MonoBehaviour {
		public int Tries = 1000;

		[Cached]
		public Test1 t1;
		[Cached]
		public Test2 t2;
		[Cached]
		public Test3 t3;
		[Cached]
		public Test4 t4;
		[Cached]
		public Test5 t5;

		void Update() {
			DirectLoop();
			ReflectedLoop();
			CachedLoop();
		}

		void DirectLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Cache Directly - 5 components");
			for ( int i = 0; i < Tries; i++ ) {
				Direct();
			}
			Profiler.EndSample();
		}

		void ReflectedLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Static Attribute Cache by Reflection (first time) - 5 components");
			for ( int i = 0; i < Tries; i++ ) {
				Reflected();
			}
			Profiler.EndSample();
		}

		void CachedLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Static Attribute Cache with known fields (next times) - 5 components");
			for ( int i = 0; i < Tries; i++ ) {
				Cached();
			}
			Profiler.EndSample();
		}

		void Direct() {
			t1 = GetComponent<Test1>();
			t2 = GetComponent<Test2>();
			t3 = GetComponent<Test3>();
			t4 = GetComponent<Test4>();
			t5 = GetComponent<Test5>();
		}

		void Reflected() {
			CacheHelper.CacheAll(this, false);
		}

		void Cached() {
			CacheHelper.CacheAll(this, true);
		}
	}
}