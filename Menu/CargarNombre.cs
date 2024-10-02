using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CargarNombre : MonoBehaviour
{
    private GameObject nombre1;
    public Text textonombre;

    //Este script se encarga de cargar el nombre que introduce el usuario en la escena de "personajes" y lo carga en la escena de "siguiente", además
    // hay posibilidad de utilizarlo en la base de datos para guardar un nombre del usuario con una puntuacón pero de momento no tiene funcionalidad
    //solo sirve para cargar el nombre en otra escena

    private void Start()
    {
       // nombre1 = GameObject.FindGameObjectWithTag("nombre1");
        //nombre1.GetComponent<Text>().text = PlayerPrefs.GetString("nombreUsuario");
    }
}