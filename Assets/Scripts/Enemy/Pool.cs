using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject EnemyArray;
    public GameObject[] cantEnemigos;

    private int amount = 6;
    private int indice;


    private int poolContadorEnemy = 0;
    private int poolContadorStrong = 0;

    




    private void Start()
    {

        cantEnemigos = new GameObject[EnemyArray.transform.childCount];

        for (int i = 0; i < 2; i++)
        {
            cantEnemigos[i] = EnemyArray.transform.GetChild(i).gameObject;
        }

  
        Debug.Log("Total de enemigos en el Bestiario: " + cantEnemigos.Length);



    }

   


    //LIMITE DE POOL
    public void IniciarPool()
    {
        Debug.Log("Se prende InitializePool");


        for (int i = 0; i < amount; i++)
        {
            
            AddEnemyToPool();
            

        }
    }

    //AÑADO ENEMIGO A POOL
    public void AddEnemyToPool()
    {
        

         if ((amount*80/100) != poolContadorEnemy)
                {
                 indice = 0;
                poolContadorEnemy++;

                 }

            else if ((amount * 20 / 100) != poolContadorStrong)
            {
                indice = 1;
                poolContadorStrong++;
            
                
            }
            

        
       
        GameObject enemy = Instantiate(cantEnemigos[indice], this.transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);

  
        


    }
}
