using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{	
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;
	private Transform target;
	void Start(){
		target = GameObject.Find ("Player").transform;
	}
	void LateUpdate(){
		transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax),transform.position.z);
	}
   /* public Transform target;
    void Start()
    {
    }


    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = target.transform.position.x ;
        //position.y = target.transform.position.y;
        transform.position = position;
    }*/
}
