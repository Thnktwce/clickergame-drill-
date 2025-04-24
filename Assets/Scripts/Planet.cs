using System;

[Serializable]
public class Planet
{
    public string name;
    public int unlockCost;
    public float resourceMultiplier;
    public bool isUnlocked;

    public Planet(string name, int unlockCost, float resourceMultiplier)
    {
        this.name = name;
        this.unlockCost = unlockCost;
        this.resourceMultiplier = resourceMultiplier;
        isUnlocked = false;
    }
}
