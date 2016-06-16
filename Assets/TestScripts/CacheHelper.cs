using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace TestScripts {
    public static class CacheHelper {
        static Dictionary<Type, List<FieldInfo>> cachedTypes = new Dictionary<Type, List<FieldInfo>>();

        public static void CacheAll(MonoBehaviour instance, bool internalCache = true) {
            var type = instance.GetType();
            if (internalCache) {
                List<FieldInfo> fields = null;
                if (!cachedTypes.TryGetValue(type, out fields)) {
                    fields = GetFieldsToCache(type);
                    cachedTypes[type] = fields;
                }
                CacheFields(instance, fields);
            } else {
                CacheFields(instance, GetFieldsToCache(type));
            }
        }

        static List<FieldInfo> GetFieldsToCache(Type type) {
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

        static void CacheFields(MonoBehaviour instance, List<FieldInfo> fields) {
            var iter = fields.GetEnumerator();
            while (iter.MoveNext()) {
                var type = iter.Current.FieldType;
                iter.Current.SetValue(instance, instance.GetComponent(type));
            }
        }
    }
}