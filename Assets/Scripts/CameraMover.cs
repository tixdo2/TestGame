using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 20f;
    void Update()
    {
        Vector3 newPosition = transform.position;
        
        if (Input.GetKey("w"))
        {
            newPosition.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            newPosition.z -= speed * Time.deltaTime;
        }
        
        if (Input.GetKey("a"))
        {
            newPosition.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        { 
            newPosition.x += speed * Time.deltaTime;
        }
        transform.position = newPosition;
    }

    public void InitPosition(Vector3 position)
    {
        position.y += 50f;
        position.z -= 50f;
        transform.position = position;
    }
}
