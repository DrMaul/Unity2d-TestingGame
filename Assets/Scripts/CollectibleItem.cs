using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public int healthRestoration = 1;
	public GameObject lightingParticles;
	public GameObject burstParticles;

	public float contador = 0;

    public GameObject Player;


    private SpriteRenderer _renderer;
	private Collider2D _collider;
	private PlayerHealth _playerHealth;

	private void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
	}


    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && _playerHealth.health < _playerHealth.totalHealth) {
			// Cure Player 
			collision.SendMessageUpwards("AddHealth", healthRestoration);

			// Disable Collider
			_collider.enabled = false;

			// Visual stuff
			_renderer.enabled = false;
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);

			// Destroy after some  time
			//Destroy(gameObject, 2f);
			Invoke("EnabledHeart", 8f);

		}
	}


    public void EnabledHeart()
    {
        GameObject Heart = this.transform.gameObject;
		//Heart.SetActive(true);

		contador = 0;
        _collider.enabled = true;

        // Visual stuff
        _renderer.enabled = true;
        lightingParticles.SetActive(true);
        burstParticles.SetActive(false);

    }


}
