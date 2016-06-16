using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestScripts {
    public class InnerCache : MonoBehaviour {
        Dictionary<Type, Component> cache = new Dictionary<Type, Component>();

        public T Get<T>() where T : Component {
            var type = typeof(T);

            if (!cache.ContainsKey(type)) {
                cache.Add(type, GetComponent<T>());
            }

            return cache[type] as T;
        }
    }
}
