using UnityEngine;
using System.Collections;

public class Location
{

    //world cords

    private Vector2 worldCord;


    //zone
    private ZoneTypes zone;
    public enum ZoneTypes
    {
        //example zones for 
        A,
        B,
        C,
        None
    }

    public Vector2 WorldCord
    {
        get { return worldCord; }
    }
	
    public ZoneTypes Zone
    {
        get { return zone; }
    }

    public Location(Vector2 worldCord)
    {
        this.worldCord = worldCord;
        zone = ZoneTypes.None;
    }

    public Location(ZoneTypes zone)
    {
        this.zone = zone;
        worldCord = Vector2.zero;
    }

    public bool Compare(Location location)
    {
        if (worldCord != Vector2.zero && location.worldCord == worldCord)
        {
            return true;
        }
        else if (zone != ZoneTypes.None && location.zone == zone)
        {
            return true;
        }
        else
            return false;
    }
}
