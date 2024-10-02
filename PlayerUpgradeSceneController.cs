using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUpgradeSceneController : MonoBehaviour
{
    private int lifeUpgrade = 1;
    private int manaUpgrade = 1;
    public CardDatabase Data;
    public CardData cardData;
    public GameObject pickNewCardCanva;

    public setPlayingCard card1;
    public setPlayingCard card2;
    public setPlayingCard card3;

    private List<int> temp = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        DisplayRandomCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pickMoreLifeButton()
    {
        GameManager.Instance.SetLifeUpgrade(lifeUpgrade);
        SceneManager.LoadScene("siguiente");
    }

    public void pickMoreManaButton()
    {
        GameManager.Instance.SetManaUpgrade(manaUpgrade);
        SceneManager.LoadScene("siguiente");
    }

    public void pickNewCardButton()
    {
        pickNewCardCanva.SetActive(true);
        DisplayRandomCards();
    }

    private void DisplayRandomCards()
    {
        if (Data == null || Data.Cartas == null || Data.Cartas.Count == 0)
        {
            Debug.LogError("Card data is not properly initialized.");
            return;
        }

        int cardCount = Data.Cartas.Count;
        HashSet<int> selectedCardIDs = new HashSet<int>();
        System.Random random = new System.Random();

        while (selectedCardIDs.Count < 3)
        {
            int randomID = random.Next(0, cardCount);
            selectedCardIDs.Add(randomID);
        }

        int[] cardIDs = new int[3];
        selectedCardIDs.CopyTo(cardIDs);

        card1.SetCard(cardIDs[0]);
        card2.SetCard(cardIDs[1]);
        card3.SetCard(cardIDs[2]);
    }

    public void OnCardClick(int id)
    {
        int cardId = cardData.ca_id;
        if (id == 1) 
        {
            cardId = card1.cardData.ca_id;
        }
        if (id == 2)
        {
            cardId = card2.cardData.ca_id;
        }
        if (id == 3)
        {
            cardId = card3.cardData.ca_id;
        }


        Debug.Log(cardId);
        temp = GameManager.Instance.GetMazoJugar();
        Debug.Log(temp);
        temp.Add(cardId);
        Debug.Log(temp);
        GameManager.Instance.SetMazoJugar(temp);
        SceneManager.LoadScene("siguiente");
    }
}
