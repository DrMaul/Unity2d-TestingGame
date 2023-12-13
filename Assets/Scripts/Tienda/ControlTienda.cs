using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlTienda : MonoBehaviour, IPointerDownHandler
{
    public GameObject StockTienda;
    public GameObject Armeria;


    
    private GameObject Player;
    private bool TocandoTienda=false;
    private bool TiendaAbierta=false;
    private GameObject Panel;
    private GameObject Inventario;
    private Inventario _inventario;
    private int SlotVacio;

    /*
     *  armas = 1 a 4
     * 
     *  pocion =5 a 8
     * 
     *  objetos =9 a 12
     */

    void Awake()
    {
        for (int i = 0; i < Armeria.transform.childCount; i++)
        {
            GameObject objetoArmeria = Armeria.transform.GetChild(i).gameObject;

            // Obtén el tipo del objeto
            string tipo = objetoArmeria.GetComponent<ArmasPlayer>().Tipo;

            // Coloca el objeto en el slot correspondiente según su tipo
            switch (tipo)
            {
                case "Arma":
                    SlotVacio = BuscarSlotLibre(0 , 4);
                    if (SlotVacio == -1) { break; }
                    ColocarEnSlot(objetoArmeria, SlotVacio);
                    break;
                case "Pocion":
                    SlotVacio = BuscarSlotLibre(4, 8);
                    if (SlotVacio == -1) { break; }
                    ColocarEnSlot(objetoArmeria, SlotVacio);
                    break;
                case "Armadura":
                    SlotVacio = BuscarSlotLibre(8, 12);
                    if (SlotVacio == -1) { break; }
                    ColocarEnSlot(objetoArmeria, SlotVacio);
                    break;
                default:
                    Debug.LogError("Tipo de objeto desconocido: " + tipo);
                    break;
            }
        }


        //recorrer StockTienda y en cada slot vacio rellenar con copia de hijo de armeria

        

    }

    int BuscarSlotLibre(int inicio, int final)
    {
        for (int i = inicio; i < final; i++)
        {
            if (StockTienda.transform.GetChild(i).childCount < 1)
            {
                return i;
            }
                        
        }
        Debug.Log("Ningun slot libre");
        return -1;
    }

    void ColocarEnSlot(GameObject objetoArmeria, int slotVacio)
    {
       
        GameObject copiaItem = Instantiate(objetoArmeria, StockTienda.transform.GetChild(slotVacio).gameObject.transform);
        copiaItem.transform.SetParent(StockTienda.transform.GetChild(slotVacio).gameObject.transform, false);



    }






    void Update()
    {
        if (TocandoTienda==true && Player != null && Input.GetKeyDown(KeyCode.E))
        {
            TiendaAbierta = !TiendaAbierta;

            //Desactivo movimiento mientras tienda está abierta
            Player.GetComponent<PlayerController>().enabled = !TiendaAbierta;
            
            
            Debug.Log("Tienda abierta = " + TiendaAbierta);
            
        }

    }

    // v---------------------DETECTAR COLISION DE TIENDA---------------------v

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
            TocandoTienda = true;
            
            //Obtener Ubicacion inventario y cantidad monedas del player
            if (Panel == null && Inventario==null)
            {
                Panel = collision.transform.Find("CanvasInventario/Background/Panel").gameObject;
                Inventario = collision.transform.Find("Inventario").gameObject;
                _inventario = Inventario.GetComponent<Inventario>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = null;
            TocandoTienda = false;
        }
    }

    // ^---------------------DETECTAR COLISION DE TIENDA---------------------^

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TiendaAbierta == true)
        {
            // Obtener la posición del puntero en la ventana
            Vector3 posicionPuntero = eventData.position;

            // Convertir la posición del puntero a coordenadas del mundo 2D
            Vector2 rayOrigen = Camera.main.ScreenToWorldPoint(posicionPuntero);

            // Realizar un raycast en 2D desde la posición del puntero
            RaycastHit2D hit = Physics2D.Raycast(rayOrigen, Vector2.zero);

            // Comprobar si el rayo golpea algún objeto
            if (hit.collider != null && hit.collider.CompareTag("ObjetoTienda"))
            {
                // Obtener el collider del objeto hijo (si existe)
                GameObject ItemTienda = hit.collider.transform.GetChild(0).gameObject;
               
                // Obtener precio del item por collider
                int PrecioItem = ItemTienda.GetComponent<ArmasPlayer>().Precio;


                if (_inventario.Monedas >= PrecioItem)
                {
                    if (ItemTienda.GetComponent<ArmasPlayer>().Tipo == "Pocion")
                    {
                        ItemTienda.transform.SetParent(Panel.transform.GetChild(4).gameObject.transform, false);
                        _inventario.Monedas -= PrecioItem;
                    }
                    else
                    {


                        for (int i = 0; i < 4; i++)
                        {
                            if (Panel.transform.GetChild(i).childCount < 1)
                            {
                                ItemTienda.transform.SetParent(Panel.transform.GetChild(i).gameObject.transform, false);
                                _inventario.Monedas -= PrecioItem;
                                break;
                            }
                        }
                    }

                }
                else { Debug.Log("NO TENES PLATA NEGRO"); }

                
                

                    Debug.Log("Objeto hijo bajo el cursor: " + ItemTienda.name);

            }
            else { Debug.Log("Le estas dando al fondo"); }

            // Agrega aquí la lógica de tu tienda
        }


       
    }








}
