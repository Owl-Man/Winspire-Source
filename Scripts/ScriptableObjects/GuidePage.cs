using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Guide Page", menuName = "Guide Page")]
    public class GuidePage : ScriptableObject
    {
        public new string name;
        [TextArea(3, 20)] public string content;
    }
}