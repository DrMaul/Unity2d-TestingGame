using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpTienda : MonoBehaviour
{
    

    public void CambioEscena()
    {
        SceneManager.LoadScene("Tienda");
    }
}