using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyIA : MonoBehaviour
{
    // Diff
    public int difficulty;

    // Life
    public int iniLife;
    public int actualLife;

    // Shield
    public int iniShield;
    public int actualShield;

    // Damage
    public int damageHit;

    // Data
    private int[] arrayNums = new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
    private int[] arrayNumsShield = new int[] { 1, 1, 1, 2, 2, 3 };

    // Turn
    public sceneManager sceneManager;
    public ChatManager chatManager;

    // States
    private int sleepTurnsRemaining;
    private int stunTurnsRemaining;
    public soundManagerMenu soundManager;

    public GameObject lose;

    public void Start()
    {
        iniLife = createIniLife();
        iniShield = createIniShield();
        actualLife = iniLife;
        actualShield = iniShield;

        difficultySelection();

        // Inicializa el soundManager
        if (soundManager == null)
        {
            soundManager = FindObjectOfType<soundManagerMenu>();
            if (soundManager == null)
            {
                Debug.LogError("SoundManager no encontrado en la escena.");
            }
        }
    }

    public void randomizeAction()
    {
        if (sleepTurnsRemaining > 0)
        {
            Debug.Log("Enemy is sleeping for " + sleepTurnsRemaining + " more turns.");
            chatManager.AddMessage("El enemigo sigue dormido por " + sleepTurnsRemaining + " turnos");
            sleepTurnsRemaining--;

            if (sleepTurnsRemaining == 0)
            {
                sceneManager.sleepIcon.SetActive(false);
            }
            return;
        }

        Debug.Log("Enemy turn started.");
        int randomNumberAction;

        if (actualLife == iniLife)
        {
            randomNumberAction = Random.Range(1, 3);
        }
        else if (actualLife <= 10)
        {
            randomNumberAction = 3;
        }
        else
        {
            randomNumberAction = Random.Range(1, 4);
        }

        if (stunTurnsRemaining > 0)
        {
            Debug.Log("Enemy is stunned for " + stunTurnsRemaining + " more turns. Can only shield or heal.");
            chatManager.AddMessage("El enemigo sigue estuneado por " + stunTurnsRemaining + " turnos, no puede atacar");
            stunTurnsRemaining--;
            randomNumberAction = Random.Range(2, 4); // Only allow shield or heal actions when stunned

            if (stunTurnsRemaining == 0)
            {
                sceneManager.stunIcon.SetActive(false);
            }
        }

        switch (randomNumberAction)
        {
            case 1:
                Debug.Log("Attack");
                attackAction();
                soundManager.onHurtSoundReproduce();
                sceneManager.UpdatePlayerSprite();
                break;
            case 2:
                Debug.Log("Shield");
                shieldAction();
                soundManager.onShieldSoundReproduce();
                break;
            case 3:
                Debug.Log("Heal");
                int heal = healAction();
                chatManager.AddMessage("El enemigo se cura " + heal + " puntos de vida");
                soundManager.onHealSoundReproduce();
                break;
        }
    }

    public void difficultySelection()
    {
        difficulty = GameManager.Instance.GetDifficultyLevel();
    }

    public void attackAction()
    {
        int randomIndex = UnityEngine.Random.Range(0, arrayNums.Length);
        damageHit = arrayNums[randomIndex] * difficulty;
        StartCoroutine(sceneManager.ChangeSpriteWhenDamaged());
        if (sceneManager.actualPlayerShieldPoints > 0)
        {
            if (damageHit > sceneManager.actualPlayerShieldPoints)
            {
                damageHit -= sceneManager.actualPlayerShieldPoints;
                sceneManager.actualPlayerShieldPoints = 0;
                sceneManager.actualPlayerLifePoints -= damageHit;
                Debug.Log("Shield broken, remaining damage applied to life: " + damageHit);
                chatManager.AddMessage("El enemigo ha roto el escudo al jugador, le hace " + damageHit + " puntos de daño a la vida");
            }
            else
            {
                sceneManager.actualPlayerShieldPoints -= damageHit;
                Debug.Log("Damage applied to shield: " + damageHit);
                chatManager.AddMessage("El enemigo te hace " + damageHit + " puntos de daño al escudo");
            }
        }
        else
        {
            sceneManager.actualPlayerLifePoints -= damageHit;
            Debug.Log("Damage applied to life: " + damageHit);
            chatManager.AddMessage("El enemigo te hace " + damageHit + " puntos de daño a la vida");
        }

        if (sceneManager.actualPlayerLifePoints <= 0)
        {
            sceneManager.actualPlayerLifePoints = 0;
            Debug.Log("El jugador ha sido derrotado");
            lose.SetActive(true);
            soundManager.onDefeatSoundReproduce();
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Personajes");
    }

    public int healAction()
    {
        int randomIndex = UnityEngine.Random.Range(0, arrayNums.Length);
        int healNumber = arrayNums[randomIndex] * difficulty;
        actualLife += healNumber;

        if (actualLife >= iniLife)
        {
            actualLife = iniLife;
        }
        return healNumber;
    }

    public void shieldAction()
    {
        int randomIndex = Random.Range(0, arrayNumsShield.Length);
        int shieldNumber = arrayNumsShield[randomIndex] * difficulty;
        actualShield += shieldNumber;
        chatManager.AddMessage("El enemigo se pone " + shieldNumber + " puntos de escudo");
    }

    private int createIniLife()
    {
        int iniLife = 30;
        return iniLife;
    }

    private int createIniShield()
    {
        int iniShield = 10;
        return iniShield;
    }

    public void ApplySleep(int turns)
    {
        sleepTurnsRemaining += turns;
        sceneManager.sleepIcon.SetActive(true);
        Debug.Log("Enemy put to sleep for " + turns + " turns.");
    }

    public void ApplyStun(int turns)
    {
        stunTurnsRemaining += turns;
        sceneManager.stunIcon.SetActive(true);
        Debug.Log("Enemy stunned for " + turns + " turns.");
    }
}
