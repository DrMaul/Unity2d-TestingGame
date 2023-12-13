using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class controlOleadas : MonoBehaviour
{
    public int Oleada;
    public int enemXOleada;

    public int enemMuertos;

    public GameObject Bestiario;

    public int[] arrayContadorEnemigos;

    public RectTransform menuOleadas;

    private int porcentaje;
    private int OleadaPublica;
    private GameObject _enemyPool;
    

    private EnemySpawn _enemySpawn;

    //private Bestiario _bestiario;

    private void Awake()
    {
        _enemyPool = this.transform.Find("EnemyPool").gameObject;


        _enemySpawn = _enemyPool.GetComponent<EnemySpawn>();    

    }

    private void Start()
    {

        arrayContadorEnemigos = new int[Bestiario.transform.childCount];

        //Debug.Log("arrayContadorENEMIGOS tiene: " + arrayContadorEnemigos.Length + " posiciones");

    
    }


    public void SetOleada()
    {
        switch  (Oleada)
        {
            case 1:
                enemXOleada = 10;
                //Debug.Log("Se llama a Activar Oleada");
                ActivarOleada(enemXOleada);
           
                break;

            case 2:
                enemXOleada = 20;
                //20 enemigos
                ActivarOleada(enemXOleada);
                break;

            case 3:
                enemXOleada = 10;
                //30 enemigos
                ActivarOleada(enemXOleada);
                break;


            case 4:
                enemXOleada = 10;
                ActivarOleada(enemXOleada);
                //Boss
                break;

            default:
                break;


        }


    }

    public void ActivarOleada (int numEnemigos)
    {
        //Debug.Log("Entró en Activar Oleada");

        for (int i = 0; i < 2; i++)
        {
            //Debug.Log("Entró al FOR con el índice: " + i);
            switch (i)
                {
                    case 0:
                        porcentaje = 80;
                        break;
                    case 1:
                        porcentaje = 20;
                        break;
                
                }

            //Debug.Log("Voy a guardar en la posicion" + i + "del Array, el valor: " + (numEnemigos*porcentaje/100));
            arrayContadorEnemigos[i] = numEnemigos * porcentaje/100 ;

        }

        //Debug.Log("El contador para Enemy es: " + arrayContadorEnemigos[0]);
        //Debug.Log("El contador para Strong es: " + arrayContadorEnemigos[1]);

        //Debug.Log("El total de enemigos muertos es: " + enemMuertos);
        //Debug.Log("El total de enemigos por oleada es: " + enemXOleada);



        _enemySpawn.StartSpawn();

        


    }
    public void ActivarSpawn()
    {
        _enemyPool.SetActive(true);
        _enemySpawn.StartSpawn();
        
    }

    public void PararSpawn()
    {
        _enemySpawn.StopSpawn();
        _enemyPool.SetActive(false);
    }

    public void SumarContadorEnemigosMuertos ()
    {
        enemMuertos++;
        
        if (enemMuertos >= enemXOleada)
        {
            enemMuertos = enemXOleada;
            FinalizarOleada();
        }

    }
    public void SkipOleada()
    {
        enemMuertos = enemXOleada;
        arrayContadorEnemigos[0] = 0;
        arrayContadorEnemigos[1] = 0;
        SumarContadorEnemigosMuertos();
    }
    private void FinalizarOleada()
    {

         _enemySpawn.StopSpawn();
            
        if (Oleada >= 4)
        {
            FinDeNivel();
        } 
        else 
        {
            Debug.Log("Oleada Finalizada");
            menuOleadas.gameObject.SetActive(true);
        }

    }

    public void OnRetryOleada ()
    {

        Oleada = 1;
        enemMuertos = 0;
        SetOleada();


    }
        public void ActualizarOleada()
    {
        Oleada++;
        enemMuertos = 0;
        Debug.Log("Siguiente Oleada: " + Oleada);
        SetOleada();


    }
  
    public void FinDeNivel()
    {
        SendMessageUpwards("MostrarEstadisticas", 1);
        Debug.Log("NIVEL FINALIZADO");
        /* menu fin de juego*/
    }

}
