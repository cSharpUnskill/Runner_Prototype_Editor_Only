using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Text scoreText;
    public GameObject Player;
    
    void Update()
    {
        scoreText.text = ((int)(player.position.z / 2)).ToString();
        if ((int.Parse(scoreText.text) >= 70)) 
        {
            Player.GetComponent<PlayerMovement>().SpeedUp();
        }
    }
}
