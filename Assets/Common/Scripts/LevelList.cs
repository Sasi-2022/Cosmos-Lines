using System.Collections.Generic;
using UnityEngine;

namespace Connect.Common
{
    [CreateAssetMenu(fileName = "Levels",menuName = "SO/AllLevels")]
    public class LevelList : ScriptableObject
    {
        public List<Levels> Levellist;
    }

   [System.Serializable]
    public class Levels
    {
        public string levelname;
        public List<LevelData> Level;
    }
}
