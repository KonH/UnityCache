using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace UnityCache {
	public static class PreCacheEditor {
		public static bool PreCacheOnBuild = true;
		public static bool WriteToLog      = false;

		[MenuItem("UnityCache/PreCache")]
		public static bool PreCacheFromMenu() {
			return PreCache(true);
		}

		static bool PreCache(bool force) {
			bool result = false;
			var items = GameObject.FindObjectsOfType<MonoBehaviour>();
			foreach ( var item in items ) {
				if ( PreCacheAll(item, force) ) {
					EditorUtility.SetDirty(item);
					if ( WriteToLog ) {
						Debug.LogFormat("PreCached: {0} [{1}]", item.name, item.GetType());
					}
					result = true;
				}
			}
			return result;
		}

		static bool PreCacheAll(MonoBehaviour instance, bool force) {
			var type = instance.GetType();
			return CacheFields(instance, GetFieldsToCache(type), force);
		}

		static List<FieldInfo> GetFieldsToCache(Type type) {
			var fields = new List<FieldInfo>();
			foreach ( var field in type.GetFields() ) {
				foreach ( var a in field.GetCustomAttributes(false) ) {
					if ( a is PreCachedAttribute ) {
						fields.Add(field);
					}
				}
			}
			return fields;
		}

		static bool CacheFields(MonoBehaviour instance, List<FieldInfo> fields, bool force) {
			bool             cached = false;
			SerializedObject serObj = null;
			
			var iter = fields.GetEnumerator();
			while ( iter.MoveNext() ) {
				if ( serObj == null ) {
					serObj = new SerializedObject(instance);
				}
				var type = iter.Current.FieldType;
				var name = iter.Current.Name;

				var property = serObj.FindProperty(name);
				if ( !property.objectReferenceValue || force ) {
					cached = true;
					property.objectReferenceValue = instance.GetComponent(type);
				}
				if ( WriteToLog ) {
					Debug.Log("Cached value: " + property.objectReferenceValue);
				}
			}
			if ( cached ) {
				serObj.ApplyModifiedProperties();
			}
			return cached;
		}

		[PostProcessScene()]
		static void OnPostProcessScene() {
			if ( PreCacheOnBuild && PreCache(false) ) {
				Debug.LogWarning("You have non-cached objects on scenes. Now that objects cached before build, but changes could not be saved. Use UnityCache/PreCache command to cache it.");
			}
		}
	}
}
