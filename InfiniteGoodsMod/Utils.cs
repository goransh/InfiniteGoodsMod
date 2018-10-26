using UnityEngine;

namespace InfiniteGoodsMod {
    public class Utils {
        private const bool DebugMode = true;

        public static void Log(string message) {
            if (DebugMode)
                Debug.Log("[Infinite Goods]: " + message);
        }
    }
}