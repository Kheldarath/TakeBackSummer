using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUp : MonoBehaviour
{    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PowerUp")
        {
            
            Destroy(coll.gameObject);
            Debug.Log($"You got a {coll.gameObject.name} nice one!");            
            

        }
    }
}
