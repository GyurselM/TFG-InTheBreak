using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CalidadImagen : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int calidad;

    // ESTE CÓDIGO FORMA PARTE DEL MENÚ DE OPCIONES, MÁS EXACTAMENTE EN LA CALIDAD DE LA IMAGEN DONDE ENCONTRAMOS
    //TRES OBPCINES (media;baja;alta) Y CON LAS CALIDADES PROPIAS DEL MONITOR VA CAMBIANDO LA CALIDAD SEGUN EL USUARI QUIERA, LOS CAMBIOS SOLO SE APRECIAN EN EL EJECUTABLE.

    // Start is called before the first frame update
    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroCalidad", 3);
        dropdown.value=calidad;
        AjustarCalidad();
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numeroCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
