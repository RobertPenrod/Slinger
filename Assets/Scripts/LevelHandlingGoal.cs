using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandlingGoal : LockedGoal
{
    public bool needCollectables;
    public string nextScene;

    private void Start()
    {
        open = false;
        playerHasCollided = false;
        animator = goalGraphics.GetComponent<Animator>();
        collectables = FindObjectsOfType<Collectable>();

        if (!needCollectables)
        {
            openGoal();
        }
    }

    override protected void LevelFinished()
    {
        //gameManager.WinLevel(); <- Code from Goal

        SceneFader.Instance.FadeTo(nextScene);
    }
}
