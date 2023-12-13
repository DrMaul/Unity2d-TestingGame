using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int instantiateGap = 2;

    public GameObject spawns;
    public GameObject[] cantSpawns;
    public GameObject padrePool;

    public controlOleadas _controlOleadas;
    

    private bool activarSpawn = false;
    private Pool _pool;

    private int cantEnemyActivos = 0;
    private int numSpawner;
    private int totalSpawners;

    private int indiceRandom;
    private int totalPool;


    void Awake()
    {
        _pool = GetComponentInChildren<Pool>();
    }

    public void InitializePool()
    {
        _pool.IniciarPool();
    }
    // BUSCAR HIJO
    void Start()
    {
        cantSpawns = new GameObject[spawns.transform.childCount];

        for(int i=0; i < cantSpawns.Length; i++)
        {
            cantSpawns[i] = spawns.transform.GetChild(i).gameObject;
        }

        totalSpawners = cantSpawns.Length;
        //Debug.Log("Total de spawners: " + totalSpawners);
    }
     
    //SPAWNEAR ENEMIGOS DE LA POOL
    private GameObject GetEnemyFromPool()
    {
        Debug.Log("Se inició GetEnemyFromPool");
        GameObject Enemy = null;

        totalPool = padrePool.transform.childCount;

        //Debug.Log("Total de hijos de Pool (Enemigos totales en la Pool): " + totalPool);

        

        if (activarSpawn == true)
        {
            //for (int i = 0; i < totalPool; i++)
            //{
                indiceRandom = Random.Range(0, totalPool);
                //FRANCO DICE: "LIMITAR INDICE RANDOM"
                //AGUS DICE: "JODER TIO!!"

                if (!padrePool.transform.GetChild(indiceRandom).gameObject.activeSelf)
                {
                    Enemy = padrePool.transform.GetChild(indiceRandom).gameObject;
                    Debug.Log("Se prepara el enemigo: " + indiceRandom + " para spawnear");

                    if (indiceRandom + 1 <= (totalPool * 80 / 100) && _controlOleadas.arrayContadorEnemigos[0] > 0)
                    {
                        Debug.Log("Spawnea un Enemy");

                        _controlOleadas.arrayContadorEnemigos[0]--;
                        SpawnearEnemy(Enemy);


                    }
                    if (indiceRandom + 1 > (totalPool * 80 / 100) && _controlOleadas.arrayContadorEnemigos[1] > 0)
                    {
                        Debug.Log("Spawnea un Strong");
                        _controlOleadas.arrayContadorEnemigos[1]--;
                        SpawnearEnemy(Enemy);

                    }

                }

            //}
        }
        //if (Enemy == null)
        //{
        //    Debug.Log("entro al enemy == null");
        //    _pool.AddEnemyToPool();

        //    Enemy = padrePool.transform.GetChild(transform.childCount - 1).gameObject;
        //}

               
        Debug.Log(cantEnemyActivos);

        if (cantEnemyActivos == padrePool.transform.childCount)
        {
            StopSpawn();
        }
        
        return Enemy;
    }

    private GameObject SpawnearEnemy(GameObject Enemy)
    {
        if (Enemy.activeSelf == false)
        {

            numSpawner = Random.Range(0, totalSpawners);

            Enemy.transform.position = cantSpawns[numSpawner].transform.position;

            Debug.Log("Spawnea el enemigo enemy en el spawn: " + numSpawner);
            Enemy.SetActive(true);
            cantEnemyActivos++;

        }

        return Enemy;
    }

    public void RestarEnemy()
    {
        cantEnemyActivos--;
        Debug.Log(cantEnemyActivos);

        if (cantEnemyActivos != padrePool.transform.childCount && activarSpawn == false)
        {
            StartSpawn();
        }
    }

  


    // INICIO, FINAL Y RETRY
    
    public void StartSpawn()
    {       
            
            activarSpawn = true;

            if (activarSpawn == true)
            {
                //Debug.Log("Start Spawn esta activado");
                InvokeRepeating("GetEnemyFromPool", 0.01f, instantiateGap);
            }

    }

    public void StopSpawn()
    {
        activarSpawn = false;
        //Debug.Log("Llamo  StopSpawn");
        CancelInvoke();


    }
    
    public void OnRetryPool()
    {
        
        cantEnemyActivos = 0;

        GameObject Enemy;

        for (int i = 0; i < padrePool.transform.childCount; i++)
        {
            Enemy = padrePool.transform.GetChild(i).gameObject;
            Enemy.SetActive(false);
        }

        _controlOleadas.OnRetryOleada();

    }

}
