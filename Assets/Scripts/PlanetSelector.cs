using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlanetSelector : MonoBehaviour
{
    public GameController gameController;
    public GameObject planetButtonPrefab;
    public Transform buttonContainer;

    void Start()
    {
        GeneratePlanetButtons();
    }

    public void GeneratePlanetButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < gameController.planets.Count; i++)
        {
            if (gameController.planets[i].isUnlocked)
            {
                int index = i; // Копия индекса для лямбды
                GameObject btnObj = Instantiate(planetButtonPrefab, buttonContainer);
                btnObj.GetComponentInChildren<TextMeshProUGUI>().text = gameController.planets[i].name;
                btnObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameController.currentPlanetIndex = index;
                    gameController.UpdateUI();
                });
            }
        }
    }
}
