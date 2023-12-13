using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    
    public static Instanciador Instance { get; private set; }

    public GameObject Puntos;
    public GameObject Oleadas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

    }
}
