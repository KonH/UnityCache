using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace UnityCache {
	public static class PreCacheEditor {
		public static bool WriteToLog = true;

		[MenuItem("UnityCache/PreCache")]
		public static void PreCache() {
			var items = GameObject.FindObjectsOfType<MonoBehaviour>();
			foreach(var item in items) {
				if(PreCacheAll(item)) {
					EditorUtility.SetDirty(item);
					if(WriteToLog) {
						Debug.LogFormat("PreCached: {0} [{1}]", item.name, item.GetType());
					}
				}
			}
		}
			
		static bool PreCacheAll(MonoBehaviour instance) {
			var type = instance.GetType();
			return CacheFields(instance, GetFieldsToCache(type)); 
		}

		static List<FieldInfo> GetFieldsToCache(Type type) {
			var fields = new List<FieldInfo>();
			foreach (var field in type.GetFields()) {
				foreach (var a in field.GetCustomAttributes(false)) {
					if (a is PreCachedAttribute) {
						fields.Add(field);
					}
				}
			}
			return fields;
		}

		static bool CacheFields(MonoBehaviour instance, List<FieldInfo> fields) {
			bool cached = false;
			UnityEditor.SerializedObject serObj = null;
			var iter = fields.GetEnumerator();
			while (iter.MoveNext()) {
				if(serObj == null) {
					serObj = new UnityEditor.SerializedObject(instance);
					cached = true;
				}
				var type = iter.Current.FieldType;
				var name = iter.Current.Name;

				var property = serObj.FindProperty(name);
				property.objectReferenceValue = instance.GetComponent(type);
				Debug.Log(property.objectReferenceValue);
			}
			if(cached) {
				serObj.ApplyModifiedProperties();
			}
			return cached;
		}
	}
}
