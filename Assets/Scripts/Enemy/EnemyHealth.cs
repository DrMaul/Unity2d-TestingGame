using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string Nombre;
    public int totalHealth;
    public int health;
    public int armor;
    public int puntos;
    public int ID;
    public bool MuerteXBala = false;
    public bool golpeBullet;
    

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    private void OnEnable()
    {
        health = totalHealth;
        _renderer.color = Color.white;
        
    }

    public void AddDamage(int golpe) //va a recibir clase
    {
       

        health = health - golpe;
        StartCoroutine("EfectoVisual");
        if (health <= 0)
        {    
            health = 0;
            
            if (golpeBullet == true)
            {
                MuerteXBala = true;

                
                golpeBullet = false;
                
            }

            EnemyDeath();
        }
        
    }

    private IEnumerator EfectoVisual()
    {
        _renderer.color = Color.black;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private void bulletDamage(int damage)
    {
        golpeBullet = true;
        AddDamage(damage);
        

    }

    public void EnemyDeath()
    {

        
        gameObject.SetActive(false);
        SendMessageUpwards("RestarEnemy");
        SendMessageUpwards("ValidarMuerteXBala", MuerteXBala);
        SendMessageUpwards("ControlPuntajeEnemigos",Nombre);
        SendMessageUpwards("SumarContadorEnemigosMuertos");
        MuerteXBala = false;


    }
}
