using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resolucion : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolucionesdropdown;
    Resolution[] resoluciones;

    //Start is called before the first frame update
    void Start()
    {
      if (Screen.fullScreen)
        {

             toggle.isOn = true;
        }
        else
        {
           toggle.isOn = false;
        }
        RevisarResolucion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesdropdown.ClearOptions();
        List<string> opciones = new List<string> ();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucionesdropdown.AddOptions(opciones);
        resolucionesdropdown.value = resolucionActual;
        resolucionesdropdown.RefreshShownValue();

        resolucionesdropdown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void CambiarResolucion( int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesdropdown.value);

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
