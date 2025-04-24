using UnityEngine;

public class ClickController : MonoBehaviour
{
    public GameController gameController;

    public void OnClick()
    {
        if (gameController != null)
        {
            Debug.Log(" нопка нажата");
            gameController.AddResources(gameController.clickPower);
        }
        else
        {
            Debug.LogWarning("GameController не прив€зан!");
        }
    }
}
