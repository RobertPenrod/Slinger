using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int starCount;
    public bool locked;

    public LevelData(int starCount, bool locked)
    {
        this.starCount = starCount;
        this.locked = locked;
    }
}
