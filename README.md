# UnityCache
## Version: 0.2

Simple scripts to cache your components using attributes.
Example:

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
	
	      void Update () {
            // Even hidden one
            MyRigidbody.AddForce(Vector3.forward * 10 * Time.deltaTime);
	      }
  }
