using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public bool hasEnergy = true; // energy is lost after the bounce pad is used.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasEnergy)
        {
            // If the collision was with the player
            if (collision.gameObject.tag.Equals("Player"))
            {
                Deactivate();
            }
        }
    }

    private void Deactivate()
    {
        hasEnergy = false;

        // Remove bouncy physics material
        GetComponent<Collider2D>().sharedMaterial = null;

        // Start Animation
        GetComponent<Animator>().SetTrigger("Deactivate");


        // Play Audio
        GetComponent<AudioSource>().Play();
    }
}
