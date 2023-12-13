using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public controlOleadas _controlOleadas;


    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth _enemyHealth = collision.GetComponent<EnemyHealth>();

            string _enemyName = _enemyHealth.Nombre;

            switch (_enemyName)
            {
                case "Enemy":
                    //Debug.Log("Sumar contador Jose");
                    _controlOleadas.arrayContadorEnemigos[0]++;
                    break;

                case "Strong Enemy":
                    //Debug.Log("Sumar contador orasio");
                    _controlOleadas.arrayContadorEnemigos[1]++;
                    break;

                default:
                    break;

            }

            collision.attachedRigidbody.gameObject.SetActive(false);
            SendMessageUpwards("RestarEnemy");
        }

        else if (collision.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = collision.GetComponent<PlayerHealth>();
            _playerHealth.Muerte(0);
            
        }
            
        
    }
}
