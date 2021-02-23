using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct ScoreStruct
{
    public float NoChampScore;
    public float OneChampScore;
    public float TwoChampScore;
    public float ThreeChampScore;
    public float FourChampScore;
    public float FiveChampScore;
}

public class ScoreData : ScriptableObject
{
    public ScoreStruct m_HardCCScores;
    public ScoreStruct m_HardEngageScores;
    public ScoreStruct m_DisengageScores;
    public ScoreStruct m_PokeScores;
    public ScoreStruct m_WaveClearScores;
    public ScoreStruct m_TankScores;
    public ScoreStruct m_PhysicalScores;
    public ScoreStruct m_MagicalScores;
    public ScoreStruct m_UtilityScores;
    public ScoreStruct m_EarlyGameScores;
    public ScoreStruct m_MidGameScores;
    public ScoreStruct m_LateGameScores;

    public float m_PreferredLaneScore;
    public float m_SideLaneScore;

#if UNITY_EDITOR
    [MenuItem("TeamComp/Create/ScoreData")]
    public static void CreateMyAsset()
    {
        ScoreData asset = ScriptableObject.CreateInstance<ScoreData>();

        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScoreData.asset");
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif

    private float GetScore(ScoreStruct scoreStruct, int champs)
    {
        switch (champs)
        {
            case 0:
                return scoreStruct.NoChampScore;
            case 1:
                return scoreStruct.OneChampScore;
            case 2:
                return scoreStruct.TwoChampScore;
            case 3:
                return scoreStruct.ThreeChampScore;
            case 4:
                return scoreStruct.FourChampScore;
            case 5:
                return scoreStruct.FiveChampScore;
        }

        return 0;
    }

    public float GetHardCCScore(int champs)
    {
        return GetScore(m_HardCCScores, champs);
    }

    public float GetHardEngageScore(int champs)
    {
        return GetScore(m_HardEngageScores, champs);
    }

    public float GetDisengageScore(int champs)
    {
        return GetScore(m_DisengageScores, champs);
    }

    public float GetPokeScore(int champs)
    {
        return GetScore(m_PokeScores, champs);
    }

    public float GetWaveClearScore(int champs)
    {
        return GetScore(m_WaveClearScores, champs);
    }

    public float GetTankScore(int champs)
    {
        return GetScore(m_TankScores, champs);
    }

    public float GetPhysicalScore(int champs)
    {
        return GetScore(m_PhysicalScores, champs);
    }

    public float GetMagicalScore(int champs)
    {
        return GetScore(m_MagicalScores, champs);
    }

    public float GetUtilityScore(int champs)
    {
        return GetScore(m_UtilityScores, champs);
    }

    public float GetEarlyScore(int champs)
    {
        return GetScore(m_EarlyGameScores, champs);
    }

    public float GetMidScore(int champs)
    {
        return GetScore(m_MidGameScores, champs);
    }

    public float GetLateScore(int champs)
    {
        return GetScore(m_LateGameScores, champs);
    }
}
