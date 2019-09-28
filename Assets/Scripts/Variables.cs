/*
 * Copyright (C) Ryan Long 2019
 * Made for the Building operations mobile app for the Mathewson IGT Knowledge center
 * 
 * Variables.cs
 * This file contains the public settings variables (mainly colour data) as a way to reduce the amount of times the app reads from the settings file 
 */
using System.Collections.Generic;

public static class Variables
{
    public static int ColorIndex { get; set; }
    public static string ColourProfile { get; set; }
    public static string ColourValues { get; set; }
    public static int HomeScreenMode { get; set; }
    public static string InOut { get; set; }
    public static int KeySetNumber { get; set; }
    public static string Name { get; set; }
    public static int Vibration { get; set; }
    public static bool ValidUser { get; set; }
    public static bool doneLoading { get; set; }
    public static int[] FloorCount { get; set; }
    public static string HourlyAssignment { get; set; }
    public static byte[] ColourCodes { get; set; }
    public static int OnRounds { get; set; }
    public static List<string> IssueArray;
    public static List<string> ScannedQRCodes;
    public static List<string> ScannedPrinters;
}
