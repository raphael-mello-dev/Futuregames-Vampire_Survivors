using UnityEngine;

public class CollectableXP : MonoBehaviour
{
    private CollectableDataSO collectableData;

    [SerializeField] private string identifier;
    private float value;

    void Awake()
    {
        collectableData = Resources.Load<CollectableDataSO>($"ScriptableObjects/Features/Collectables/XP/{identifier}");

        value = collectableData.value;
    }
}