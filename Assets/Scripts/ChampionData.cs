using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum eDamageType
{
    PHYSICAL,
    MAGICAL,
    UTILITY
}

public class ChampionData : ScriptableObject
{
    public string m_ChampionName;
    public bool m_IsHardCC;
    public bool m_IsHardEngage;
    public bool m_IsDisengage;
    public bool m_IsPoke;
    public bool m_IsWaveclear;
    public bool m_Tank;
    public eDamageType m_DamageType;
    public bool m_IsEarlyGame;
    public bool m_IsMidGame;
    public bool m_IsLateGame;

    public List<string> m_CounteredBy;

#if UNITY_EDITOR
    [MenuItem("TeamComp/Create/ChampionData")]
    public static void CreateMyAsset()
    {
        ChampionData asset = ScriptableObject.CreateInstance<ChampionData>();

        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ChampionData/NewScripableObject.asset");
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif

    public override bool Equals(object other)
    {
        return m_ChampionName.Equals(((ChampionData)other).m_ChampionName);
    }

    public override int GetHashCode()
    {
        return m_ChampionName.GetHashCode();
    }
}
