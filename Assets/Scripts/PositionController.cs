using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour
{
    public PlayerData m_CurrentPlayer;
    public ChampionData m_CurrentChampion;

    public Dropdown m_PlayerDropdown;
    public Dropdown m_ChampionDropdown;
    public Toggle m_PlayerLockToggle;
    public Toggle m_ChampionLockToggle;
    public GameObject m_ChampionPanel;

    public ePosition m_Position;

    public Text m_CompText;

    public bool m_IsPlayerLocked;
    public bool m_IsChampionLocked;

    public void Initialize()
    {
        m_PlayerDropdown.onValueChanged.AddListener(delegate
        {
            PlayerChanged();
        });
        m_PlayerLockToggle.onValueChanged.AddListener(delegate
        {
            LockPlayer();
        });
        m_ChampionDropdown.onValueChanged.AddListener(delegate
        {
            ChampionChanged();
        });
        m_ChampionLockToggle.onValueChanged.AddListener(delegate
        {
            LockChampion();
        });
    }

    public void PopulatePlayerDropdown(List<string> players)
    {
        m_PlayerDropdown.ClearOptions();
        m_PlayerDropdown.AddOptions(new List<string>() { "None" });
        m_PlayerDropdown.value = 0;
        m_PlayerDropdown.AddOptions(players);
    }

    public void PopulateChampionDropdown()
    {
        List<ChampionData> championDataList = null;

        switch (m_Position)
        {
            case ePosition.TOP:
                championDataList = m_CurrentPlayer.m_TopChampions;
                break;
            case ePosition.JUNGLE:
                championDataList = m_CurrentPlayer.m_JungleChampions;
                break;
            case ePosition.MIDDLE:
                championDataList = m_CurrentPlayer.m_MiddleChampions;
                break;
            case ePosition.BOTTOM:
                championDataList = m_CurrentPlayer.m_BottomChampions;
                break;
            case ePosition.SUPPORT:
                championDataList = m_CurrentPlayer.m_SupportChampions;
                break;
        }

        List<string> championNames = new List<string>();

        for (int i = 0; i < championDataList.Count; i++)
        {
            championNames.Add(championDataList[i].m_ChampionName);
        }

        championNames = championNames.OrderBy(x => x).ToList<string>();

        m_ChampionDropdown.ClearOptions();
        m_ChampionDropdown.AddOptions(new List<string>() { "None" });
        m_ChampionDropdown.value = 0;
        m_ChampionDropdown.AddOptions(championNames);
    }

    public void PlayerChanged()
    {
        AssignPlayer(m_PlayerDropdown.options[m_PlayerDropdown.value].text);
    }

    public void ChampionChanged()
    {
        AssignChampion(m_ChampionDropdown.options[m_ChampionDropdown.value].text, true);
    }

    public void AssignPlayer(string player)
    {
        m_CurrentPlayer = CompDataController.Instance.GetPlayerData(player);

        if (m_CurrentPlayer == null)
        {
            AssignChampion("", true);
            m_IsPlayerLocked = false;
            m_IsChampionLocked = false;
            m_PlayerDropdown.value = 0;
            m_ChampionDropdown.value = 0;
            m_PlayerLockToggle.isOn = false;
            m_ChampionLockToggle.isOn = false;
            m_ChampionPanel.SetActive(false);
            m_CompText.text = "";
        }
        else
        {
            if (!m_PlayerDropdown.options[m_PlayerDropdown.value].text.Equals(player))
            {
                for (int i = 0; i < m_PlayerDropdown.options.Count; i++)
                {
                    if (m_PlayerDropdown.options[i].text.Equals(player))
                    {
                        m_PlayerDropdown.value = i;
                        break;
                    }
                }
            }
            m_ChampionPanel.SetActive(true);
            PopulateChampionDropdown();
        }
    }

    public void AssignChampion(string champion, bool shouldUpdate = false)
    {
        m_CurrentChampion = CompDataController.Instance.GetChampionData(champion);

        if (m_CurrentChampion == null)
        {
            m_IsChampionLocked = false;
            m_ChampionDropdown.value = 0;
            m_ChampionLockToggle.isOn = false;
            m_CompText.text = "";
        }
        else
        {
            string statsString = "";

            if (m_CurrentChampion.m_IsHardCC)
            {
                statsString += "- Hard CC\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsHardEngage)
            {
                statsString += "- Hard Engage\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsDisengage)
            {
                statsString += "- Disengage\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsPoke)
            {
                statsString += "- Poke\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsWaveclear)
            {
                statsString += "- Wave Clear\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_Tank)
            {
                statsString += "- Tank\n";
            }
            else
            {
                statsString += "\n";
            }

            if (!string.IsNullOrEmpty(statsString))
            {
                statsString += "\n";
            }
            else
            {
                statsString += "\n";
            }

            switch (m_CurrentChampion.m_DamageType)
            {
                case eDamageType.PHYSICAL:
                    statsString += "- Physical\n";
                    break;
                case eDamageType.MAGICAL:
                    statsString += "- Magical\n";
                    break;
                case eDamageType.UTILITY:
                    statsString += "- Utility\n";
                    break;
            }

            statsString += "\n";

            if (m_CurrentChampion.m_IsEarlyGame)
            {
                statsString += "- Strong Early\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsMidGame)
            {
                statsString += "- Strong Mid\n";
            }
            else
            {
                statsString += "\n";
            }

            if (m_CurrentChampion.m_IsLateGame)
            {
                statsString += "- Strong Late\n";
            }
            else
            {
                statsString += "\n";
            }

            m_CompText.text = statsString;

            if (!m_ChampionDropdown.options[m_ChampionDropdown.value].text.Equals(champion))
            {
                for (int i = 0; i < m_ChampionDropdown.options.Count; i++)
                {
                    if (m_ChampionDropdown.options[i].text.Equals(champion))
                    {
                        m_ChampionDropdown.value = i;
                        break;
                    }
                }
            }
        }
        
        CompDataController.Instance.ChampionChanged(shouldUpdate);
    }

    public void LockPlayer()
    {
        m_IsPlayerLocked = m_PlayerLockToggle.isOn;
        m_PlayerDropdown.interactable = !m_PlayerLockToggle.isOn;
    }

    public void LockChampion()
    {
        m_IsChampionLocked = m_ChampionLockToggle.isOn;
        m_ChampionDropdown.interactable = !m_ChampionLockToggle.isOn;
    }
}
