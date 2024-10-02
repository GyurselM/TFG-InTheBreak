using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollbarScript : MonoBehaviour
{
    public Scrollbar sbar;
    public int maxCards;
    int totalRows;
    // Start is called before the first frame update
    void Start()
    {
        float result = maxCards / 3.0f;
        totalRows = Mathf.CeilToInt(result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
