using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdBody;
    public float flapStrength;
    public LogicScript logicScript;
    public AudioSource chirpSound;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start(){
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update(){
        if (isAlive && Input.GetKeyDown(KeyCode.Space)){
            birdBody.velocity = Vector2.up * flapStrength;
            chirpSound.PlayOneShot(chirpSound.clip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false; // They are now pining for the fjords
        logicScript.GameOver();
    }
}
