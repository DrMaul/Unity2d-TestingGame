using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
   /*
    *  Datos a enviar:
    * numeros[0].text = scoreTotal.ToString("0");
        numeros[1].text = vidas.ToString("0");
        numeros[2].text = vidasPerdidas.ToString("0");
        numeros[3].text = da�oJugador.ToString("0");
        numeros[4].text = enemigosTotalesMuertos.ToString("0");
        numeros[5].text = ArrayContEMuertos[0].ToString("0");
    numeros[6].text = ArrayContEMuertos[1].ToString("0");*/



    public int scoreTotal;
    public int vidas;
    public int vidasPerdidas;
    public int da�oJugador;
    public int enemigosTotalesMuertos;
    public int tama�oArray;
    public int[] ArrayContEMuertos;
   
   // public int oleadaPublica;

   


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        RetryDatos();
    }

    public void AsignarArray()
    {
        if (ArrayContEMuertos.Length == 0) {
            ArrayContEMuertos = new int[tama�oArray];
        }
    }



    public void RetryDatos()
    {
        

        scoreTotal = 0;
        vidas = 0;
        vidasPerdidas = 0;
        da�oJugador = 0;
        enemigosTotalesMuertos = 0;
        ArrayContEMuertos[0] = 0;
        ArrayContEMuertos[1] = 0;
       

    }



        
}
