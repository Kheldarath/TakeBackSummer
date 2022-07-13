using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //This serves as a skeleton for the player class.
    
    [SerializeField]int maxLives = 1; //how many times player can die.
       
    [SerializeField]int maxHearts = 2; //how many times player can be hit

    public int score = 0; //will be linked to enemy kills
    public int livesLeft;

    public int heartsLeft = 0;
    //TODO implement "weapons" or rather the spray gun thingy.

    public bool isAlive = true;
    public bool hasSword = false;


    void Start()
    {
        isAlive = true;
        hasSword = false;
        heartsLeft = maxHearts;
        livesLeft = maxLives;
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
        heartsLeft = 0;
        livesLeft--;
        isAlive = false;
        Destroy(gameObject, 1f);
    }

   
    
}
