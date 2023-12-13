using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textoPuntaje;
    private TextMeshProUGUI textoVidas;

    public GameObject HUD;
    public GameObject Player;
    public GameObject Bestiario;
    public GameObject FinNivel;
    public GameObject Memory;
    public RectTransform RetryMenu;


    private GameObject Estadisticas;

    private RectTransform pantallaFinNivel;
    private RectTransform pantallaNextLevel;
    private RectTransform pantallaMuerte;
    private Memory _memory;
    


    public int vidasTotales;

    public int scoreTotal;

    public int[] ArrayContEMuertos;
    public int[] ArrayEMuertosXBala;

    private GameObject[] textos;
    private TextMeshProUGUI[] numeros;
    private PlayerController _controller;

    private int ePuntos;
    private int vidas;
    private int ID;
    private int dañoJugador;
    private int vidasPerdidas;
    private int enemigosTotalesMuertos;
    


    private bool MuerteXBala;

    private void Awake()
    {
        _controller = Player.GetComponent<PlayerController>();
        pantallaFinNivel = FinNivel.GetComponent<RectTransform>();
        _memory = Memory.GetComponent<Memory>();
        _memory.AsignarArray();
        LeerMemory();

    }

    private void Start()
    {

        
        for (int i=0; i<HUD.transform.childCount; i++)
        {
            textoPuntaje = HUD.transform.Find("Score").GetComponent<TextMeshProUGUI>();
            textoVidas = HUD.transform.Find("Vidas").GetComponent<TextMeshProUGUI>();
        }
        for (int i = 0; i < FinNivel.transform.childCount; i++)
        {
            Estadisticas = FinNivel.transform.Find("Estadisticas").gameObject;

            pantallaNextLevel = FinNivel.transform.Find("End").GetComponent<RectTransform>();
            pantallaMuerte = FinNivel.transform.Find("Game Over").GetComponent<RectTransform>();
        }



        textos = new GameObject[Estadisticas.transform.childCount];
        numeros = new TextMeshProUGUI[Estadisticas.transform.childCount];

        for (int i = 0; i < textos.Length; i++)
        {
            textos[i] = Estadisticas.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < textos.Length; i++)
        {
            numeros[i] = textos[i].transform.Find("Numero").GetComponent<TextMeshProUGUI>();
        }

        ArrayContEMuertos = new int[Bestiario.transform.childCount];
        ArrayEMuertosXBala = new int[Bestiario.transform.childCount];
        _memory.tamañoArray = ArrayContEMuertos.Length;

        scoreTotal = 0;
        actualizarScore();

        vidasTotales = 4;
        vidas = vidasTotales;
        textoVidas.text = vidas.ToString("0");


    }
    private void ControlPuntajeEnemigos(string nombre)
    {

        GameObject Enemigo = Bestiario.transform.Find(nombre).gameObject; // Obtener una referencia al objeto hijo
        EnemyHealth _enemyHealth = Enemigo.GetComponent<EnemyHealth>(); // Acceder a la variable pública del objeto hijo
        ID = _enemyHealth.ID;


        ePuntos = _enemyHealth.puntos;
        if (MuerteXBala == false)
        {
            ArrayContEMuertos[ID]++;
        }
        else
        {
            ArrayContEMuertos[ID]++;
            ArrayEMuertosXBala[ID]++;
            ePuntos = ePuntos * 2;
        }
        scoreTotal += ePuntos;

        Debug.Log("OBTIENE LOS SIGUIENTES PUNTOS: " + ePuntos);

        MuerteXBala = false;
        actualizarScore();
    }




    private void ValidarMuerteXBala(bool muerte)
    {
        MuerteXBala = muerte;

    }



    public void ControlPuntajePlayer(int pjPuntos)
    {
        scoreTotal += pjPuntos;
        dañoJugador = dañoJugador - (pjPuntos / 2);
        actualizarScore();
    }

    private void ControlPuntajeMuerte(int pjMuerte)
    {
        scoreTotal += pjMuerte;
        actualizarScore();
    }



    public void OnRetryScore()
    {
        vidas = vidasTotales;
        textoVidas.text = vidas.ToString("0");
        vidasPerdidas = 0;
        dañoJugador = 0;
        scoreTotal = 0;
        actualizarScore();
    }




    private void actualizarScore()
    {
        if (scoreTotal <= 0)
        {
            scoreTotal = 0;
        }

        textoPuntaje.text = scoreTotal.ToString("0");
    }



    public void ControlVidas(string pMuerte)
    {
        vidas--;
        vidasPerdidas++;
        
        switch (pMuerte)
        {
            case "vacio":
                ControlPuntajeMuerte(-10);

                break;
            case "Enemy":
                ControlPuntajeMuerte(-5);
                break;
        }

        if (vidas < 0)
        {
            vidas = 0;
            MostrarEstadisticas(0);
        }
        else
        {
            RetryMenu.gameObject.SetActive(true);
        }

        textoVidas.text = vidas.ToString("0");

    }


    private void MostrarEstadisticas(int pantalla)
    {
        pantallaFinNivel.gameObject.SetActive(true);

        if (pantalla == 1)
        {
            pantallaNextLevel.gameObject.SetActive(true);

        }
        if (pantalla == 0)
        {
            pantallaMuerte.gameObject.SetActive(true);
        }





        _controller.enabled = false;




        enemigosTotalesMuertos = ArrayContEMuertos[0] + ArrayContEMuertos[1];

        numeros[0].text = scoreTotal.ToString("0");
        numeros[1].text = vidas.ToString("0");
        numeros[2].text = vidasPerdidas.ToString("0");
        numeros[3].text = dañoJugador.ToString("0");
        numeros[4].text = enemigosTotalesMuertos.ToString("0");
        numeros[5].text = ArrayContEMuertos[0].ToString("0");
        numeros[6].text = ArrayContEMuertos[1].ToString("0");

        GuardarMemory();

    }

     private void GuardarMemory()
    {
        
        _memory.scoreTotal += scoreTotal;
        _memory.vidas += vidas;
        _memory.vidasPerdidas += vidasPerdidas;
        _memory.dañoJugador += dañoJugador;
        _memory.enemigosTotalesMuertos += enemigosTotalesMuertos;
        _memory.ArrayContEMuertos[0] += ArrayContEMuertos[0];
        _memory.ArrayContEMuertos[1] += ArrayContEMuertos[1];
        
    }

    private void LeerMemory()
    {
        scoreTotal += _memory.scoreTotal;
        vidas += _memory.vidas;
        vidasPerdidas += _memory.vidasPerdidas;
        dañoJugador += _memory.dañoJugador;
        enemigosTotalesMuertos += _memory.enemigosTotalesMuertos;
        ArrayContEMuertos[0] = _memory.ArrayContEMuertos[0];
        ArrayContEMuertos[1] = _memory.ArrayContEMuertos[1];


    }

}















