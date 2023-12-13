using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
   
    public int totalHealth = 3;
    public RectTransform hearthUI;
    public RectTransform gameOverMenu;
    //public GameObject hordes;
    private GameObject spawnObject;
    public bool IDDQD;

    
    public int health;
    //-------------------------------------------------
    private float heartSize = 16f;
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerController _controller;
    //private EnemySpawn _enemySpawn;
    private GameObject Puntos;
    private GameObject Oleadas;

    private Score _score;
    private controlOleadas _controlOleadas;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
        Debug.Log("Se empezó nivel");
        //_enemySpawn = hordes.GetComponent<EnemySpawn>();

    }

    void Start()
    {
        health = totalHealth;
        

        Puntos = Instanciador.Instance.Puntos;
        Oleadas = Instanciador.Instance.Oleadas;

        _score = Puntos.GetComponent<Score>();
        _controlOleadas = Oleadas.GetComponent<controlOleadas>();

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += CambioDeEscena;

        // Inicializar la posición al comienzo
        CambioDeEscena(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    //skip oleada
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
		{
            _controlOleadas.SkipOleada();
            Debug.Log("Oleada Skipeada");
        }
    } 
    private void CambioDeEscena(Scene scene, LoadSceneMode mode)
    {
        // Buscar el objeto "Spawn" en la escena actual
        spawnObject = GameObject.Find("SpawnPoint");

        // Si se encuentra el objeto "Spawn", adoptar su posición
        if (spawnObject != null)
        {
            transform.position = spawnObject.transform.position;
            _controller.enabled=true;
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto 'Spawn' en la escena.");
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        SceneManager.sceneLoaded -= CambioDeEscena;
    }


    public void AddDamage(int amount)
    {
        health = health - amount;
       // SendMessageUpwards("ControlPuntajePlayer", amount *(-2));
        _score.ControlPuntajePlayer(amount *(-2));

        // Visual Feedback
        StartCoroutine("VisualFeedback");

        // Game  Over
        if (health <= 0 && IDDQD==false)
        {
            health = 0;
            Muerte(1);
        }

        hearthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        //Debug.Log("Player got damaged. His current health is " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        // Max health
        if (health > totalHealth)
        {
            health = totalHealth;


        }

        hearthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        //Debug.Log("Player got some life. His current health is " + health);
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private void OnEnable()
    {
        _animator.enabled = true;
        _controller.enabled = true;
        //_enemySpawn.StartSpawn();

    }


    public void Muerte(int N)
    {
        int tipoMuerte = N;
        switch (tipoMuerte)
        {
            case 0://Muerte por vacio
                OnDeath("vacio");

                break;
            case 1://Muerte por enemigo
                OnDeath("Enemy");

                break;
            


        }


    }

    private void OnDeath(string Muerte)
    {
       // SendMessageUpwards("PararSpawn");
        _controlOleadas.PararSpawn();

        gameObject.SetActive(false);
        //gameOverMenu.gameObject.SetActive(true);
        //SendMessageUpwards("ControlVidas", Muerte);
        _score.ControlVidas(Muerte);
        
        // hordes.SetActive(false);

        // _enemyPooling.enabled = false;
       //  _enemySpawn.StopSpawn();

        _animator.enabled = false;
        _controller.enabled = false;


    }

    public void OnRetry()
    {
        
        health = totalHealth;
        hearthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        _renderer.color = Color.white;
        this.transform.position = spawnObject.transform.position;
        // hordes.SetActive(true);



        // _enemyPooling.enabled = true;
        
    }


}
