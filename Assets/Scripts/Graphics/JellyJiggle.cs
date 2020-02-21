using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyJiggle : MonoBehaviour
{
    public GameObject jiggleObject; //  transform to scale and perform jiggle on.

    // Oscillation Variables:
    // e^(-lt) * a * cos(wt - p)
    [SerializeField]
    private float timer;
    public float exponential;  // exponent of e
    public float frequency;    // angular frequency of sinusoid
    public float phase;        // probably not going to use this
    public float sizeMax = 1f;
    public float sizeMin = 0.25f;
    public float maxVelocity = 12;
    public float maxExp = 2;    // 5
    public float minExp = 0.5f;

    private float amplitude = 1;    // mplitude of sinusoid
    private Vector2 direction;  // direction to jiggle relative to

    private float jiggleValue;

    // Start is called before the first frame update
    void Start()
    {
        initJiggle();
        timer = 999; // to disable jiggling on the start of the level.
    }

    void initJiggle()
    {
        timer = 0;
        //exponential = 1;
        //frequency = 1;
        //phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Clamp rotation to jiggleDirection
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        jiggleObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Step Oscillation
        timer += Time.deltaTime;
        jiggleValue = Mathf.Exp(-exponential * timer) * amplitude * Mathf.Cos(frequency * timer - phase);
        float q = (jiggleValue + 1) / 2f; // map value between 0 and 1.
        float p = 2 *( q > 0.5f ? 1 - q : q); // maps value between 0, 1, 0.
        float invP = Mathf.Abs(1 - p);  // maps value between 1, 0, 1.
        //float scaleX = sizeMin + (sizeMax - sizeMin) * q;
        //float scaleY = sizeMin + (sizeMax - sizeMin) * (1 - q);
        float scaleY = p + invP * (sizeMin + (sizeMax - sizeMin) * q);
        float scaleX = p + invP * (sizeMin + (sizeMax - sizeMin) * (1-q));
        jiggleObject.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = collision.GetContact(0).normal;
        float collisionVelocity = collision.relativeVelocity.magnitude;
        float velocityPercent = Mathf.Clamp(collisionVelocity / maxVelocity, 0, 1);
        exponential = minExp + (maxExp - minExp) * (1 - velocityPercent);
        amplitude = Mathf.Sqrt(velocityPercent);
        initJiggle();
    }
}
