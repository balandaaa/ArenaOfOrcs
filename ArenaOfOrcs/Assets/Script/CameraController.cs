using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    void Start()
    {
    }


    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = target.transform.position.x ;
        //position.y = target.transform.position.y;
        transform.position = position;
    }
}
