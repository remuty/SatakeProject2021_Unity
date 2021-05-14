using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject target;
    Rigidbody rb;
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slash()
    {
        transform.LookAt(target.transform);
        rb.AddForce(transform.forward * 3000);
        Invoke("Return", 1f);
    }

    void Return()
    {
        transform.position = initialPos;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
