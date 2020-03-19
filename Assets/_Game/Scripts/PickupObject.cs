using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
	    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            print("Item picked up");
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
