using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Robot Parts Data", menuName = "Robot Parts")]
    public class RobotParts : ScriptableObject
    {
        public new string name;
        public Sprite[] headIcons, bodyIcons, legsIcons;
        
        public Vector2[] leftArmPositions, rightArmPositions, legsPositions, legsPositions2, legsPositions3;
    }
}