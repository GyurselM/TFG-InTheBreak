using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public List<int> actualDeck;
    public setPlayingCard card1;
    public setPlayingCard card2;
    public setPlayingCard card3;
    public GameObject[] cards;
    private Vector3[] originalPositions;
    public float yOffset = 60f;
    public float positionThreshold = 0.1f;
    public enemyIA enemyIA;
    public ChatManager chatManager;

    public GameObject win;

    //ENEMY INFO

    public TextMeshProUGUI enemyLifeText;
    public TextMeshProUGUI shieldLifeText;
    public TextMeshProUGUI EnemyInfo;
    public GameObject sleepIcon;
    public GameObject stunIcon;
    public GameObject poisonIcon;

    //PLAYER INFO
    public int iniPlayerLifePoints;
    public int iniPlayerShieldPoints;
    public int iniPlayerManaPoints;
    public int actualPlayerLifePoints;
    public int actualPlayerShieldPoints;
    public int actualPlayerManaPoints;
    private int maxPlayerManaPoints = 6;
    private int actualMaxPlayerManaPoints;
    public int manaIncrement = 1;
    public int lifeUpgradeMultiplier = 5;

    public TextMeshProUGUI playerLifeText;
    public TextMeshProUGUI playerShieldText;
    public TextMeshProUGUI playerManaText;
    public TextMeshProUGUI playerInfo;

    public Image playerIcon;
    

    //TURN
    public bool isPlayerTurn;
    public bool quitControlBool; 
    public int enemyPoisonCount;
    public int poisonDamage;
    public GameObject dontTouchPanel;

    //GRAVEYARD AND DECK
    public List<int> graveyard;
    public TextMeshProUGUI graveyardText;
    public TextMeshProUGUI deckText;
    public ScrollRect scrollView;
    public soundManagerMenu soundManager;

    public GameObject playerTurnPanel;
    public GameObject enemyTurnPanel;
    public imagesManager imagesManager;
    

    private class CardInfo
    {
        public string TypeClass;
        public int TimeEffect;
        public int UD;
        public int ManaCost;
    }
    private Dictionary<int, CardInfo> cardInfos = new Dictionary<int, CardInfo>();

    public class PoisonEffect
    {
        public int Turns { get; set; }
        public int Damage { get; set; }

        public PoisonEffect(int turns, int damage)
        {
            Turns = turns;
            Damage = damage;
        }
    }
    private List<PoisonEffect> poisonEffects = new List<PoisonEffect>();

    void Start()
    {
        graveyard = new List<int>(); // Initialize graveyard
        GetDeck();
        Debug.Log("Initial Deck: " + string.Join(", ", actualDeck));

        // Asegúrate de que todas las cartas se registren
        RegisterAllCardInfos();

        Shuffle(actualDeck);

        if (actualDeck.Count >= 3)
        {
            card1.SetCard(GetNextCard());
            card2.SetCard(GetNextCard());
            card3.SetCard(GetNextCard());
        }
        else
        {
            Debug.LogError("Not enough cards in the deck!");
        }

        originalPositions = new Vector3[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null)
            {
                originalPositions[i] = cards[i].transform.position;
            }
            else
            {
                Debug.LogError("Card GameObject is not assigned at index " + i);
            }
        }
        //Initialize enemy info
        SetEnemyInfo();
        //Initialize player info
        SetPlayer();
        SetPlayerInfo();
        
        //Initialize turn bools
        isPlayerTurn = true;
        quitControlBool = false;
        // Inicializa el soundManager
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<soundManagerMenu>();
            if (soundManager == null)
            {
                Debug.LogError("SoundManager no encontrado en la escena.");
            }
        }
        poisonIcon.SetActive(false);
        sleepIcon.SetActive(false);
        stunIcon.SetActive(false);
        imagesManager = FindObjectOfType<imagesManager>();
        if (imagesManager == null)
        {
            Debug.LogError("imagesManager no encontrado en la escena.");
        }
    }


    void Update()
    {
        UpdateEnemyInfo();
        UpdatePlayerInfo();
        UpdateUiInfo();
        dontTouchPanel.SetActive(quitControlBool);
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    //MARK:- Deck Manager

    public void DrawCards(int numberOfCards)
    {
        int emptyCardSlots = 0;

        // Count how many card slots are empty
        foreach (GameObject cardObj in cards)
        {
            if (!cardObj.activeSelf)
            {
                emptyCardSlots++;
            }
        }

        // Draw cards only if there are empty slots and the deck has enough cards
        for (int i = 0; i < numberOfCards && emptyCardSlots > 0; i++)
        {
            // Check if the deck is empty and if the graveyard has cards to reshuffle
            if (actualDeck.Count == 0)
            {
                if (graveyard.Count > 0)
                {
                    actualDeck.AddRange(graveyard);
                    graveyard.Clear();
                    Shuffle(actualDeck);
                }
                else
                {
                    Debug.Log("No more cards to draw!");
                    break;
                }
            }

            // Draw a card from the deck
            int cardId = GetNextCard();

            // Find the first inactive card slot and set the new card
            foreach (GameObject cardObj in cards)
            {
                setPlayingCard card = cardObj.GetComponent<setPlayingCard>();
                if (!cardObj.activeSelf)
                {
                    card.SetCard(cardId);
                    cardObj.SetActive(true);
                    emptyCardSlots--; // Decrease the number of empty slots
                    break;
                }
            }
        }
    }
    public void GetDeck()
    {
        actualDeck = new List<int>( GameManager.Instance.GetMazoJugar());

    }
    public int GetNextCard()
    {
        if (actualDeck.Count == 0)
        {
            Debug.LogError("No more cards in the deck!");
            return -1;
        }

        int i = actualDeck[0];
        actualDeck.RemoveAt(0);
        Debug.Log(actualDeck);
        return i;
    }

    public void cardMovement(GameObject selectedCard)
    {
        Debug.Log("Moving card: " + selectedCard.name);

        for (int i = 0; i < cards.Length; i++)
        {
            GameObject card = cards[i];
            if (card == selectedCard)
            {
                if (IsPositionClose(card.transform.position, originalPositions[i]))
                {
                    Debug.Log("Card moved to show position: " + card.name);
                    card.transform.position += new Vector3(0, yOffset, 0);
                    soundManager.onTouchCardSoundReproduce();
                }
                else
                {
                    Debug.Log("Using card: " + card.name);
                    UseCard(card);
                    card.transform.position = originalPositions[i];
                }
            }
            else
            {
                card.transform.position = originalPositions[i];
            }
        }
    }

    private void UseCard(GameObject card)
    {
        int cardId = card.GetComponent<setPlayingCard>().cardData.ca_id;
        Debug.Log("Using card with ID: " + cardId);

        if (cardInfos.TryGetValue(cardId, out CardInfo cardInfo))
        {
            if(actualPlayerManaPoints >= cardInfo.ManaCost) { 
                switch (cardInfo.TypeClass)
                {
                    case "attack":
                        Debug.Log("Hago " + cardInfo.UD + " de daño");
                        ApplyDamage(cardInfo.UD);
                        chatManager.AddMessage("El jugador hace " + cardInfo.UD + " de daño");
                        soundManager.onHurtSoundReproduce();
                        break;
                    case "shield":
                        soundManager.onShieldSoundReproduce();
                        Debug.Log("Doy " + cardInfo.UD + " de escudo");
                        ApplyShieldCard(cardInfo.UD);
                        chatManager.AddMessage("El jugador se pone " + cardInfo.UD + " de escudo");
                        break;
                    case "heal":
                        Debug.Log("Curo " + cardInfo.UD + " de vida");
                        chatManager.AddMessage("El jugador se cura " + cardInfo.UD + " de vida");
                        ApplyHealCard(cardInfo.UD);
                        soundManager.onHealSoundReproduce();
                        break;
                    case "stun":
                        Debug.Log("Stuneo " + cardInfo.TimeEffect + " turnos");
                        enemyIA.ApplyStun(cardInfo.TimeEffect);
                        chatManager.AddMessage("Stunea al enemigo " + cardInfo.TimeEffect + " turnos");
                        soundManager.onStunSoundReproduce();
                        stunIcon.SetActive(true);
                        break;
                    case "poison":
                        Debug.Log("Enveneno " + cardInfo.TimeEffect + " turnos, haciendo " + cardInfo.UD + " de daño");
                        CreatePoisonDmgObject(cardInfo.TimeEffect, cardInfo.UD);
                        chatManager.AddMessage("Enveneno al enemigo " + cardInfo.TimeEffect + " turnos, haciendo " + cardInfo.UD + " de daño por turno");
                        soundManager.onPoisonSoundReproduce();
                        poisonIcon.SetActive(true);
                        break;
                    case "sleep":
                        Debug.Log("Duermo " + cardInfo.TimeEffect + " turnos");
                        enemyIA.ApplySleep(cardInfo.TimeEffect);
                        chatManager.AddMessage("Duermo " + cardInfo.TimeEffect + " turnos");
                        soundManager.onSleepSoundReproduce();
                        sleepIcon.SetActive(true);
                        break;
                    default:
                        Debug.LogWarning("Tipo de carta desconocido: " + cardInfo.TypeClass);
                        break;
                }
                actualPlayerManaPoints -= cardInfo.ManaCost;
                graveyard.Add(cardId);
                Debug.Log(graveyard);
                card.SetActive(false); 
            }
            else
            {
                Debug.Log("No tengo mana suficiente, mi mana actual es: " + actualPlayerManaPoints);
            }
        }
        else
        {
            Debug.LogError("Información de carta no encontrada para ID: " + cardId);
        }
        UpdateEnemyInfo();
    }

    //MARK:- Card movement
    public void OnCardClick(GameObject clickedCard)
    {
        cardMovement(clickedCard);
    }

    private bool IsPositionClose(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2) < positionThreshold;
    }
    private void ResetCardPositions()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.position = originalPositions[i];
        }
    }

    //MARK:- Card info
    public void RegisterCardInfo(int cardId, string typeclass, int timeEffect, int ud, int mana)
    {
        CardInfo cardInfo = new CardInfo
        {
            TypeClass = typeclass,
            TimeEffect = timeEffect,
            UD = ud,
            ManaCost = mana
        };

        if (cardInfos.ContainsKey(cardId))
        {
            cardInfos[cardId] = cardInfo;
        }
        else
        {
            cardInfos.Add(cardId, cardInfo);
        }

        Debug.Log("Registered card info for ID: " + cardId + " - Type: " + typeclass + " TimeEffect: " + timeEffect + " UD: " + ud);
    }

    void RegisterAllCardInfos()
    {
        List<CardData> allCards = GetAllCardsFromDatabase();
        foreach (CardData card in allCards)
        {
            RegisterCardInfo(card.ca_id, card.ca_class, card.ca_time_effect, card.ca_ud, card.ca_mana);
        }
    }

    List<CardData> GetAllCardsFromDatabase()
    {
        CardDatabase cardDatabase = FindObjectOfType<CardDatabase>();
        if (cardDatabase != null)
        {
            return cardDatabase.Cartas;
        }
        else
        {
            //Debug.LogError("CardDatabase no encontrado.");
            return new List<CardData>();
        }
    }

    public void ApplyDamage(int damage)
    {
        if (enemyIA.actualShield > 0)
        {
            if (damage > enemyIA.actualShield)
            {
                damage -= enemyIA.actualShield;
                enemyIA.actualShield = 0;
                enemyIA.actualLife -= damage;
                Debug.Log("Shield broken, remaining damage applied to life: " + damage);
            }
            else
            {
                enemyIA.actualShield -= damage;
                Debug.Log("Damage applied to shield: " + damage);
                
            }
        }
        else
        {
            enemyIA.actualLife -= damage;
           
        }
        // Asegúrate de que la vida del enemigo no baje por debajo de 0
        if (enemyIA.actualLife <= 0)
        {
            enemyIA.actualLife = 0;
            Debug.Log("¡Tu enemigo ha sido derrotado!");
            soundManager.onWinSoundReproduce();
            win.SetActive(true);
        }
    }

    public void GoToPlayerUpgrade()
    {
        SceneManager.LoadScene("playerUpgrades");
    }

    // MARK:- Poison Damage
    public void CreatePoisonDmgObject(int turns, int damage)
    {
        poisonEffects.Add(new PoisonEffect(turns, damage));
        poisonIcon.SetActive(true);
    }

    public void CheckAndDealPoisonDamage()
    {
        int totalDamage = 0;

        for (int i = poisonEffects.Count - 1; i >= 0; i--)
        {
            PoisonEffect effect = poisonEffects[i];
            totalDamage += effect.Damage;
            effect.Turns--;

            if (effect.Turns <= 0)
            {
                poisonEffects.RemoveAt(i);
            }
        }

        // Aplica el daño total al enemigo aquí
        ApplyDamage(totalDamage);
        if (totalDamage > 0) 
        {
            chatManager.AddMessage("Haces al enemigo " + totalDamage + " puntos de daño de veneno");
            soundManager.onPoisonSoundReproduce();
        }
        poisonIcon.SetActive(poisonEffects.Count > 0);
    }
    public void ApplyShieldCard(int shield)
    {
        int maxShieldPoints = 30;
        actualPlayerShieldPoints += shield;
        if (actualPlayerShieldPoints >= maxShieldPoints) 
        {
            actualPlayerShieldPoints = maxShieldPoints;
            Debug.Log("Maximus shield points obtained by the player");
        }

    }
    public void ApplyHealCard(int life)
    {
        int maxLifePoints = 50;
        actualPlayerLifePoints += life;
        if (actualPlayerLifePoints >= maxLifePoints)
        {
            actualPlayerLifePoints = maxLifePoints;
            Debug.Log("Maximus life points obtained by the player");
          
        }

    }
    //MARK:- Set enemy info
    public void SetEnemyInfo()
    {
        enemyLifeText.text = "" + enemyIA.actualLife;
        shieldLifeText.text = "" + enemyIA.actualShield;
    }
    private void UpdateEnemyInfo()
    {
        enemyLifeText.text = "" + enemyIA.actualLife;
        shieldLifeText.text = "" + enemyIA.actualShield;
    }
    private void SetPlayer()
    {
        int upgradeLifei = GameManager.Instance.GetLifeUpgrade();
        int upgradeManai = GameManager.Instance.GetManaUpgrade();
        iniPlayerLifePoints = 30 + upgradeLifei * lifeUpgradeMultiplier;
        actualPlayerLifePoints = iniPlayerLifePoints;
        iniPlayerShieldPoints = 20;
        actualPlayerShieldPoints = iniPlayerShieldPoints;
        iniPlayerManaPoints = 1 + upgradeManai;
        actualPlayerManaPoints = iniPlayerManaPoints;
        actualMaxPlayerManaPoints = actualPlayerManaPoints;
    }
    private void SetPlayerInfo()
    {
        playerLifeText.text = "" + actualPlayerLifePoints;
        playerShieldText.text = "" + actualPlayerShieldPoints;
        playerManaText.text = "" + iniPlayerManaPoints;
    }
    private void UpdatePlayerInfo()
    {
        playerLifeText.text = "" + actualPlayerLifePoints;
        playerShieldText.text = "" + actualPlayerShieldPoints;
        playerManaText.text = "" + actualPlayerManaPoints;
    }
    public void UpdatePlayerSprite()
    {
        
        float lifePercentage = (float)actualPlayerLifePoints / iniPlayerLifePoints;

        if (lifePercentage <= 0.3f)
        {
            playerIcon.sprite = imagesManager.spriteArrayToUse[2];
        }
        else if (lifePercentage <= 0.6f)
        {
            playerIcon.sprite = imagesManager.spriteArrayToUse[1];
        }
        else
        {
            playerIcon.sprite = imagesManager.spriteArrayToUse[0];
        }
    }
    private void UpdateUiInfo()
    {
        deckText.text = "" + actualDeck.Count;
        graveyardText.text = "" + graveyard.Count;
    }
    
    public void ChangeTurnToEnemyButton()
    {
        if (isPlayerTurn == true)
        {
            ResetCardPositions();
            StartCoroutine(ChangeTurnToEnemy());
        }
    }

    private void ScrollChatToBottom()
    {
        scrollView.verticalNormalizedPosition = 0f;
    }

    public void ManaManager()
    {
        if (actualMaxPlayerManaPoints < maxPlayerManaPoints)
        {
            actualMaxPlayerManaPoints += manaIncrement;
        }
        actualPlayerManaPoints = actualMaxPlayerManaPoints;
    }
    public void chatPrueba()
    {
        chatManager.AddMessage("Machape");
    }
    public void chatPrueb2a()
    {
        chatManager.AddMessage("Machape2222");
    }

    public IEnumerator ChangeTurnToEnemy()
    {
        enemyTurnPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        enemyTurnPanel.SetActive(false);
        isPlayerTurn = false;
        quitControlBool = true;
        yield return new WaitForSeconds(1);
        CheckAndDealPoisonDamage();
        yield return new WaitForSeconds(1);
        enemyIA.randomizeAction();
        yield return new WaitForSeconds(1);
        ManaManager();
        DrawCards(3); // Draw up to 3 cards to fill the hand
        isPlayerTurn = true;
        quitControlBool = false;
        playerTurnPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        playerTurnPanel.SetActive(false);
        yield return null;
    }
    public IEnumerator ChangeSpriteWhenDamaged()
    {
        Sprite originalSprite = playerIcon.sprite; // Guarda el sprite original del jugador
        playerIcon.sprite = imagesManager.spriteArrayToUse[4]; // Cambia al sprite de daño
        yield return new WaitForSeconds(1); // Espera un segundo
        playerIcon.sprite = originalSprite; // Vuelve al sprite original
    }

    public void CheckWin()
    {
       
    }
}