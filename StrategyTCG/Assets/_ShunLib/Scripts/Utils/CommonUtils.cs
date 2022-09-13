using UnityEngine;

namespace ShunLib.Utils.Common
{
    public class CommonUtils
    {
        // 値がN以上か
        public static bool CheckValueOrHigher(int compareValue, int originalValue)
        {
            return compareValue <= originalValue;
        }

        // 値がN以下か
        public static bool CheckValueOrLower(int compareValue, int originalValue)
        {
            return compareValue >= originalValue;
        }
        
        // ゲーム終了
        public static void GameExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}

