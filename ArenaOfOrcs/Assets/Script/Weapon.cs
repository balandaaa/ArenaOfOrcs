using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Weapon : MonoBehaviour {
	[SerializeField]
	private float speed;
	private Rigidbody2D rb;

	private Vector2 direction;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		direction = Vector2.right;
	}
	void FixedUpdate(){
		rb.velocity = direction * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
