using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CompDataController : MonoBehaviour
{
    public List<PlayerData> m_PlayerData;
    public List<ChampionData> m_ChampionData;

    public List<PositionController> m_PositionControllers;

    public List<Dropdown> m_PlayerDropdowns;
    public InputField m_BannedChampsField;
    public List<ChampionData> m_BannedChampions = new List<ChampionData>();

    public CompMaker m_CompMaker;

    private static CompDataController m_Instance;
    public static CompDataController Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<CompDataController>();
            }

            return m_Instance;
        }
    }

    public void ChampionChanged(bool shouldUpdate = false)
    {
        if (!shouldUpdate)
        {
            return;
        }

        TeamComp teamComp = default;
        teamComp.Players = new List<PlayerData>();
        teamComp.Champs = new List<ChampionData>();

        for (int i = 0; i < m_PositionControllers.Count; i++)
        {
            teamComp.Players.Add(m_PositionControllers[i].m_CurrentPlayer);
            teamComp.Champs.Add(m_PositionControllers[i].m_CurrentChampion);
        }

        Score.Instance.GetTotalScore(teamComp);
        m_CompMaker.FillScoreText(Score.Instance.GetTotalScore(teamComp));
        m_CompMaker.FillCompText(teamComp);
    }

    public void Initialize()
    {
        
        List<string> basePlayers = new List<string>();

        for (int i = 0; i < m_PlayerData.Count; i++)
        {
            basePlayers.Add(m_PlayerData[i].m_PlayerName);
        }

        basePlayers = basePlayers.OrderBy(x => x).ToList<string>();

        for (int i = 0; i < m_PlayerDropdowns.Count; i++)
        {
            m_PlayerDropdowns[i].ClearOptions();
            m_PlayerDropdowns[i].AddOptions(basePlayers);
            m_PlayerDropdowns[i].value = i;
        }

        List<PlayerData> activePlayers = GetActivePlayers();
        List<string> activePlayerStrings = new List<string>();
        for (int i = 0; i < activePlayers.Count; i++)
        {
            activePlayerStrings.Add(activePlayers[i].m_PlayerName);
        }

        activePlayerStrings = activePlayerStrings.OrderBy(x => x).ToList<string>();

        for (int i = 0; i < m_PositionControllers.Count; i++)
        {
            m_PositionControllers[i].Initialize();
            m_PositionControllers[i].PopulatePlayerDropdown(activePlayerStrings);
        }

        m_BannedChampsField.onEndEdit.AddListener(delegate
        {
            BannedChampsEdited();
        });
    }

    public void ResetActivePlayers()
    {
        List<PlayerData> activePlayers = GetActivePlayers();
        List<string> activePlayerStrings = new List<string>();
        for (int i = 0; i < activePlayers.Count; i++)
        {
            activePlayerStrings.Add(activePlayers[i].m_PlayerName);
        }

        activePlayerStrings = activePlayerStrings.OrderBy(x => x).ToList<string>();

        for (int i = 0; i < m_PositionControllers.Count; i++)
        {
            m_PositionControllers[i].PopulatePlayerDropdown(activePlayerStrings);
        }
    }

    public List<PlayerData> GetActivePlayers()
    {
        List<PlayerData> activePlayers = new List<PlayerData>();

        for (int i = 0; i < m_PlayerDropdowns.Count; i++)
        {
            activePlayers.Add(GetPlayerData(m_PlayerDropdowns[i].options[m_PlayerDropdowns[i].value].text));
        }

        return activePlayers;
    }

    private void BannedChampsEdited()
    {
        m_BannedChampions.Clear();
        string champString = m_BannedChampsField.text;
        champString.Replace(" ", "");
        champString.Replace(".", ",");

        string[] splitStrings = m_BannedChampsField.text.Split(',');

        for (int i = 0; i < splitStrings.Length; i++)
        {
            ChampionData championData = GetChampionData(splitStrings[i]);
            if (championData != null)
            {
                m_BannedChampions.Add(championData);
            }
        }
    }

    public PlayerData GetPlayerData(string player)
    {
        for (int i = 0; i < m_PlayerData.Count; i++)
        {
            if (m_PlayerData[i].m_PlayerName.ToLower().Equals(player.ToLower()))
            {
                return m_PlayerData[i];
            }
        }

        return null;
    }

    public List<PlayerData> GetPlayerDataList(List<string> players)
    {
        List<PlayerData> returnList = new List<PlayerData>();

        for (int i = 0; i < players.Count; i++)
        {
            returnList.Add(GetPlayerData(players[i]));
        }

        return returnList;
    }

    public ChampionData GetChampionData(string champion)
    {
        for (int i = 0; i < m_ChampionData.Count; i++)
        {
            if (m_ChampionData[i].m_ChampionName.ToLower().Equals(champion.ToLower()))
            {
                return m_ChampionData[i];
            }
        }

        return null;
    }

    public List<ChampionData> GetChampionDataList(List<string> champions)
    {
        List<ChampionData> returnList = new List<ChampionData>();

        for (int i = 0; i < champions.Count; i++)
        {
            returnList.Add(GetChampionData(champions[i]));
        }

        return returnList;
    }

    public void Start()
    {
        Initialize();
    }

}
