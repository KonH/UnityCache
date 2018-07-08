using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestScripts {
	public class InnerCache : MonoBehaviour {
		Dictionary<Type, Component> _cache = new Dictionary<Type, Component>();

		public T Get<T>() where T : Component {
			var type = typeof(T);
			Component item = null;
			if ( !_cache.TryGetValue(type, out item) ) {
				item = GetComponent<T>();
				_cache.Add(type, item);
			}

			return item as T;
		}
	}
}
