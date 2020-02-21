using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SlingEnergyUI : MonoBehaviour
{
    public SlingMovementHandler player;
    public GameObject slingEnergyObject;
    public GameObject slingEnergyTwoStarObject;
    public GameObject slingEnergyOneStarObject;
    public GameObject slingEnergyExplosionObject;
    public float size = 0.5f;

    private List<GameObject> slingEnergyObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // remove all pre-existing sling energy objects.
        if (Application.isPlaying)
        {
            while (transform.childCount > 0)
            {
                removeSlingEnergyUIObjectWithoutExplosion();
            }
       }
    }

    // Update is called once per frame
    void Update()
    {
        // This should be called on start in final build
        UpdateSlingEnergyUI();
    }

    public void UpdateSlingEnergyUI()
    {
        if (player != null)
        {
            // Update slingEnergySprites

            // need to add sling energy objects
            while (transform.childCount < player.slingEnergy)
            {
                // First two objects are of different sling types
                if (transform.childCount < 2)
                {
                    if(transform.childCount < 1 )
                    {
                        addSlingEnergy1StarObject();
                    }
                    else
                    {
                        addSlingEnergy2StarObject();
                    }
                }
                else
                {
                    addSlingEnergyUIObject();
                }
            }

            // need to decrease sling energy objects
            while (transform.childCount > player.slingEnergy)
            {
                removeSlingEnergyUIObject();
            }

            // pulse last sling energy
            if (transform.childCount > 0)
            {
                if(transform.GetChild(transform.childCount - 1).GetComponent<Animator>() != null)
                    transform.GetChild(transform.childCount - 1).GetComponent<Animator>().SetBool("Pulse", true);
            }
        }

        // add existing sling energy objects to list and pulse them
        for (int i = 0; transform.childCount > slingEnergyObjects.Count; i++)
        {
            slingEnergyObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    void addSlingEnergyUIObject()
    {
        AddSlingEnergyUIObjectOfPrefab(slingEnergyObject);
    }

    void addSlingEnergy2StarObject()
    {
        AddSlingEnergyUIObjectOfPrefab(slingEnergyTwoStarObject);
    }

    void addSlingEnergy1StarObject()
    {
        AddSlingEnergyUIObjectOfPrefab(slingEnergyOneStarObject);
    }

    void AddSlingEnergyUIObjectOfPrefab(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(this.transform);
        // position sling object
        int number = transform.childCount - 1;
        newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(size * number * 2, 0);
        // set size of new sling object
        newObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
    }

    void removeSlingEnergyUIObject()
    {
        GameObject objectToRemove = transform.GetChild(transform.childCount - 1).gameObject;

        // Instantiate object explosion @ objectToRemove location
        if (Application.isPlaying)
        {
            GameObject newExplosion = Instantiate(slingEnergyExplosionObject);
            newExplosion.GetComponent<RectTransform>().position = objectToRemove.GetComponent<RectTransform>().position;
        }

        // Destroy sling energy object
        DestroyImmediate(objectToRemove);
    }

    void removeSlingEnergyUIObjectWithoutExplosion()
    {
        GameObject objectToRemove = transform.GetChild(transform.childCount - 1).gameObject;

        // Destroy sling energy object
        DestroyImmediate(objectToRemove);
    }
}
