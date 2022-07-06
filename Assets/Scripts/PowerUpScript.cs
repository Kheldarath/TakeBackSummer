using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PICKUPTYPE { Weapon, Heart, Power }
public class PowerUpScript : MonoBehaviour
{
    [SerializeField] PICKUPTYPE pickup;
    PICKUPTYPE myType;
    Collider2D myObject;

    private void Start()
    {
        myObject = GetComponent<Collider2D>();
        PICKUPTYPE myType = pickup;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(myType == PICKUPTYPE.Weapon)
            {
                Player player = coll.gameObject.GetComponent<Player>();
                player.hasSword = true;
            }

            Destroy(gameObject);
        }
    }
}
