using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    private List<KeyCode> keyCodeInput = new List<KeyCode>();

    private enum Keys
    {
        q,
        w,
        e,
        r,
        t,
        y,
        u,
        i,
        o,
        p,
        a,
        s,
        d,
        f,
        g,
        h,
        j,
        k,
        l,
        z,
        x,
        c,
        v,
        b,
        n,
        m

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    int KeyCodeToKeyboardPos(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.Q:
                return (int)Keys.q;
            case KeyCode.W:
                return (int)Keys.w;
            case KeyCode.E:
                return (int)Keys.e;
            case KeyCode.R:
                return (int)Keys.r;
            case KeyCode.T:
                return (int)Keys.t;
            case KeyCode.Y:
                return (int)Keys.y;
            case KeyCode.U:
                return (int)Keys.u;
            case KeyCode.I:
                return (int)Keys.i;
            case KeyCode.O:
                return (int)Keys.o;
            case KeyCode.P:
                return (int)Keys.p;
            case KeyCode.A:
                return (int)Keys.a;
            case KeyCode.S:
                return (int)Keys.s;
            case KeyCode.D:
                return (int)Keys.d;
            case KeyCode.F:
                return (int)Keys.f;
            case KeyCode.G:
                return (int)Keys.g;
            case KeyCode.H:
                return (int)Keys.h;
            case KeyCode.J:
                return (int)Keys.j;
            case KeyCode.K:
                return (int)Keys.k;
            case KeyCode.L:
                return (int)Keys.l;
            case KeyCode.Z:
                return (int)Keys.z;
            case KeyCode.X:
                return (int)Keys.x;
            case KeyCode.C:
                return (int)Keys.c;
            case KeyCode.V:
                return (int)Keys.v;
            case KeyCode.B:
                return (int)Keys.b;
            case KeyCode.N:
                return (int)Keys.n;
            case KeyCode.M:
                return (int)Keys.m;
            default:
                return -1;
        }
    }

    string KeyCodesToString(Queue<KeyCode> input)
    {
        string ret = "";
        foreach (KeyCode code in input)
        {
            ret += code.ToString();
        }
        return ret;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            //Key pressed
            if (e.keyCode != KeyCode.None)
            {
                //keyCodeInput.Enqueue(e.keyCode);
                
            }
            keys[KeyCodeToKeyboardPos(e.keyCode)].PushCorrectLetter();
        }
        else if (e.type == EventType.KeyUp)
        {
            //Key released
            keys[KeyCodeToKeyboardPos(e.keyCode)].ResetLetter();
        }
        
    }

    //private void ParserLoop(string currText)
    //{
    //    if (keyCodeInput.Any())
    //    {
    //        KeyCode currInput = keyCodeInput.First();
    //        Debug.Log(currInput);
    //    }
    //}
}
