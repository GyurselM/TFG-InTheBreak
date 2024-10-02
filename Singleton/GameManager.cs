using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Lista que deseas almacenar en el singleton
    private List<int> mazoJugar = new List<int>();

    // Instancia estática del GameManager
    private static GameManager _instance;
    // Nivel de dificultad
    private int difficultyLevel = 0;
    // Mejoras
    private int lifeUpgrade = 0;
    private int manaUpgrade = 0;
    private int spriteId = 0;

    // Propiedad estática para acceder a la instancia del GameManager
    public static GameManager Instance
    {
        get
        {
            // Si no hay una instancia existente, crea una nueva
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go); // No destruye el objeto GameManager al cargar una nueva escena
            }
            return _instance;
        }
    }

    // Método para agregar un dato a la lista
    public void AddToMazoJugar(int dato)
    {
        mazoJugar.Add(dato);
    }

    // Método para obtener la lista
    public List<int> GetMazoJugar()
    {
        return mazoJugar;
    }

    public void SetMazoJugar(List<int> newDeck)
    {
       mazoJugar = newDeck;
        //mazoJugar = new List<int> { 1, 2, 3, 4, 5, 6 };
    }
    // Método para obtener el nivel de dificultad
    public int GetDifficultyLevel()
    {
        return difficultyLevel;
    }

    // Método para establecer el nivel de dificultad
    public void SetDifficultyLevel(int level)
    {
        difficultyLevel = level;
    }

    //Life upgrade
    public int GetLifeUpgrade()
    {
        return lifeUpgrade;
    }
    public void SetLifeUpgrade(int upgrade)
    {
        lifeUpgrade += upgrade;
    }

    //Mana upgrade
    public int GetManaUpgrade()
    {
        return manaUpgrade;
    }
    public void SetManaUpgrade(int upgrade)
    {
        manaUpgrade += upgrade;
    }
    public int GetSpriteId()
    {
        return spriteId;
    }
    public void SetSpriteId(int id)
    {
        spriteId = id;
    }
}