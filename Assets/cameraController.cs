using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPoint(Transform point)
    {
        transform.position = point.position;
        transform.rotation = Quaternion.LookRotation(point.forward);
    }
}
