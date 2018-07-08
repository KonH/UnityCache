using UnityEngine;

namespace UnityCache {
	/// <summary>
	/// Experimental feature: 
	/// You can make instance of this component in our script and use it for lazy initialization
	/// Pro:
	/// - You can cache external dependencies by passing its GameObject to constructor
	/// Contra:
	/// - You want to explicit create all instances (definition and initialization)
	/// </summary>
	/// <typeparam name="T">Any component</typeparam>
	public class Cached<T> where T : Component {
		GameObject _target    = null;
		bool       _isCached  = false;
		T          _component = null;

		public Cached(GameObject target) {
			_target = target;
		}

		public T Value {
			get {
				if ( !_isCached ) {
					_component = _target.GetComponent<T>();
					_isCached  = false;
				}
				return _component;
			}
		}

		public T SafeValue {
			get {
				if ( _target ) {
					if ( !_component ) {
						_component = _target.GetComponent<T>();
					}
					return _component;
				}
				return null;
			}
		}
	}
}
