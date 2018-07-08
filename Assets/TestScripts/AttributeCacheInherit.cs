using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace TestScripts {
	public class AttributeCacheInherit : MonoBehaviour {
		public int Tries = 1000;

		protected virtual void Awake() {
			CacheAll();
		}

		void CacheAll() {
			var type = GetType();
			CacheFields(GetFieldsToCache(type));
		}

		List<FieldInfo> GetFieldsToCache(Type type) {
			var fields = new List<FieldInfo>();
			foreach (var field in type.GetFields()) {
				foreach (var a in field.GetCustomAttributes(false)) {
					if (a is CachedAttribute) {
						fields.Add(field);
					}
				}
			}
			return fields;
		}

		void CacheFields(List<FieldInfo> fields) {
			var iter = fields.GetEnumerator();
			while (iter.MoveNext()) {
				var type = iter.Current.FieldType;
				iter.Current.SetValue(this, GetComponent(type));
			}
		}

		void Update() {
			MemberLoop();
		}

		void MemberLoop() {
			GC.Collect();
			UnityEngine.Profiling.Profiler.BeginSample("Init Member Attribute Cache by Reflection");
			for (int i = 0; i < Tries; i++) {
				Member();
			}
			UnityEngine.Profiling.Profiler.EndSample();
		}

		void Member() {
			CacheAll();
		}
	}
}
