using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public ScoreData m_ScoreData;

    private static Score m_Instance;
    public static Score Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<Score>();
            }

            return m_Instance;
        }
    }

    public float GetTotalScore(TeamComp teamComp)
    {
        float score = 0;
        int count = 0;

        score += GetHardCCScore(teamComp.Champs, out count);
        score += GetHardEngageScore(teamComp.Champs, out count);
        score += GetDisengageScore(teamComp.Champs, out count);
        score += GetPokeScore(teamComp.Champs, out count);
        score += GetWaveClearScore(teamComp.Champs, out count);
        score += GetTankScore(teamComp.Champs, out count);
        score += GetPhysicalScore(teamComp.Champs, out count);
        score += GetMagicalScore(teamComp.Champs, out count);
        score += GetUtilityScore(teamComp.Champs, out count);
        score += GetEarlyScore(teamComp.Champs, out count);
        score += GetMidScore(teamComp.Champs, out count);
        score += GetLateScore(teamComp.Champs, out count);
        score += GetPreferredLaneScore(teamComp.Players);

        return score;
    }

    public float GetTotalScore(TeamComp teamComp, out int[] champCounts)
    {
        float score = 0;
        List<int> returnChampCounts = new List<int>();
        int count = 0;

        score += GetHardCCScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetHardEngageScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetDisengageScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetPokeScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetWaveClearScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetTankScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetPhysicalScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetMagicalScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetUtilityScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetEarlyScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetMidScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetLateScore(teamComp.Champs, out count);
        returnChampCounts.Add(count);
        score += GetPreferredLaneScore(teamComp.Players);

        champCounts = returnChampCounts.ToArray();

        return score;
    }

    public float GetHardCCScore(List<ChampionData> champs, out int count)
    {
        int hardCCCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsHardCC)
            {
                hardCCCount++;
            }
        }

        count = hardCCCount;

        return m_ScoreData.GetHardCCScore(hardCCCount);
    }

    public float GetHardEngageScore(List<ChampionData> champs, out int count)
    {
        int hardEngageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsHardEngage)
            {
                hardEngageCount++;
            }
        }

        count = hardEngageCount;

        return m_ScoreData.GetHardEngageScore(hardEngageCount);
    }

    public float GetDisengageScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsDisengage)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetDisengageScore(disengageCount);
    }

    public float GetPokeScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsPoke)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetPokeScore(disengageCount);
    }

    public float GetWaveClearScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsWaveclear)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetWaveClearScore(disengageCount);
    }

    public float GetTankScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_Tank)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetTankScore(disengageCount);
    }

    public float GetPhysicalScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_DamageType == eDamageType.PHYSICAL)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetPhysicalScore(disengageCount);
    }

    public float GetMagicalScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_DamageType == eDamageType.MAGICAL)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetMagicalScore(disengageCount);
    }

    public float GetUtilityScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_DamageType == eDamageType.UTILITY)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetUtilityScore(disengageCount);
    }

    public float GetEarlyScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsEarlyGame)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetEarlyScore(disengageCount);
    }

    public float GetMidScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsMidGame)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetMidScore(disengageCount);
    }

    public float GetLateScore(List<ChampionData> champs, out int count)
    {
        int disengageCount = 0;

        for (int i = 0; i < champs.Count; i++)
        {
            if (champs[i] != null && champs[i].m_IsLateGame)
            {
                disengageCount++;
            }
        }

        count = disengageCount;

        return m_ScoreData.GetLateScore(disengageCount);
    }

    public float GetPreferredLaneScore(List<PlayerData> players)
    {
        float score = 0;

        if (players[0] != null && players[0].m_MainRole == ePosition.TOP)
        {
            score += m_ScoreData.m_PreferredLaneScore;
        }
        else if (players[0] != null && players[0].m_OffRole == ePosition.TOP)
        {
            score += m_ScoreData.m_SideLaneScore;
        }

        if (players[1] != null && players[1].m_MainRole == ePosition.JUNGLE)
        {
            score += m_ScoreData.m_PreferredLaneScore;
        }
        else if (players[1] != null && players[1].m_OffRole == ePosition.JUNGLE)
        {
            score += m_ScoreData.m_SideLaneScore;
        }

        if (players[2] != null && players[2].m_MainRole == ePosition.MIDDLE)
        {
            score += m_ScoreData.m_PreferredLaneScore;
        }
        else if (players[2] != null && players[2].m_OffRole == ePosition.MIDDLE)
        {
            score += m_ScoreData.m_SideLaneScore;
        }

        if (players[3] != null && players[3].m_MainRole == ePosition.BOTTOM)
        {
            score += m_ScoreData.m_PreferredLaneScore;
        }
        else if (players[3] != null && players[3].m_OffRole == ePosition.BOTTOM)
        {
            score += m_ScoreData.m_SideLaneScore;
        }

        if (players[4] != null && players[4].m_MainRole == ePosition.SUPPORT)
        {
            score += m_ScoreData.m_PreferredLaneScore;
        }
        else if (players[4] != null && players[4].m_OffRole == ePosition.SUPPORT)
        {
            score += m_ScoreData.m_SideLaneScore;
        }

        return score;
    }

}
