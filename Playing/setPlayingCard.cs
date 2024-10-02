using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class setPlayingCard : MonoBehaviour
{
    public CardData cardData;
    public CardDatabase Data;
    public Image sprite;
    public TextMeshProUGUI name;
    public TextMeshProUGUI UD;
    public TextMeshProUGUI description;
    public TextMeshProUGUI manaInt;
    public string typeclass;
    public Button boton;
    public Image selection;
    public GameObject physicalCard;
    public float cardYMovement = 60f;
 


    private Vector3 cardNotShowPosition;
    private Vector3 cardShowPosition;

    void Start()
    {
        if (physicalCard == null)
        {
            Debug.LogError("Physical card is not assigned!");
            return;
        }

        cardNotShowPosition = physicalCard.transform.position;
        cardShowPosition = cardNotShowPosition + new Vector3(0, cardYMovement, 0);
    }

    public void SetCard(int id)
    {
        Debug.Log("Setting card with ID: " + id);

        cardData = Data.Cartas.Find(card => card.ca_id == id);
        if (cardData == null)
        {
            Debug.LogError("Card data not found for ID: " + id);
            return;
        }

        typeclass = cardData.ca_class;
        switch (typeclass)
        {
            case "attack":
                UD.text = "Hago " + cardData.ca_ud + " de daño";
                break;
            case "shield":
                UD.text = "Doy " + cardData.ca_ud + " de escudo";
                break;
            case "heal":
                UD.text = "Curo " + cardData.ca_ud + " de vida";
                break;
            case "stun":
                UD.text = "Stuneo " + cardData.ca_time_effect + " turnos";
                break;
            case "poison":
                UD.text = "Enveneno " + cardData.ca_time_effect + " turnos, haciendo " + cardData.ca_ud + " de daño";
                break;
            case "sleep":
                UD.text = "Duermo " + cardData.ca_time_effect + " turnos";
                break;
            default:
                Debug.LogWarning("Unknown card type: " + typeclass);
                break;
        }

        name.text = cardData.ca_name;
        description.text = cardData.ca_description;
        manaInt.text = cardData.ca_mana.ToString();
        sprite.sprite = Data.imagencart[cardData.ca_sprite_id - 1];

        // Registra la carta en el sceneManager
        var sceneManager = FindObjectOfType<sceneManager>();
        if (sceneManager != null)
        {
            sceneManager.RegisterCardInfo(cardData.ca_id, cardData.ca_class, cardData.ca_time_effect, cardData.ca_ud, cardData.ca_mana);
        }
    }

    public void cardMovement()
    {
        Debug.Log("Carta Movida");
        if (physicalCard.transform.position == cardNotShowPosition)
        {
            physicalCard.transform.position = cardShowPosition;
        }
        else if (physicalCard.transform.position == cardShowPosition)
        {
            useCard();
        }
    }

    public void useCard()
    {
        Debug.Log("Carta Usada");
    }

}
