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

	void Start() {
		// Now we can use cached items
		var pos = MyTransform.position;
	}

	void FixedUpdate() {
		// Even hidden one
		MyRigidbody.AddForce(Vector3.forward * 10 * Time.fixedDeltaTime);
	}
}
