using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParsePlayers : MonoBehaviour
{
    const int PLAYER_NAME = 0;
    const int CHAMPION = 1;
    const int POSITION = 2;

    public TextAsset m_Player;

#if UNITY_EDITOR
    [ContextMenu("ParsePlayerData")]
    public void ParsePlayerFromString()
    {
        string[] newLines = m_Player.text.Split('\n');

        PlayerData data = ScriptableObject.CreateInstance<PlayerData>();
        data.m_PlayerName = newLines[1].Split(',')[PLAYER_NAME];
        data.m_TopChampions = new List<ChampionData>();
        data.m_JungleChampions = new List<ChampionData>();
        data.m_MiddleChampions = new List<ChampionData>();
        data.m_BottomChampions = new List<ChampionData>();
        data.m_SupportChampions = new List<ChampionData>();

        for (int i = 1; i < newLines.Length; i++)
        {
            string[] playerString = newLines[i].Split(',');

            if (playerString[POSITION].Contains("Top"))
            {
                data.m_TopChampions.Add(Resources.Load<ChampionData>("ChampionData/" + playerString[CHAMPION]));
            }
            else if (playerString[POSITION].Contains("Jungle"))
            {
                data.m_JungleChampions.Add(Resources.Load<ChampionData>("ChampionData/" + playerString[CHAMPION]));
            }
            else if (playerString[POSITION].Contains("Mid"))
            {
                data.m_MiddleChampions.Add(Resources.Load<ChampionData>("ChampionData/" + playerString[CHAMPION]));
            }
            else if (playerString[POSITION].Contains("Bot"))
            {
                data.m_BottomChampions.Add(Resources.Load<ChampionData>("ChampionData/" + playerString[CHAMPION]));
            }
            else if (playerString[POSITION].Contains("Sup"))
            {
                data.m_SupportChampions.Add(Resources.Load<ChampionData>("ChampionData/" + playerString[CHAMPION]));
            }
        }

        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/PlayerData/" + data.m_PlayerName + ".asset");
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();
    }
#endif

}
