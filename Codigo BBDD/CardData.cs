using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public List<CardData> cards;

    public int ca_id;
    public string ca_name;
    public int ca_mana;
    public int ca_ud;
    public int ca_sprite_id;
    public int ca_time_effect;
    public string ca_description;
    public string ca_class;
    public string ca_rarity;
    

    
    public CardData() { }
    //Creacion de Lista para luego llamar lo sin problema
    public static List<CardData> cartas = new List<CardData>();
    //Creamos un metodo de ParseJson donde convertimos en un lista anonima y los separabamos para luego volver a meterlo todo otra vez en una lista pero separado todo
    public static void ParseJson(string json)
    {
        List<CardData> cardList = new List<CardData>();

        // Deserializar el JSON a una lista de objetos anónimos
        var anonList = JsonConvert.DeserializeAnonymousType(json, cardList);

        // Convertir la lista de objetos anónimos a una lista de CardData
        foreach (var anonObject in anonList)
        {
            CardData card = new CardData
            {
                cards = anonObject.cards,
                ca_id = anonObject.ca_id,
                ca_name = anonObject.ca_name,
                ca_mana = anonObject.ca_mana,
                ca_ud = anonObject.ca_ud,
                ca_time_effect = anonObject.ca_time_effect,
                ca_sprite_id = anonObject.ca_sprite_id,
                ca_description = anonObject.ca_description,
                ca_class = anonObject.ca_class,
                ca_rarity = anonObject.ca_rarity

            };

            cardList.Add(card);
        }

        cartas = cardList;
    }

}


