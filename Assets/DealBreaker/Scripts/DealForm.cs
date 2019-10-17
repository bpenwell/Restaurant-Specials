/*
 * Copyright (C) 2018 Ben Penwell
 * Made for the Deal-Breaker app
 * 
 * DatabaseContact.cs
 * Class that contacts database for deal query and/or submital
 */

using System;

public class DealForm
{
    public String BusinessName;
    public String BusinessAddress;
    public String City; //Should autofill
    public String State; //Should autofill
    public String DealTags;
    public String DealType;
    public String ShortDescription;
    public String Frequency; //one off - daily - m/t/w/t/f/s/su - 
    public bool AllDay;
    public UInt16 StartTime;
    public UInt16 EndTime;
    //public String Zipcode; //Shouldnt need this data
}