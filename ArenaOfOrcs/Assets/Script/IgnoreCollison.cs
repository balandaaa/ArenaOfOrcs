using UnityEngine;
using System.Collections;

public class IgnoreCollison : MonoBehaviour {

    private Collider2D other;
    // Use this for initialization
	private void Awake () {
        other = GameObject.Find("Enemy").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
	}
	
}
