using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //This serves as a skeleton for the player class.
    
    int lives = 0; //how many times player can die.
       
    int hearts = 0; //how many times player can be hit

    int score = 0; //will be linked to enemy kills

    //TODO implement "weapons" or rather the spray gun thingy.

    public bool isAlive = true;



    void Start()
    {
        isAlive = true;
    }

    
    void Update()
    {
        if (!isAlive)
        {
            killPlayer();
        }
    }

    public void killPlayer()
    {
        isAlive = false;
    }
}
