using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI planetUnlockCostText;
    public TextMeshProUGUI upgradeCostText;

    [Header("Game Values")]
    public float totalResources = 0f;
    public float clickPower = 1f;
    public float clickUpgradeCost = 10f;

    [Header("Planets")]
    public List<Planet> planets;
    public int currentPlanetIndex = 0;

    [Header("AutoMiner")]
    public float autoMinerPower = 0f;
    public float autoMinerCost = 20f;

    void Start()
    {
        // Начальная планета должна быть разблокирована
        if (planets.Count > 0)
        {
            planets[0].isUnlocked = true;
        }

        InvokeRepeating(nameof(AutoMine), 1f, 1f);
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void AddResources(float amount)
    {
        totalResources += amount * planets[currentPlanetIndex].resourceMultiplier;
        UpdateUI();
    }

    public void UpgradeClick()
    {
        if (totalResources >= clickUpgradeCost)
        {
            totalResources -= clickUpgradeCost;
            clickPower += 1f;
            clickUpgradeCost *= 1.5f;
            UpdateUI();
        }
    }

    public void BuyAutoMiner()
    {
        if (totalResources >= autoMinerCost)
        {
            totalResources -= autoMinerCost;
            autoMinerPower += 1f;
            autoMinerCost *= 1.7f;
            UpdateUI();
        }
    }

    void AutoMine()
    {
        AddResources(autoMinerPower);
    }

    public void UnlockPlanet()
    {
        if (currentPlanetIndex + 1 < planets.Count)
        {
            Planet nextPlanet = planets[currentPlanetIndex + 1];
            if (!nextPlanet.isUnlocked && totalResources >= nextPlanet.unlockCost)
            {
                totalResources -= nextPlanet.unlockCost;
                nextPlanet.isUnlocked = true;
                currentPlanetIndex++;
                UpdateUI();
                Debug.Log($"Планета {nextPlanet.name} разблокирована!");
            }
            else
            {
                Debug.Log("Недостаточно ресурсов для разблокировки планеты или она уже разблокирована.");
            }
        }
        else
        {
            Debug.Log("Все планеты уже разблокированы.");
        }
        FindObjectOfType<PlanetSelector>().GeneratePlanetButtons();
    }

    public void UpdateUI()
    {
        resourceText.text = $"Ресурсы: {totalResources:F1}";

        if (currentPlanetIndex < planets.Count)
        {
            planetNameText.text = $"Планета: {planets[currentPlanetIndex].name}";
        }

        if (currentPlanetIndex + 1 < planets.Count)
        {
            planetUnlockCostText.text = $"Разблокировать: {planets[currentPlanetIndex + 1].unlockCost:F1}";
        }
        else
        {
            planetUnlockCostText.text = "Все планеты разблокированы!";
        }

        upgradeCostText.text = $"Улучшить клик: {clickUpgradeCost:F1}";
    }
}
