using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    
    public int Monedas = 100;
    public GameObject PocionInv;
    private Pocion _pocion;
    private int CantPocion;
    
    
   
    public void UsarPocion()
    {
        if (PocionInv.transform.childCount > 0)
        {
            CantPocion = PocionInv.transform.childCount;
            //llamar funcion cura y destroy
            if (CantPocion > 0)
            {
                _pocion = PocionInv.transform.GetChild(0).GetComponent<Pocion>();
                _pocion.Cura();

            }
            else
            {
                Debug.Log("No quedan más pociones");
            }

        }


    }
}
