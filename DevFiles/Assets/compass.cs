using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compass : MonoBehaviour
{
    public GameObject playerCompass;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCompass.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.y);
    }
}
