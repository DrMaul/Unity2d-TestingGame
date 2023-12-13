using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiario : MonoBehaviour
{
    public GameObject[] cantEnemigos;



    void Start()
    {
        cantEnemigos = new GameObject[this.transform.childCount];

        for (int i = 0; i < 2; i++)
        {
            cantEnemigos[i] = this.transform.GetChild(i).gameObject;
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
