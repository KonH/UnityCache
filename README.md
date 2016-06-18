# UnityCache
## Version: 0.31

Simple scripts to cache your components using attributes.
You can do it at runtime with some performance issues and in editor without it.

### Runtime Example:
Components loaded using [Cached] attribute and you do not want to call GetComponent() on each one.

    using UnityEngine;
    // Add cache dependency
    using UnityCache;

    public class CacheExample : MonoBehaviour {
        // This members would be initialized on load
        // Only public members
        [Cached]
        public Transform MyTransform = null;
        // But you can cache hide-in-inspector objects
        [HideInInspector]
        [Cached] 
        public Rigidbody MyRigidbody = null;

        void Awake() {
            // Cache all [Cached] members
            Cache.CacheAll(this);
        }

        void Start () {
            // Now we can use cached items
            var pos = MyTransform.position;
        }
	
        void FixedUpdate () {
            // Even hidden one
            MyRigidbody.AddForce(Vector3.forward * 10 * Time.fixedDeltaTime);
        }
    }
  
### Editor Example:
Components loaded in Editor (before application start) and saved to instance variables.

    using UnityEngine;
    // Add cache dependency
    using UnityCache;
    
    public class PreCacheExample : MonoBehaviour {
      // This members would be cached in scene objects after execute UnityCache/PreCache command in menu
      // Only public members
      [PreCached]
      public Transform MyTransform = null;
      // But you can cache hide-in-inspector objects
      [HideInInspector]
      [PreCached] 
      public Rigidbody MyRigidbody = null;
  
      void Awake() {
          // No field initialization on load!
      }

      void Start () {
          // Now we can use cached items
          var pos = MyTransform.position;
      }
	
      void FixedUpdate () {
          // Even hidden one
          MyRigidbody.AddForce(Vector3.forward * 10 * Time.fixedDeltaTime);
      }
  }

LICENSE: MIT (see LICENSE.txt beside)
