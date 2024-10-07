using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bloodParticlesList;
    [SerializeField] private int bloodCount;

    void Start()
    {
        for (int i = 0; i < bloodCount; i++)
        {
            GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/Blood"), Vector3.zero, Quaternion.identity, gameObject.transform);
            blood.SetActive(false);
            bloodParticlesList.Add(blood);
        }
    }

    public void ActivateBloodPtl(Vector3 pos)
    {
        foreach (GameObject blood in bloodParticlesList)
        {
            if (!blood.activeInHierarchy)
            {
                blood.transform.position = pos;
                blood.SetActive(true);
                blood.GetComponent<ParticleSystem>().Play();
                break;
            } 
        }
    }

    public void DeactivateBloodPtl(GameObject reference)
    {
        if (reference != null)
            reference.SetActive(false);
    }
}