using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimationHandler : MonoBehaviour
{
    public AudioClip StarEarnedSound;
    public AudioClip StarUnearnedSound;
    private Animator[] starAnimators;
    private AudioSource[] starAudios;

    public bool isAudioDone;

    // Start is called before the first frame update
    void Start()
    {
        starAnimators = new Animator[3];
        starAudios = new AudioSource[3];

        for (int i = 0; i < transform.childCount; i++)
        {
            starAnimators[i] = transform.GetChild(i).GetComponent<Animator>();
            starAudios[i] = transform.GetChild(i).GetComponent<AudioSource>();
        }

        isAudioDone = true;
    }

    public IEnumerator ShowStars(int numStarsEarned)
    // Uses the star animators to animate the number of stars earned on a level.
    {
        isAudioDone = false;
        for (int starIndex = 0; starIndex < 3; starIndex++)
        {
            // Start Animation.
            bool earned = starIndex < numStarsEarned;
            starAnimators[starIndex].SetBool("Earned", earned);
            starAnimators[starIndex].SetTrigger("Show");

            // Play Star Sound
            if (earned)
            {
                starAudios[starIndex].clip = StarEarnedSound;
            }
            else
            {
                starAudios[starIndex].clip = StarUnearnedSound;
            }

            PlayStarSound(starIndex);

            // Wait for animation to finish.
            while (!AnimatorDone(starIndex))
            {
                yield return null;
            }
        }

        // Wait for last stars audio to finish
        while(starAudios[2].isPlaying)
        {
            yield return null;
        }
        isAudioDone = true;
    }

    private void PlayStarSound(int index)
    {
        starAudios[index].Play();
    }

    private bool AnimatorDone(int index)
    {
        return starAnimators[index].GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            starAnimators[index].GetCurrentAnimatorStateInfo(0).IsName("Idle Blacked Out");
    }
}
