using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    //Este script es el encargado de moverse entre escenas donde en los buidSettings se ha organizado las escenas en orden y 
    // van sumando uno lo que hace que pase a la siguiente escena

    public void jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Este metodo se utiliza en el menu de opciones donde al pulsar el botón salir la aplicación se para.

    public void Salir()
    {
        Debug.Log("Salir de la aplicacion");
        Application.Quit();
    }
}
