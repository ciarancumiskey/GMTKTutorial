using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 6;
    public float heightOffset = -10;
    public float deadZone = -45; //beyond the left edge of the screen
    private float timer = 0;
    // Start is called before the first frame update
    void Start(){
        SpawnPipe();
    }

    // Update is called once per frame
    void Update(){
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }
    }

    private void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        //Spawns the pipes with a random vertical offset
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        
    }
}
