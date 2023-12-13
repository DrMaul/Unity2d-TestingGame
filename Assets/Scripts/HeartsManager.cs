using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour 
{

    public GameObject Player; 

    private GameObject Hearts;

    private SpriteRenderer _renderer;
    private Collider2D _collider;



    void Start()
    {
        
    }

   
    public void ActiveHeart ()
    {
        

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Hearts = this.transform.GetChild(i).gameObject;
            Hearts.SetActive(true);

            GameObject lightingParticles = Hearts.transform.GetChild(0).gameObject;
            GameObject burstParticles = Hearts.transform.GetChild(1).gameObject;
            lightingParticles.SetActive(true);
            burstParticles.SetActive(false);
            

            _renderer = Hearts.GetComponent<SpriteRenderer>();
            _renderer.enabled = true;

            _collider = Hearts.GetComponent<Collider2D>();
            _collider.enabled = true;
        }
    }

    
    
}
