namespace Ch120.Utils.Common
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
    }
}

