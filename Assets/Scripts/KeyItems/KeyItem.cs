using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField]
    List<string> keyTagsList;
    private HashSet<string> keyTags = null;
    public HashSet<string> KeyTags
    {
        get
        {
            if (keyTags == null) keyTags = new HashSet<string>(keyTagsList);
            return keyTags;
        }
    }

    virtual public bool IsRightKey(KeyItem key)
    {
        HashSet<string> intersect = new HashSet<string>(key.KeyTags);
        intersect.IntersectWith(KeyTags);
        return intersect.Count > 0;
    }
}
