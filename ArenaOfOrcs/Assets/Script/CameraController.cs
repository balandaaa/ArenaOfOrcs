using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{	
	private Transform target;
	[SerializeField]
	private float y;
	void Start(){
		target = GameObject.Find ("Player").transform;
	}
	void LateUpdate(){
		Vector3 position = transform.position;
		position.x = target.transform.position.x ;
		position.y = target.transform.position.y + y;
		transform.position = position;
	}
}
