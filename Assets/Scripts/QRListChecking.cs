using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QRListChecking : MonoBehaviour {
    /*
    private int numScanned = 0;
    private readonly List<string> Locations = new List<string>{ "1 Back Stairs", "1 Main Gate", "1 N Restroom", "1 Garden", "1 Stairs B", "1 Viewing Room", "2 Back Stairs", "Loading Docks", "2 Stairs F", "2 N Entrance", "2 S Entrance", "2 N Restroom", "2 S Restroom", "Popular Reading", "Reference Area", "3 Back Stairs", "3 Basque Lobby", "3 Basque Back", "3 N Overlook", "3 N Restroom", "3 S Restroom", "3 S Stacks", "3 Tower", "4 Back Stairs", "4 E Stacks", "4 N Stacks", "4 S Stacks", "4 N Restroom", "4 S Restroom", "4 Carrels", "4 Tower", "4 Rotunda", "5 Roof Access", "5 E Stacks", "5 S Stacks", "5 N W Corner", "5 N Restroom", "5 S Restroom", "5 Tower" };
    //private bool[] checkTable;
    private List<string> unscannedCodes = new List<string>();

    private void Start()
    {   
        //initialise the bool check table at the start
        checkTable = new bool[Locations.Count];
    }

    //update the bool table when the user scans a code to indicate that that code has been scanned
    public void UpdateTable(string arg)
    {
        for (int i = 0; i < Locations.Count; i++)
        {
            if (arg == Locations[i])
            {
                checkTable[i] = true;
                break;
            }
        }
    }

    //return a list of unscanned codes
    public List<string> GetUnscanndedCodes()
    {
        unscannedCodes = Locations.Except(Variables.ScannedQRCodes).ToList();
        return unscannedCodes;
    }

    //reset the bool table to false
    public void ResetArrays()
    {
        for (int i = 0; i < checkTable.Length; i++) checkTable[i] = false;
    }
    */
}
