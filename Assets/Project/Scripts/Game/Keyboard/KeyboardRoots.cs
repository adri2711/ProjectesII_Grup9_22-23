using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardRoots : MonoBehaviour
{
    [SerializeField] private Transform keyRootObject;
    private KeyboardManager keyboard;
    public static List<KeyRoot> keyRoots;
    void Start()
    {
        keyboard = GetComponent<KeyboardManager>();
        keyRoots = new List<KeyRoot>();
        for (int i = 0; i < keyboard.keys.Count; i++)
        {
            keyboard.keys[i].GetComponent<KeyPlacement>().index = keyboard.keys[i].GetComponent<KeyPlacement>().rootIndex = i;
            keyRoots.Add(CreateRoot(i));
        }
    }
    void Update()
    {
    }
    private KeyRoot CreateRoot(int i)
    {
        Transform newRootTransform = Instantiate(keyRootObject.transform, keyboard.keys[i].transform.position, Quaternion.identity, GameObject.Find("KeyRoots").transform);
        return newRootTransform.AddComponent<KeyRoot>().Setup(i);
    }
    public static int FindRoot(int currIndex)
    {
        if (currIndex >= 0 && currIndex < keyRoots.Count)
        {
            return keyRoots[currIndex].currIndex;
        }
        return -1;
    }
}
