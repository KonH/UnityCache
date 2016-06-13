using UnityEngine;

public class Example : MonoBehaviour {
    Rigidbody _rigidbody;

	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        _rigidbody.AddForce(Vector3.up * Time.deltaTime);
	}
}
