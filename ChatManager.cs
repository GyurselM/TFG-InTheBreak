using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text chatText;
    public int maxMessages = 50;

    private void Start()
    {
        // Ajusta el desplazamiento del scroll hacia abajo al inicio
        scrollRect.verticalNormalizedPosition = 0;
    }

    // M�todo para agregar un nuevo mensaje al chat
    public void AddMessage(string message)
    {
        // Agrega el nuevo mensaje al Text
        chatText.text += message + "\n";

        // Si la cantidad de l�neas supera el l�mite, elimina el mensaje m�s antiguo
        string[] lines = chatText.text.Split('\n');
        if (lines.Length > maxMessages)
        {
            int numLinesToRemove = lines.Length - maxMessages;
            string newText = string.Join("\n", lines, numLinesToRemove, maxMessages);
            chatText.text = newText;
        }

        // Ajusta el desplazamiento del scroll hacia abajo para mostrar el nuevo mensaje
        Canvas.ForceUpdateCanvases(); // Aseg�rate de que el canvas se actualice antes de ajustar el desplazamiento
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // Aseg�rate de que el canvas se actualice antes de ajustar el desplazamiento

        // Establece el desplazamiento vertical al m�nimo (abajo)
        scrollRect.verticalNormalizedPosition = 0f;
    }
    // M�todo para eliminar el mensaje m�s antiguo del chat
}
