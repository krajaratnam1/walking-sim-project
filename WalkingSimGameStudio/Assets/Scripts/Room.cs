using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Master master;
    public GameObject playerA, playerB;
    public int roomNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.name == "Player A")
        {
            master.playerARoom = roomNumber;
        }

        if(other.name == "Player B")
        {
            master.playerBRoom = roomNumber;
        }
    }
}
