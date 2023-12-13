using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    private ArmasPlayer _armasPlayer;

    private void Awake()
    {
        _armasPlayer = GetComponent<ArmasPlayer>();
    }

    public void Cura()
    {
        SendMessageUpwards("AddHealth", _armasPlayer.Daño);

        Destroy(gameObject);
        
    }
}
