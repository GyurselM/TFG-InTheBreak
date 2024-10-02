using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class dragScript : MonoBehaviour
{
    public Canvas canvas;
    public GameObject card;
    public Vector3 handScale = new Vector3 (0, 0, 0);
    public Vector3 playScale = new Vector3 (0, 0, 0);
    public Vector2 position = new Vector2 (0, 0);
    public main MainContoller;
    public Vector3 startPosition;
    public int deck;
    public int cemetery;
    public void Start()
    {
        //canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        card = this.gameObject;
        startPosition = this.transform.position;
    }
    public void DragHandler(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position );
        transform.position = canvas.transform.TransformPoint(position);
        //Debug.Log("X"+ position.x + "Y" + position.y);
        
        
        if(position.y < -240)
        {
            //PONER ESCALADO CUANDO SE HAGA GRANDE O PEQUEÑO
            transform.localScale = handScale;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
            canvas.sortingOrder = 1;
        }
        else
        {
            transform.localScale = playScale;
        }
        
    }

    public void UseCard() 
    {
        canvas.sortingOrder = 0;
        if (position.y < -240)
        {
            Debug.Log("La carta esta en la mano");
            //MainContoller.DropInPosition(true);
            card.transform.position = startPosition;
        }
        else
        {
            Debug.Log("La carta se ha lanzado");
            //MainContoller.DropInPosition(false);
            card.transform.position = startPosition;
            card.SetActive(false);
        }
    }
    public void DropCard()
    {
        
    }



}
