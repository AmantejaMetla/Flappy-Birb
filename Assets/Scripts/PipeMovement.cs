using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;  // Speed at which pipes move to the left

    void Update()
    {
        // Move the pipe to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        
    }
}
    