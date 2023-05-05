using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float deadZone = -45.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < deadZone){
            Destroy(gameObject); //free up memory after it leaves the screen
        }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime; //speed is dependent on framerate
        
    }
}
