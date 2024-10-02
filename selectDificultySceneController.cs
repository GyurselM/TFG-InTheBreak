using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // Include SceneManager namespace

public class selectDificultySceneController : MonoBehaviour
{
    public Image monday;
    public Image tuesday;
    public Image wednesday;
    public Image thursday;
    public Image friday;

    public Button mondayButton;
    public Button tuesdayButton;
    public Button wednesdayButton;
    public Button thursdayButton;
    public Button fridayButton;

    private Button selectedButton;
    private int difficultyLevel = 5; // 5 for Monday, 4 for Tuesday, ..., 1 for Friday

    // Start is called before the first frame update
    void Start()
    {
        SetupImagesUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupImagesUI()
    {
        monday.gameObject.SetActive(false);
        tuesday.gameObject.SetActive(false);
        wednesday.gameObject.SetActive(false);
        thursday.gameObject.SetActive(false);
        friday.gameObject.SetActive(false);

        // Assign button events for each button
        AssignButtonEvents(mondayButton, monday);
        AssignButtonEvents(tuesdayButton, tuesday);
        AssignButtonEvents(wednesdayButton, wednesday);
        AssignButtonEvents(thursdayButton, thursday);
        AssignButtonEvents(fridayButton, friday);
    }

    void AssignButtonEvents(Button button, Image image)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { ShowImage(image); });
        trigger.triggers.Add(entryEnter);

        // OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        if (selectedButton != null && selectedButton.GetComponent<Image>() != image)
        {
            entryExit.callback.AddListener((data) => { HideImage(image); });
        }
        trigger.triggers.Add(entryExit);

        // OnClick
        //button.onClick.AddListener(() => ToggleButton(button, image));
    }

    void ShowImage(Image image)
    {
        if (selectedButton == null || selectedButton.GetComponent<Image>() != image)
        {
            image.gameObject.SetActive(true);
            HideAllOtherImages(image);
        }
    }

    void HideImage(Image image)
    {
        if (selectedButton != null && selectedButton.GetComponent<Image>() != image)
        {
            image.gameObject.SetActive(false);
        }
    }

    void HideAllOtherImages(Image exceptImage)
    {
        if (monday != exceptImage)
            monday.gameObject.SetActive(false);
        if (tuesday != exceptImage)
            tuesday.gameObject.SetActive(false);
        if (wednesday != exceptImage)
            wednesday.gameObject.SetActive(false);
        if (thursday != exceptImage)
            thursday.gameObject.SetActive(false);
        if (friday != exceptImage)
            friday.gameObject.SetActive(false);
    }

    public void pushButtonFriday()
    {
        difficultyLevel = 1; // Establecer dificultad para viernes
        GameManager.Instance.SetDifficultyLevel(difficultyLevel);
        SceneManager.LoadScene("siguiente");
    }

    public void pushButtonThursday()
    {
        difficultyLevel = 2; // Establecer dificultad para jueves
        GameManager.Instance.SetDifficultyLevel(difficultyLevel);
        SceneManager.LoadScene("siguiente");
    }

    public void pushButtonWednesday()
    {
        difficultyLevel = 3; // Establecer dificultad para miércoles
        GameManager.Instance.SetDifficultyLevel(difficultyLevel);
        SceneManager.LoadScene("siguiente");
    }

    public void pushButtonTuesday()
    {
        difficultyLevel = 4; // Establecer dificultad para martes
        GameManager.Instance.SetDifficultyLevel(difficultyLevel);
        SceneManager.LoadScene("siguiente");
    }

    public void pushButtonMonday()
    {
        difficultyLevel = 5; // Establecer dificultad para lunes
        GameManager.Instance.SetDifficultyLevel(difficultyLevel);
        SceneManager.LoadScene("siguiente");
    }

}