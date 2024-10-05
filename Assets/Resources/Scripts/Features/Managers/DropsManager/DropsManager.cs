using System.Collections.Generic;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> smallXPList;
    [SerializeField] private List<GameObject> mediumXPList;

    [SerializeField] private int smallXPCount;
    [SerializeField] private int mediumXPCount;


    private void Start()
    {
        for (int i = 0; i < smallXPCount; i++)
        {
            GameObject smallXP = Instantiate(Resources.Load<GameObject>("Prefabs/SmallXP"), Vector3.zero, Quaternion.identity, gameObject.transform);
            smallXP.SetActive(false);
            smallXPList.Add(smallXP);
        }

        for (int i = 0; i < mediumXPCount; i++)
        {
            GameObject mediumXP = Instantiate(Resources.Load<GameObject>("Prefabs/MediumXP"), Vector3.zero, Quaternion.identity, gameObject.transform);
            mediumXP.SetActive(false);
            mediumXPList.Add(mediumXP);
        }
    }

    public void SpawnSmallXP(Vector3 enemyDrop)
    {
        foreach (GameObject smallXP in smallXPList)
        {
            if (!smallXP.activeInHierarchy)
            {
                smallXP.transform.position = enemyDrop;
                smallXP.SetActive(true);
                break;
            }
        }
    }

    public void SpawnMediumXP(Vector3 enemyDrop)
    {
        foreach (GameObject mediumXP in mediumXPList)
        {
            if (!mediumXP.activeInHierarchy)
            {
                mediumXP.transform.position = enemyDrop;
                mediumXP.SetActive(true);
                break;
            }
        }
    }
}