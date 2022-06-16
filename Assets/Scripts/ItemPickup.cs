using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]private GameObject scoreline;
    private Text scoredisplay;
    private int collected;

    private void Awake()
    {
        scoredisplay = scoreline.GetComponent<Text>();
    }

    
    void Start()
    {        
        collected = 0;
        scoredisplay.text = $"Collected: {collected} ";
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Collectable")
        {
            //add to pickup counter etc
            collected += 1;
            Destroy(coll.gameObject);
            Debug.Log($"You got a {coll.gameObject.name} nice one!");
            scoredisplay.text = $"Collected: {collected} ";
        }
    }
}
