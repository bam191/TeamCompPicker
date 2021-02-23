using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ePosition
{
    TOP,
    JUNGLE,
    MIDDLE,
    BOTTOM,
    SUPPORT
}

public class PlayerData : ScriptableObject
{
    public string m_PlayerName;
    public ePosition m_MainRole;
    public ePosition m_OffRole;
    public List<ChampionData> m_TopChampions;
    public List<ChampionData> m_JungleChampions;
    public List<ChampionData> m_MiddleChampions;
    public List<ChampionData> m_BottomChampions;
    public List<ChampionData> m_SupportChampions;

#if UNITY_EDITOR
    [MenuItem("TeamComp/Create/PlayerData")]
    public static void CreateMyAsset()
    {
        PlayerData asset = ScriptableObject.CreateInstance<PlayerData>();

        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/PlayerData/NewScripableObject.asset");
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif

    public override bool Equals(object other)
    {
        return m_PlayerName.Equals(((PlayerData)other).m_PlayerName);
    }

    public override int GetHashCode()
    {
        return m_PlayerName.GetHashCode();
    }
}
