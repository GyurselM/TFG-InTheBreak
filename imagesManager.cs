using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class imagesManager : MonoBehaviour
{
    int idPjChoosen;
    public Sprite[] spritesRobertoRegular;
    public Sprite[] spritesCatdalf;
    public Sprite[] spritesCroackNorris;
    public Sprite[] spritesIlMapachef;
    public Sprite[] spriteArrayToUse;
    public Image player;
    // Start is called before the first frame update
    void Start()
    {
        idPjChoosen = GameManager.Instance.GetSpriteId();
        setImageArray(idPjChoosen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setImageArray(int id)
    {
        switch (id)
        {
            case 0:
                spriteArrayToUse = spritesRobertoRegular;
                break;
            case 1:
                spriteArrayToUse = spritesCatdalf;
                break;
            case 2:
                spriteArrayToUse = spritesCroackNorris;
                break;
            case 3:
                spriteArrayToUse = spritesIlMapachef;
                break;
            default:
                Debug.LogError("Invalid sprite ID");
                break;
        }

        if (spriteArrayToUse != null && spriteArrayToUse.Length > 0)
        {
            player.sprite = spriteArrayToUse[0]; // Asigna la primera imagen del array al jugador
        }
        else
        {
            Debug.LogError("Sprite array is null or empty");
        }
    }
}
