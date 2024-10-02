using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class volumen : MonoBehaviour
{
    public Slider slider;
    public float valorSlider;
    


    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        
    }

    // Este metodo cambia el volumen en el panel de opciones segun el usuario quiera moviendo la barra(slider) que aparece.

    public void ChangeSlider(float valor)
    {
        slider.value=valor;
        PlayerPrefs.SetFloat("VolumenAudio", valorSlider);
        AudioListener.volume =slider.value;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
