using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    //References to required canvas objects
    [SerializeField] GameObject NoSwordBox;
    [SerializeField] GameObject SwordBox;
    [SerializeField] GameObject LifeBox;
    [SerializeField] GameObject HeartBoxes;

    [SerializeField] Player player;

    //need to set up ability to change values on the canvas to reflect life and heart changes.
    private Image Hearts, Sword;
    private Text Lives;
    
    void Start()
    {
        player = FindObjectOfType<Player>(); //looks for player in a scene (should only ever be one)
        SwordBox.SetActive(false);
        NoSwordBox.SetActive(true);
        Lives = LifeBox.GetComponent<Text>();
    }
    
    void Update()
    {
        Lives.text = $" X {player.livesLeft}";
        if (player.hasSword)
        {
            NoSwordBox.SetActive(false);
            SwordBox.SetActive(true);
        }
    }
}
