using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct TeamComp
{
    public List<PlayerData> Players;
    public List<ChampionData> Champs;
    public float CompScore;

    public override string ToString()
    {
        string returnString = CompScore + " --- ";

        for (int i = 0; i < Players.Count; i++)
        {
            returnString += Players[i].m_PlayerName + ": " + Champs[i] + " --- ";
        }

        return returnString;
    }
}

public class CompMaker : MonoBehaviour
{
    public const int HARD_CC = 0;
    public const int HARD_ENGAGE = 1;
    public const int DISENGAGE = 2;
    public const int POKE = 3;
    public const int WAVECLEAR = 4;
    public const int TANK = 5;
    public const int PHYSICAL = 6;
    public const int MAGICAL = 7;
    public const int UTILITY = 8;
    public const int EARLY_GAME = 9;
    public const int MID_GAME = 10;
    public const int LATE_GAME = 11;

    private List<TeamComp> m_TeamComps = new List<TeamComp>();
    private bool m_CompsMade = false;

    public Button m_RollButton;
    public Button m_ResetPlayersButton;
    public Text m_ScoreText;
    public Text m_CompText;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_RollButton.onClick.AddListener(delegate
        {
            RollComp();
        });

        m_ResetPlayersButton.onClick.AddListener(delegate
        {
            m_CompsMade = false;
            CompDataController.Instance.ResetActivePlayers();
        });
    }

    public void RollComp()
    {
        if (!m_CompsMade)
        {
            PopulateComps();
            m_CompsMade = true;
        }

        List<TeamComp> rolledComps = new List<TeamComp>();

        PositionController topPosition = CompDataController.Instance.m_PositionControllers[0];
        PositionController junglePosition = CompDataController.Instance.m_PositionControllers[1];
        PositionController middlePosition = CompDataController.Instance.m_PositionControllers[2];
        PositionController bottomPosition = CompDataController.Instance.m_PositionControllers[3];
        PositionController supportPosition = CompDataController.Instance.m_PositionControllers[4];

        List<ChampionData> bannedChampions = CompDataController.Instance.m_BannedChampions;

        foreach (TeamComp comp in m_TeamComps)
        {
            if (topPosition.m_IsPlayerLocked && !comp.Players[0].m_PlayerName.Equals(topPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (junglePosition.m_IsPlayerLocked && !comp.Players[1].m_PlayerName.Equals(junglePosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (middlePosition.m_IsPlayerLocked && !comp.Players[2].m_PlayerName.Equals(middlePosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (bottomPosition.m_IsPlayerLocked && !comp.Players[3].m_PlayerName.Equals(bottomPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (supportPosition.m_IsPlayerLocked && !comp.Players[4].m_PlayerName.Equals(supportPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (topPosition.m_IsChampionLocked && !comp.Champs[0].m_ChampionName.Equals(topPosition.m_CurrentChampion.m_ChampionName))
            {
                continue;
            }

            if (junglePosition.m_IsChampionLocked && !comp.Champs[1].m_ChampionName.Equals(junglePosition.m_CurrentChampion.m_ChampionName))
            {
                continue;
            }

            if (middlePosition.m_IsChampionLocked && !comp.Champs[2].m_ChampionName.Equals(middlePosition.m_CurrentChampion.m_ChampionName))
            {
                continue;
            }

            if (bottomPosition.m_IsChampionLocked && !comp.Champs[3].m_ChampionName.Equals(bottomPosition.m_CurrentChampion.m_ChampionName))
            {
                continue;
            }

            if (supportPosition.m_IsChampionLocked && !comp.Champs[4].m_ChampionName.Equals(supportPosition.m_CurrentChampion.m_ChampionName))
            {
                continue;
            }

            bool containsBanned = false;

            for (int i = 0; i < comp.Champs.Count; i++)
            {
                if (bannedChampions.Contains(comp.Champs[i]))
                {
                    containsBanned = true;
                    break;
                }
            }

            if (containsBanned)
            {
                continue;
            }

            rolledComps.Add(comp);
        }

        rolledComps = rolledComps.OrderByDescending(x => x.CompScore).ToList<TeamComp>();

        float maxScore = rolledComps[0].CompScore;

        List<TeamComp> maxScoreComps = new List<TeamComp>();

        for (int i = 0; i < rolledComps.Count; i++)
        {
            if (rolledComps[i].CompScore >= maxScore)
            {
                maxScoreComps.Add(rolledComps[i]);
            }
            else
            {
                break;
            }
        }

        TeamComp bestComp = maxScoreComps[UnityEngine.Random.Range(0,maxScoreComps.Count)];

        if (!topPosition.m_IsPlayerLocked)
        {
            topPosition.AssignPlayer(bestComp.Players[0].m_PlayerName);
        }
        if (!topPosition.m_IsChampionLocked)
        {
            topPosition.AssignChampion(bestComp.Champs[0].m_ChampionName);
        }

        if (!junglePosition.m_IsPlayerLocked)
        {
            junglePosition.AssignPlayer(bestComp.Players[1].m_PlayerName);
        }
        if (!junglePosition.m_IsChampionLocked)
        {
            junglePosition.AssignChampion(bestComp.Champs[1].m_ChampionName);
        }

        if (!middlePosition.m_IsPlayerLocked)
        {
            middlePosition.AssignPlayer(bestComp.Players[2].m_PlayerName);
        }
        if (!middlePosition.m_IsChampionLocked)
        {
            middlePosition.AssignChampion(bestComp.Champs[2].m_ChampionName);
        }

        if (!bottomPosition.m_IsPlayerLocked)
        {
            bottomPosition.AssignPlayer(bestComp.Players[3].m_PlayerName);
        }
        if (!bottomPosition.m_IsChampionLocked)
        {
            bottomPosition.AssignChampion(bestComp.Champs[3].m_ChampionName);
        }

        if (!supportPosition.m_IsPlayerLocked)
        {
            supportPosition.AssignPlayer(bestComp.Players[4].m_PlayerName);
        }
        if (!supportPosition.m_IsChampionLocked)
        {
            supportPosition.AssignChampion(bestComp.Champs[4].m_ChampionName);
        }

        FillScoreText(bestComp.CompScore);
        FillCompText(bestComp);
    }

    public void FillScoreText(float score)
    {
        m_ScoreText.text = "Score: " + score;
    }

    public void FillCompText(TeamComp comp)
    {
        int[] compCounts = null;
        Score.Instance.GetTotalScore(comp, out compCounts);

        string compText = "";
        if (compCounts == null)
        {
            m_CompText.text = "Can't calculate score!";
            return;
        }

        if (compCounts[HARD_CC] < 3)
        {
            compText += 3 - compCounts[HARD_CC] + " Hard CC,";
        }

        if (compCounts[HARD_ENGAGE] < 2)
        {
            compText += 2 - compCounts[HARD_ENGAGE] + " Hard Engage,";
        }

        if (compCounts[DISENGAGE] < 1)
        {
            compText += 1 - compCounts[DISENGAGE] + " Disengage,";
        }

        if (compCounts[POKE] < 1)
        {
            compText += 1 - compCounts[POKE] + " Poke,";
        }

        if (compCounts[WAVECLEAR] < 1)
        {
            compText += 1 - compCounts[WAVECLEAR] + " Wave Clear,";
        }

        if (compCounts[TANK] < 2)
        {
            compText += 2 - compCounts[TANK] + " Tank,";
        }

        if (compCounts[PHYSICAL] < 1)
        {
            compText += 1 - compCounts[PHYSICAL] + " AD,";
        }

        if (compCounts[MAGICAL] < 1)
        {
            compText += 1 - compCounts[MAGICAL] + " AP,";
        }

        if (compCounts[UTILITY] < 1)
        {
            compText += 1 - compCounts[UTILITY] + " Utility,";
        }

        if (compCounts[EARLY_GAME] < 1)
        {
            compText += 1 - compCounts[EARLY_GAME] + " Early Game,";
        }

        if (compCounts[MID_GAME] < 1)
        {
            compText += 1 - compCounts[MID_GAME] + " Mid Game,";
        }

        if (compCounts[LATE_GAME] < 2)
        {
            compText += 2 - compCounts[LATE_GAME] + " Late Game,";
        }

        if (!string.IsNullOrEmpty(compText))
        {
            compText = compText.Substring(0, compText.Length - 1);
            m_CompText.text = "Missing:\n" + compText;
        }
        else
        {
            m_CompText.text = "All bases covered!";
        }
    }

    [ContextMenu("PopulateComps")]
    public void PopulateComps()
    {
        m_TeamComps.Clear();
        GC.Collect();

        List<List<PlayerData>> players = GetPlayers();

        PositionController topPosition = CompDataController.Instance.m_PositionControllers[0];
        PositionController junglePosition = CompDataController.Instance.m_PositionControllers[1];
        PositionController middlePosition = CompDataController.Instance.m_PositionControllers[2];
        PositionController bottomPosition = CompDataController.Instance.m_PositionControllers[3];
        PositionController supportPosition = CompDataController.Instance.m_PositionControllers[4];

        List<ChampionData> bannedChampions = CompDataController.Instance.m_BannedChampions;

        bool sizeExceeded = false;

        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++)
        {
            if (topPosition.m_IsPlayerLocked && !players[playerIndex][0].m_PlayerName.Equals(topPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (junglePosition.m_IsPlayerLocked && !players[playerIndex][1].m_PlayerName.Equals(junglePosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (middlePosition.m_IsPlayerLocked && !players[playerIndex][2].m_PlayerName.Equals(middlePosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (bottomPosition.m_IsPlayerLocked && !players[playerIndex][3].m_PlayerName.Equals(bottomPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            if (supportPosition.m_IsPlayerLocked && !players[playerIndex][4].m_PlayerName.Equals(supportPosition.m_CurrentPlayer.m_PlayerName))
            {
                continue;
            }

            for (int top = 0; top < players[playerIndex][0].m_TopChampions.Count; top++)
            {
                if (topPosition.m_IsChampionLocked && !players[playerIndex][0].m_TopChampions[top].m_ChampionName.Equals(topPosition.m_CurrentChampion.m_ChampionName))
                {
                    continue;
                }
                
                for (int jungle = 0; jungle < players[playerIndex][1].m_JungleChampions.Count; jungle++)
                {
                    if (junglePosition.m_IsChampionLocked && !players[playerIndex][1].m_JungleChampions[jungle].m_ChampionName.Equals(junglePosition.m_CurrentChampion.m_ChampionName))
                    {
                        continue;
                    }
                    for (int middle = 0; middle < players[playerIndex][2].m_MiddleChampions.Count; middle++)
                    {
                        if (middlePosition.m_IsChampionLocked && !players[playerIndex][2].m_MiddleChampions[middle].m_ChampionName.Equals(middlePosition.m_CurrentChampion.m_ChampionName))
                        {
                            continue;
                        }

                        for (int bottom = 0; bottom < players[playerIndex][3].m_BottomChampions.Count; bottom++)
                        {
                            if (bottomPosition.m_IsChampionLocked && !players[playerIndex][3].m_BottomChampions[bottom].m_ChampionName.Equals(bottomPosition.m_CurrentChampion.m_ChampionName))
                            {
                                continue;
                            }

                            for (int support = 0; support < players[playerIndex][4].m_SupportChampions.Count; support++)
                            {


                                if (supportPosition.m_IsChampionLocked && !players[playerIndex][4].m_SupportChampions[support].m_ChampionName.Equals(supportPosition.m_CurrentChampion.m_ChampionName))
                                {
                                    continue;
                                }

                                List<ChampionData> loopChampComp = new List<ChampionData>();
                                loopChampComp.Add(players[playerIndex][0].m_TopChampions[top]);
                                loopChampComp.Add(players[playerIndex][1].m_JungleChampions[jungle]);
                                loopChampComp.Add(players[playerIndex][2].m_MiddleChampions[middle]);
                                loopChampComp.Add(players[playerIndex][3].m_BottomChampions[bottom]);
                                loopChampComp.Add(players[playerIndex][4].m_SupportChampions[support]);

                                TeamComp teamComp = new TeamComp();
                                teamComp.Champs = loopChampComp;
                                teamComp.Players = players[playerIndex];
                                teamComp.CompScore = Score.Instance.GetTotalScore(teamComp);

                                m_TeamComps.Add(teamComp);

                                if (m_TeamComps.Count > 2000000)
                                {
                                    sizeExceeded = true;
                                    break;
                                }
                            }

                            if (sizeExceeded)
                            {
                                break;
                            }
                        }

                        if (sizeExceeded)
                        {
                            break;
                        }
                    }

                    if (sizeExceeded)
                    {
                        break;
                    }
                }

                if (sizeExceeded)
                {
                    break;
                }
            }

            if (sizeExceeded)
            {
                break;
            }
        }

        m_TeamComps = m_TeamComps.OrderByDescending(x => x.CompScore).ToList<TeamComp>();

        float maxScore = m_TeamComps[0].CompScore;

        List<TeamComp> maxScoreComps = new List<TeamComp>();

        for (int i = 0; i < m_TeamComps.Count; i++)
        {
            if (m_TeamComps[i].CompScore >= maxScore)
            {
                maxScoreComps.Add(m_TeamComps[i]);
            }
            else
            {
                break;
            }
        }

        GC.Collect();
    }

    public List<List<PlayerData>> GetPlayers()
    {
        List<PlayerData> basePlayers = new List<PlayerData>(CompDataController.Instance.GetActivePlayers().ToArray());
        List<PlayerData> lockedPlayers = new List<PlayerData>() { null, null, null, null, null };

        int lockedPlayerCount = 0;

        List<List<PlayerData>> playerDatas = new List<List<PlayerData>>();

        List<int> indices = new List<int>();
        for (int i = 0; i < basePlayers.Count; i++)
        {
            if (!lockedPlayers.Contains(basePlayers[i]))
            {
                indices.Add(i);
            }
        }

        foreach (IEnumerable<int> i in GetPermutations(indices.ToArray(), (5 - lockedPlayerCount)))
        {
            List<PlayerData> playerComp = new List<PlayerData>();

            int playerIndex = 0;
            for (int j = 0; j < 5; j++)
            {
                if (lockedPlayers[j] != null)
                {
                    playerComp.Add(lockedPlayers[j]);
                }
                else
                {
                    playerComp.Add(basePlayers[i.ElementAt(playerIndex)]);
                    playerIndex++;
                }
            }

            playerDatas.Add(playerComp);

        }

        return playerDatas;
    }

    static IEnumerable<IEnumerable<T>>
    GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}
