using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParseChamps : MonoBehaviour
{
    public const int CHAMP_NAME = 0;
    public const int HARD_CC = 1;
    public const int HARD_ENGAGE = 2;
    public const int DISENGAGE = 3;
    public const int POKE = 4;
    public const int WAVECLEAR = 5;
    public const int TANK = 6;
    public const int PHYSICAL = 7;
    public const int MAGICAL = 8;
    public const int UTILITY = 9;
    public const int EARLY_GAME = 10;
    public const int MID_GAME = 11;
    public const int LATE_GAME = 12;

    public TextAsset m_Champs;

#if UNITY_EDITOR
    [ContextMenu("ParseChampData")]
    public void ParseChampsFromString()
    {
        string[] newLines = m_Champs.text.Split('\n');

        for (int i = 1; i < newLines.Length; i++)
        {
            string[] champString = newLines[i].Split(',');
            ChampionData data = ScriptableObject.CreateInstance<ChampionData>();
            data.m_ChampionName = champString[CHAMP_NAME];
            data.m_IsHardCC = champString[HARD_CC].Equals("y");
            data.m_IsHardEngage = champString[HARD_ENGAGE].Equals("y");
            data.m_IsDisengage = champString[DISENGAGE].Equals("y");
            data.m_IsPoke = champString[POKE].Equals("y");
            data.m_IsWaveclear = champString[WAVECLEAR].Equals("y");
            data.m_Tank = champString[TANK].Equals("y");

            if (champString[PHYSICAL].Equals("y"))
            {
                data.m_DamageType = eDamageType.PHYSICAL;
            }
            else if (champString[MAGICAL].Equals("y"))
            {
                data.m_DamageType = eDamageType.MAGICAL;
            }
            else if (champString[UTILITY].Equals("y"))
            {
                data.m_DamageType = eDamageType.UTILITY;
            }

            data.m_IsEarlyGame = champString[EARLY_GAME].Equals("y");
            data.m_IsMidGame = champString[MID_GAME].Equals("y");
            data.m_IsLateGame = champString[LATE_GAME].Contains("y");

            string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ChampionData/" + data.m_ChampionName + ".asset");
            AssetDatabase.CreateAsset(data, assetPath);
            AssetDatabase.SaveAssets();
        }
    }
#endif
}
