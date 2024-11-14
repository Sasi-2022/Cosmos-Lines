using System.Collections.Generic;
using UnityEngine;

namespace Connect.Common
{
    [CreateAssetMenu(fileName = "Levels",menuName = "SO/AllLevels")]
    public class LevelList : ScriptableObject
    {
        public List<Levels> Levellist;
        public IEnumerable<object> Level;

        public IEnumerable<object> level { get; set; }
    }

   [System.Serializable]
    public class Levels
    {
        public string levelname;
        public List<LevelData> Level;
    }
}
