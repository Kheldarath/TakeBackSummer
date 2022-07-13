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
        Animator CollectAnim;

        if (coll.gameObject.tag == "Collectable")
        {
            CollectAnim = coll.gameObject.GetComponent<Animator>();
            //add to pickup counter etc
            collected += 1;
            CollectAnim.SetTrigger("OnPickup");
            Destroy(coll.gameObject, 0.6f);
            Debug.Log($"You got a {coll.gameObject.name} nice one!");
            scoredisplay.text = $"Collected: {collected} ";
        }
    }
}
