using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armeria : MonoBehaviour
{
    public GameObject[] Arrayarmeria;


    void Start()
    {
        Arrayarmeria = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Arrayarmeria[i] = this.transform.GetChild(i).gameObject;
        }
    }

    
}
