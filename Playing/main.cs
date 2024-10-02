using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class main : MonoBehaviour
{
    // ########## CARD ##########


    public bool isVisible = false;
    public float moveDistance = 0.5f;

    public card card;
    public GameObject[] menuSeleccion;

    public CardSetter[] cardSetter;
    public int CurrentInt;


    // ########## PLAYER ##########
    //public Player player;

    // ########## ENEMY ##########

    public enemyIA enemy;

    //public UIManager uiManager;

    private bool playerTurn;

    void Start()
    {
        // ########## CARD ##########

        List<int> currentDeck = GameManager.Instance.GetMazoJugar();


    }

    // ########## CARD ##########

    public void CargaCarta(int startID)
    {
        CurrentInt = startID;
        int idtemp = startID;


        for (int i = 0; i < 2; i++)
        {
            menuSeleccion[i].GetComponent<CardSetter>().SetCard(idtemp);
            //Agreagar que el Boton tenga el mismo id que cada carta
            Debug.Log("Id Temporal " + idtemp);
            idtemp++;
        }

    }
    public void testButton()
    {
        Debug.Log("El boton ha sido pulsado");
    }
    public void ToggleVisibility()
    {
        if (isVisible)
        {
            HideCard();
        }
        else
        {
            ShowCard();
        }
    }

    public void ShowCard()
    {
        transform.Translate(Vector3.up * moveDistance);
        isVisible = true;
    }

    public void HideCard()
    {
        transform.Translate(Vector3.down * moveDistance);
        isVisible = false;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        while (!GameIsOver())
        {
            if (playerTurn)
            {
                yield return PlayerTurn();
            }
            else
            {
                yield return EnemyTurn();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PlayerTurn()
    {
        //uiManager.DisplayMessage("Player's Turn");
        //player.DrawCard();
        //player.AddMana(1);
        playerTurn = false; // Cambia al turno del enemigo
        yield return null;
    }

    IEnumerator EnemyTurn()
    {
        //uiManager.DisplayMessage("Enemy's Turn");
        enemy.randomizeAction();
        playerTurn = true; // Cambia al turno del jugador
        yield return null;
    }

    bool GameIsOver()
    {
        // Aquí defines tus condiciones de victoria o derrota
        
        return false;
    }
}
