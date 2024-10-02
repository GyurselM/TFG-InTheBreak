using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    //Es es un objeto donde se van a guardar todos los datos que descarguemos del supabase y un array de sprite para guardar todas las imagenes
    public List<CardData> Cartas = new List<CardData>();
    public Sprite[] imagencart;
}
