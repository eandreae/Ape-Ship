using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorUpdateEvents : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable] public class IntEvent : UnityEvent<int> {}
}
