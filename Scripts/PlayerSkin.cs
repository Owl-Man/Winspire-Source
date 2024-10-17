using System.Collections;
using ScriptableObjects;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer head, body, legs;
    [SerializeField] private Transform rightArmTransform, leftArmTransform;
    [SerializeField] private RobotParts _robotParts;

    private IEnumerator Start()
    {
        //Initialize skin
        var headID = PlayerPrefs.GetInt("HeadID");
        var bodyID = PlayerPrefs.GetInt("BodyID");
        var legsID = PlayerPrefs.GetInt("LegsID");
        
        //Apply player skin
        head.sprite = _robotParts.headIcons[headID];
        body.sprite = _robotParts.bodyIcons[bodyID];
        legs.sprite = _robotParts.legsIcons[legsID];
        
        //Attach position to new skin
        rightArmTransform.localPosition = _robotParts.rightArmPositions[bodyID];
        leftArmTransform.localPosition = _robotParts.leftArmPositions[bodyID];
        
        if (legsID == 0)
        {
            legs.transform.localPosition = _robotParts.legsPositions[bodyID];
        }
        else if (legsID == 1)
        {
            legs.transform.localPosition = _robotParts.legsPositions2[bodyID];
            legs.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        }
        else if (legsID == 2)
        {
            legs.transform.localPosition = _robotParts.legsPositions3[bodyID];
            legs.transform.localScale = new Vector3(1.11f, 1.4f, 1f);
        }

        //Fix scale with body №3
        if (bodyID == 2) body.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        
        yield return null;
    }
}