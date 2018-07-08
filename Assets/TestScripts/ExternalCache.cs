using UnityEngine;
using System;
using System.Collections.Generic;

namespace TestScripts {
	public static class ExternalCache {
		static Dictionary<GameObject, Transform> _trans = new Dictionary<GameObject, Transform>();

		public static Transform GetCachedTransfom(this GameObject owner) {
			Transform item = null;
			if (!_trans.TryGetValue(owner, out item)) {
				item = owner.GetComponent<Transform>();
				_trans.Add(owner, item);
			}
			return item;
		}

		static Dictionary<GameObject, TestComponent> _test = new Dictionary<GameObject, TestComponent>();

		public static TestComponent GetCachedTestComponent(this GameObject owner) {
			TestComponent item = null;
			if (!_test.TryGetValue(owner, out item)) {
				item = owner.GetComponent<TestComponent>();
				_test.Add(owner, item);
			}
			return _test[owner];
		}

		static Dictionary<GameObject, Dictionary<Type, Component>> _cache = new Dictionary<GameObject, Dictionary<Type, Component>>();

		public static T GetCachedComponent<T>(this GameObject owner) where T : Component {
			var type = typeof(T);

			Dictionary<Type, Component> container = null;
			if (!_cache.TryGetValue(owner, out container)) {
				container = new Dictionary<Type, Component>();
				_cache.Add(owner, container);
			}
			Component item = null;
			if (!container.TryGetValue(type, out item)) {
				item = owner.GetComponent<T>();
				container.Add(type, item);
			}

			return item as T;
		}
	}
}