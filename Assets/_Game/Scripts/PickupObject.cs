using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    AudioSource collectSound;
	    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collectSound.Play();
            print("Item picked up");
            //GameManager.instance.AddScore(1);
            //print("GameManager.instance.playerScore");
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
