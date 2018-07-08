using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCache {
	public static class UCache {
		static Dictionary<Type, List<FieldInfo>> _cachedTypes = new Dictionary<Type, List<FieldInfo>>();

		/// <summary>Init all 'instance' members marked with [Cached] attribute using GetComponent</summary>
		/// <param name="instance">MonoBehaviour instance</param>
		public static void CacheAll(MonoBehaviour instance) {
			var type = instance.GetType();
			List<FieldInfo> fields = null;
			if ( !_cachedTypes.TryGetValue(type, out fields) ) {
				fields = GetFieldsToCache(type);
				_cachedTypes[type] = fields;
			}
			CacheFields(instance, fields);
		}

		static List<FieldInfo> GetFieldsToCache(Type type) {
			var fields = new List<FieldInfo>();
			foreach ( var field in type.GetFields() ) {
				foreach ( var a in field.GetCustomAttributes(false) ) {
					if ( a is CachedAttribute ) {
						fields.Add(field);
					}
				}
			}
			return fields;
		}

		static void CacheFields(MonoBehaviour instance, List<FieldInfo> fields) {
			var iter = fields.GetEnumerator();
			while ( iter.MoveNext() ) {
				var type = iter.Current.FieldType;
				iter.Current.SetValue(instance, instance.GetComponent(type));
			}
		}
	}
}
