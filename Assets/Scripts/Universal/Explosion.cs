using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{


    public float timeBeforeDestroying;

    void Start()
    {

        Invoke("Destroy", timeBeforeDestroying);

    }



    private void Destroy()
    {
        Destroy(gameObject);
    }


}
