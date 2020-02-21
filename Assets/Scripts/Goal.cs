using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class Goal : MonoBehaviour
{
    public float suckTime;
    protected float suckTimeStart;
    protected float innerRadius = 0.01f; // distance from goal player must be sucked to.
    protected bool playerHasCollided;
    protected GameObject playerObject;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerHasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        // suck player into center of goal
        // then load next scene
        if (playerHasCollided)
        {
            if(isPlayerInsideInnerGoal())
            {
                // load next scene
                LevelFinished();
            }
            else
            {
                // suck player towards goal
                suckPlayerTowardsGoal();
            }
        }
    }

    // returns true if player is within the inner radius of the goal
    protected bool isPlayerInsideInnerGoal()
    {
        if(playerObject != null)
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
        playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        float suckPercent = (Time.time - suckTimeStart) / suckTime;
        playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, this.transform.position, suckPercent);
    }

    protected virtual void LevelFinished()
    {
        gameManager.WinLevel();
    }

    // if this detects a collision with the Player object,
    // reset player's velocity to zero
    // set playerObject = to player object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colidingObject = collision.gameObject;

        if(colidingObject.tag.Equals("Player"))
        {
            playerObject = colidingObject;
            playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playerHasCollided = true;
            suckTimeStart = Time.time;
        }
    }
}
