using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasaescena1 : MonoBehaviour
{

    //Este script es el encargado de moverse entre escenas

    public void Pasar
        ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (Input.anyKeyDown) {

          Pasar();
        }

    }
    
}
