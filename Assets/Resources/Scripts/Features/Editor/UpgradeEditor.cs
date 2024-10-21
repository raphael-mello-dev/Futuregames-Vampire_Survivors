using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Windows;
using UnityEditor.PackageManager.Requests;
using System;

public class UpgradeEditor : EditorWindow
{
    private const string upgradeSOPath = "Assets/Resources/ScriptableObjects/Features/Upgrades/";
    private const string prefabPath = "Assets/Resources/Prefabs/Weapons/";
    private const string orbitPrefabPath = "Prefabs/Weapons/NormalSword";
    private const string pjtlPrefabPath = "Prefabs/Weapons/NormalMace";

    private string upgradeName;
    private bool nameChecked = false;
    private string message;

    private GUIStyle style;

    private UpgradeSO.UpgradeType type;
    private UpgradeSO.AttachedType aType;
    private UpgradeSO.StatType sType;
    private string cardName;
    private string cardDescription;
    private Sprite sprite;
    private string value;
    private string prefabName;

    [MenuItem("Tools/Upgrade Creator")]
    public static void ShowWindow()
    {
        GetWindow<UpgradeEditor>("Upgrade Creator");
    }

    private void OnGUI()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

        GUILayout.Space(10);
        GUILayout.Label("Create a new upgrade", style);
        GUILayout.Space(10);
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Upgrade Name:");
        upgradeName = EditorGUILayout.TextField("", upgradeName);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create new upgrade"))
            message = CheckUpgradeName();

        GUILayout.Label(message, EditorStyles.boldLabel);

        if (nameChecked && message == "")
            CreateUpgrade();
    }

    string CheckUpgradeName()
    {
        if (string.IsNullOrEmpty(upgradeName))
            return "Please assign a name to the upgrade";
        else if (File.Exists($"{upgradeSOPath}{upgradeName}.asset"))
            return "Upgrade name already exists, please change it";
        else
        {
            nameChecked = true;
            return "";
        }
    }

    void CreateUpgrade()
    {
        GuiLine();
        
        GUILayout.Space(10);
        GUILayout.Label("Upgrade Card", style);
        GUILayout.Space(10);

        type = (UpgradeSO.UpgradeType) EditorGUILayout.EnumPopup("Select upgrade type: ", type);

        if (type == UpgradeSO.UpgradeType.AttatchToPlayer)
            aType = (UpgradeSO.AttachedType)EditorGUILayout.EnumPopup("Select attached type: ", aType);
        else
            aType = UpgradeSO.AttachedType.NotAttachable;

        if (type == UpgradeSO.UpgradeType.AddStats)
            sType = (UpgradeSO.StatType)EditorGUILayout.EnumPopup("Select stat type: ", sType);
        else
            sType = UpgradeSO.StatType.Damage;
        
        GUILayout.Space(10);
        sprite = (Sprite) EditorGUILayout.ObjectField("Select Icon:", sprite, typeof(Sprite), false);
        GUILayout.Space(10);
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card Name:");
        cardName = EditorGUILayout.TextField("", cardName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card Description:");
        cardDescription = EditorGUILayout.TextField("", cardDescription);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Atribute value:");
        value = EditorGUILayout.TextField("", value);
        EditorGUILayout.EndHorizontal();

        if (type == UpgradeSO.UpgradeType.AttatchToPlayer)
        {
            GUILayout.Space(10);
            GUILayout.Label("Upgrade Prefab", style);
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab Name:");
            prefabName = EditorGUILayout.TextField("", prefabName);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Create Upgrade"))
        {
            if ((type == UpgradeSO.UpgradeType.AttatchToPlayer && aType == UpgradeSO.AttachedType.NotAttachable) || sprite == null ||
                string.IsNullOrEmpty(cardName) || string.IsNullOrEmpty(cardDescription) || !IsNumber(value) ||
                (type == UpgradeSO.UpgradeType.AttatchToPlayer && string.IsNullOrEmpty(prefabName)))
            {
                return;
            }
            else
                CreateAsset();
        }
    }

    void CreateAsset()
    {
        try
        {
            UpgradeSO newUpgrade = CreateInstance<UpgradeSO>();

            newUpgrade.type = type;
            newUpgrade.attachedType = aType;
            newUpgrade.statType = sType;
            newUpgrade.value = float.Parse(value);
            newUpgrade.sprite = sprite;
            newUpgrade.nameUpgd = cardName;
            newUpgrade.description = cardDescription;

            if (type == UpgradeSO.UpgradeType.AddStats)
                newUpgrade.prefabName = null;
            else
            {
                prefabName = prefabName.Replace(" ", "");

                newUpgrade.prefabName = prefabName;

                if (aType == UpgradeSO.AttachedType.OrbitsAround)
                {
                    GameObject originalPrefab = Resources.Load<GameObject>(orbitPrefabPath);
                    GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(originalPrefab);
                    instance.name = prefabName;
                    GameObject child = FindObjectOfType<Weapon>().gameObject;
                    child.name = $"{prefabName}Obj";
                    child.GetComponent<SpriteRenderer>().sprite = sprite;
                    PrefabUtility.SaveAsPrefabAsset(instance, $"{prefabPath}{prefabName}.prefab");
                    Destroy(instance);
                }
                //else
                //{
                //    GameObject originalPrefab = Resources.Load<GameObject>(pjtlPrefabPath);
                //    GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(originalPrefab);
                //    instance.name = prefabName;
                //    instance.GetComponent<SpriteRenderer>().sprite = sprite;
                //    PrefabUtility.SaveAsPrefabAsset(instance, $"{prefabPath}{prefabName}.asset");
                //    Destroy(instance);
                //}
            }

            AssetDatabase.CreateAsset(newUpgrade, $"{upgradeSOPath}{upgradeName}.asset");
            AssetDatabase.Refresh();
        }
        catch (NullReferenceException e)
        {
            Debug.LogError($"Null reference encountered: {e.Message}");
        }
        catch (UnityException e)
        {
            Debug.LogError($"Unity exception: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"An unexpected error occurred: {e.Message}");
        }
    }

    void GuiLine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }

    bool IsNumber(string reference)
    {
        return float.TryParse(reference, out float number);
    }
}