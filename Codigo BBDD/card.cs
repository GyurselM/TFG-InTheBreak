using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;




public class card : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();
    public CardDatabase BBDD;
    public GameObject[] menuSeleccion;
    public Sprite[] sprites;
    public GameObject LoadingScreen;//loading por ahora esta quitado

    public GameObject PaTras;
    public GameObject PaLante;
    //Prueba para llamar cardSetter
    public CardSetter[] cardSetter;

    //Array Carta Selecionado
    public List<int> MazoJugar = new List<int>();
    public int maxCardToSelect = 5;
    
    //Selector Cartas
    public int CurrentInt;

    //Confirmar mazo
    public GameObject confirm;
    public GameObject PanelMazo;
    public personaje personaje;





    // Start llamamo la peticion de web aqui y desactivamos un boton nada mas dar le al play
    void Start()
    {
        StartCoroutine(GetText());
        //mover para atras en mazo
        PaTras.gameObject.SetActive(false);
        //confirmar mazo
        confirm.gameObject.SetActive(false);

    }
    //Cuando le das la boton de mazo sale un boton que seria esto para ir pasando de "paginas" y mirar que cartas hay 
    public void Next()
    {
        cartas(CurrentInt + 5);
        PaTras.SetActive(true);

        if (CurrentInt == 26)
        {
            PaLante.gameObject.SetActive(false);
        }
    }
    //Es lo mismo que antes solo que este va hacia atras
    public void Previous()
    {
        cartas(CurrentInt - 5);
        PaLante.gameObject.SetActive(true);
        if (CurrentInt == 1) {
            PaTras.gameObject.SetActive(false);
        }
    }
    //Aqui es donde se añade al Mazo Final para poner jugar aqui esta la logica de seleccion y deselecion, y un limitador que el tope es hasta 6 cartas
    public void SeleccionCarta(int cardID)
    {

        //Debug.Log("SELECCION");

        if (MazoJugar.Contains(cardID))
            {

            //Debug.Log("DESSELECCION");
            MazoJugar.Remove(cardID);
            
            }
        else
        {

            if (maxCardToSelect >= MazoJugar.Count)
            {
            //Debug.Log("AÑADIR");
            MazoJugar.Add(cardID);
                    
                

            }
            else
            {
            //Debug.Log("DESSELECCION");
            MazoJugar.Remove(cardID);
                    
            }
                  

        }

        if (MazoJugar.Count == 6)
        {
            confirm.gameObject.SetActive(true);
        }
        else
        {
            confirm.gameObject.SetActive(false);
        }


    }

    public void Confirm()
    {
        // Guardar el tamaño del mazo
        PlayerPrefs.SetInt("MazoSize", MazoJugar.Count);
        // Guardar cada carta individualmente
        for (int i = 0; i < MazoJugar.Count; i++)
        {
            PlayerPrefs.SetInt("Carta_" + i, MazoJugar[i]);
        }
        // Ocultar el panel de mazo
        PanelMazo.gameObject.SetActive(false);
        // Actualizar la imagen de la baraja
        personaje.Baraja.sprite = personaje.baraja[0];
    }


    //Aqui se encuentra toda la logica que se carga a las 5 cartas que se encuentra en el boton de crear mazo, basicamente coge las 5 cartas prefab y accede al CardSetter y carga los datos
    public void cartas(int startID)
    {
        CurrentInt = startID;
        int idtemp = startID;

     
        for (int i = 0; i < 5; i++)
        {
            menuSeleccion[i].GetComponent<CardSetter>().SetCard(idtemp);
            //Agreagar que el Boton tenga el mismo id que cada carta
            Debug.Log("Id Temporal " + idtemp);
            idtemp++;
        }

    }

    /* Este metodo es la peticion web que accede a supabase y coge los dados de la tabla cards
     * llega aqui como un array de string lo que hemos hecho es crear un script llamado CardData 
     * que lo que hace es que nos separa todo por cachos es decir campo nombre y su valor, es la 
     * mejor manera para poder luego cargar en las cartas.
     */
    IEnumerator GetText()
    {
        string APIKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imt0ZWhzeG9saHF1bXRudnBxb2NlIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTYzMTc1MjIsImV4cCI6MjAxMTg5MzUyMn0.xB3p8XVTAFHwBH6Q1callbfUEZNHqWkJFnxj5G6fucE";
        string APIKeyP = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imt0ZWhzeG9saHF1bXRudnBxb2NlIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY5NjMxNzUyMiwiZXhwIjoyMDExODkzNTIyfQ.ula80gVkM3C24hMX5BrEgyJvaVI1cXnkt4e4ekH9Kp8";
        string url = "https://ktehsxolhqumtnvpqoce.supabase.co/rest/v1/cards?select=*&order=ca_id.asc&apikey=" + APIKey;

        UnityWebRequest supabase = UnityWebRequest.Get(url);
        supabase.SetRequestHeader("Authorization","Bearer" + APIKeyP);
        yield return supabase.SendWebRequest();
        

        print(supabase.result);

        if (supabase.result != UnityWebRequest.Result.Success){Debug.LogError("ERROR: "+supabase.error);}
        else{

            string jsonR = supabase.downloadHandler.text;
            CardData.ParseJson(jsonR);
            Debug.Log(jsonR);

            cards = CardData.cartas;
            BBDD.Cartas = cards;
            Debug.Log("Reconstructed CardData: " + CardData.cartas);
            LoadingScreen.SetActive(false);//Loading esta quitado por ahora

        }
    }
}
