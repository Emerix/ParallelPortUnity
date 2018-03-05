using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class ParallelPort : MonoBehaviour {
    [DllImport("inpoutx64.dll")]
    public static extern short Inp32(int address);
    [DllImport("inpoutx64.dll", EntryPoint = "Out32")]
    public static extern void Output(int address, int value);
    [DllImport("inpoutx64.dll", EntryPoint = "IsInpOutDriverOpen")]
    private static extern UInt32 IsInpOutDriverOpen_x64();
    [SerializeField]
    int PortAdress = 889;
    void Start () {
        uint nResult = IsInpOutDriverOpen_x64();

        if (nResult == 0)
        {
            Debug.LogError("Unable to open inpoutx64 driver");
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Figure out the proper address range..
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ParallelCoroutine());
        }

    }
    IEnumerator ParallelCoroutine()
    {
        Debug.Log("Beep");
        Output(PortAdress, 5);
        yield return new WaitForSecondsRealtime(0.005f);
        Output(PortAdress, 0);
        Debug.Log("Unbeep");
    }
}
