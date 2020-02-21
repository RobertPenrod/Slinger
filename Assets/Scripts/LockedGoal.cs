using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class LockedGoal : MonoBehaviour
{
    public GameManager gameManager;
    public float suckTime;

    public GameObject goalGraphics;

    // Open / Closed Handling
    protected bool open;    // Whether the goal is open or not.
    protected Collectable[] collectables;

    protected Animator animator;

    // Suck Handling
    protected float suckTimeStart;
    protected float innerRadius = 0.01f; // distance from goal player must be sucked to.
    protected bool playerHasCollided;
    protected GameObject playerObject;

    protected bool isLevelFinished;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        playerHasCollided = false;
        animator = goalGraphics.GetComponent<Animator>();

        collectables = FindObjectsOfType<Collectable>();

        isLevelFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if collectables have been collected
        if(!open && allCollectablesCollected())
        // Open Goal
        {
            openGoal();
        }

        // suck player into center of goal
        // then load next scene
        if (playerHasCollided && open)
        {
            if (isPlayerInsideInnerGoal())
            {
                if (!isLevelFinished)
                {
                    isLevelFinished = true;
                    // Call end of level stuff
                    LevelFinished();
                }
            }
            else
            {
                // suck player towards goal
                suckPlayerTowardsGoal();
            }
        }
    }

    protected bool allCollectablesCollected()
    {
        foreach(Collectable c in collectables)
        {
            if (!c.collected)
                return false;
        }
        return true;
    }

    // returns true if player is within the inner radius of the goal
    protected bool isPlayerInsideInnerGoal()
    {
        if (playerObject != null)
        {
            float displacement = (transform.position - playerObject.transform.position).magnitude;
            if (displacement <= innerRadius)
            {
                return true;
            }
        }
        return false;
    }

    protected void suckPlayerTowardsGoal()
    {
        playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        float suckPercent = (Time.time - suckTimeStart) / suckTime;
        playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, this.transform.position, suckPercent);
    }

    protected virtual void LevelFinished()
    {
        StartCoroutine(gameManager.WinLevel());
    }

    // if this detects a collision with the Player object,
    // reset player's velocity to zero
    // set playerObject = to player object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colidingObject = collision.gameObject;

        if (colidingObject.tag.Equals("Player") && open)
        {
            playerObject = colidingObject;
            playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playerHasCollided = true;
            suckTimeStart = Time.time;

            // Disable Player AFIE Input
            GameManager.Instance.DisableAFIEInput();
        }
    }

    protected void openGoal()
    {
        open = true;
        animator.SetBool("Open", true);
    }
}
