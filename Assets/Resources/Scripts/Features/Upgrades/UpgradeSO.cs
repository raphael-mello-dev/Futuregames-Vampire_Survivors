using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade", fileName = "New Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public enum UpgradeType
    {
        AttatchToPlayer,
        AddStats
    }

    public enum AttachedType
    {
        NotAttachable,
        OrbitsAround,
        Projectiles
    }

    public enum StatType
    {
        Damage,
        Speed,
        Health
    }

    public string nameUpgd;
    public string description;
    public Sprite sprite;
    public UpgradeType type;
    public AttachedType attachedType;
    public StatType statType;
    public float value;

    [SerializeField] private string prefabName;
    public string PrefabPath
    {
        get { return $"Prefabs/Weapons/{prefabName}"; }
    }
}