using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CardSetter : MonoBehaviour
{

    public int idAct;
    public int act;
    public int IdNuevo;
    public CardData cardData;
    public CardDatabase Data;
    public card card;
    public Button nextPag;
    public Button prevPag;
    public GameObject canvas1;

    //campo de cartas
    public Image sprite;
    public TextMeshProUGUI name;
    public TextMeshProUGUI UD;
    public TextMeshProUGUI description;
    public TextMeshProUGUI manaInt;
    public string typeclass;
    public Button boton;
    public Color C;
    public Color C2;
    public Image selection;

    // En start auto referenciamos el boton
    void Start()
    {
        this.boton = this.gameObject.GetComponentInChildren<Button>();


    }

    // En Update se lanza para comprobar si la carta esta seleccionado
    void Update()
    {
        Comprobacion();
    }
    //lo que pasa aqui es que cuando entra en el mazo para seleccionar cartas esto lo que detecta que carta estas seleccionado y manda al sript de card al metodo SeleccionCartas y manda id de la carta
    public void pickCard()
    {
        //Debug.Log("PICK");
        canvas1.GetComponent<card>().SeleccionCarta(IdNuevo);


    }
    //Metodo para comprobar si la carta esta selecionado y si lo esta salta en verde indicando que esta seleccionado
    public void Comprobacion()
    {
        if (card.MazoJugar.Contains(IdNuevo))
        {
            selection.gameObject.SetActive(true);
        }
        else
        {
            selection.gameObject.SetActive(false);
        }

    }
    // Esto es un metodo donde pasamos por todas las cartas que hemos guardado y los cargamos en las cartas, cada parte nombre, descripcion, mana, daño y etc...
    public void SetCard(int id)
    {

        IdNuevo = id;
        //Debug.Log("IDs: " + id);
        for (int i = 0; i < Data.Cartas.Count; i++)
        {

            if (Data.Cartas[i].ca_id == id)
            {
                cardData = Data.Cartas[i];
                break;
            }
        }


        typeclass = cardData.ca_class;
        switch (typeclass)
        {
            case "attack":
                UD.text = "Hago " + cardData.ca_ud.ToString() + " de daño";
                break;
            case "shield":
                UD.text = "Doy " + cardData.ca_ud.ToString() + " de escudo";
                break;
            case "heal":
                UD.text = "Curo " + cardData.ca_ud.ToString() + " de vida";
                break;
            case "stun":
                UD.text = "Stuneo " + cardData.ca_time_effect.ToString() + " turnos";
                break;
            case "poison":
                UD.text = "Enveneno " + cardData.ca_time_effect.ToString() + " turnos, haciendo " + cardData.ca_ud + " de daño";
                break;
            case "sleep":
                UD.text = "Duermo " + cardData.ca_time_effect.ToString() + " turnos";
                break;

        }

        name.text = cardData.ca_name;
        description.text = cardData.ca_description;
        manaInt.text = cardData.ca_mana.ToString();
        sprite.sprite = Data.imagencart[id - 1];

    }
}
