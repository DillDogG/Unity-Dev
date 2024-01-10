using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform startPoint;
    public Transform endPoint;
    private Rigidbody rb;
    private bool toEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toEnd) {
            transform.LookAt(endPoint);
            rb.MovePosition(rb.position + endPoint.position * Time.fixedDeltaTime);
            if (transform.position.x <= endPoint.position.x + 1 && transform.position.z <= endPoint.position.z + 1) {
                if (transform.position.x >= endPoint.position.x - 1 && transform.position.z >= endPoint.position.z - 1) toEnd = false;
            }
        }
        else {
            transform.LookAt(startPoint);
            rb.MovePosition(rb.position + startPoint.position * Time.fixedDeltaTime);
            if (transform.position.x <= startPoint.position.x + 1 && transform.position.z <= startPoint.position.z + 1) {
                if (transform.position.x >= startPoint.position.x - 1 && transform.position.z >= startPoint.position.z - 1) toEnd = true;
            }
        }
    }
}
