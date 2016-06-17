using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestScripts {
    public class InnerCache : MonoBehaviour {
        Dictionary<Type, Component> cache = new Dictionary<Type, Component>();

        public T Get<T>() where T : Component {
            var type = typeof(T);
			Component item = null;
			if (!cache.TryGetValue(type, out item)) {
				item = GetComponent<T>();
				cache.Add(type, item);
            }

            return item as T;
        }
    }
}
