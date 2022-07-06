using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //This serves as a skeleton for the player class.
    
    [SerializeField]int lives = 0; //how many times player can die.
       
    [SerializeField]int maxHearts = 2; //how many times player can be hit

    int score = 0; //will be linked to enemy kills

    public int heartsLeft = 0;
    //TODO implement "weapons" or rather the spray gun thingy.

    public bool isAlive = true;   


    void Start()
    {
        isAlive = true;
        heartsLeft = maxHearts;
    }

    
    void Update()
    {
        if (!isAlive)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        isAlive = false;
        Destroy(gameObject, 1f);
    }

   
    
}
