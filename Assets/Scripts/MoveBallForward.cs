using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>().AddForce(Vector3.forward * 50,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
