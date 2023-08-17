using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FighterName { 
    None, // Represents none hero
    VI,
    Doombot,
    Nora
}
public class FighterLogicFactory : MonoBehaviour
{
    /*private static FighterLogicFactory _current;

    public static FighterLogicFactory current
    {
        get
        {
            if (_current == null) _current = (Instantiate(Resources.Load("FighterLogicFactory")) as GameObject).GetComponent<FighterLogicFactory>();
            return _current;
        }
    }*/

    /*public static FighterLogic GetFighterLogic(string name, Fighter parent)
    {
        string fighterHeader = "Fighters/" + name + "/";
        ////
        ///("Trying to get " + name + "'s unit.");
        Unit u = UnitConverter.Convert(Resources.Load<UnitSpec>(fighterHeader + name));
        switch (name)
        {
            case "VI":
                ////Disabled.Log("Returning VI's logic.");
                return new VILogic(u, parent, parent.tag);
            case "Doombot":
                ////Disabled.Log("Returning Doombot's logic.");
                return new DoombotLogic(u, parent, parent.tag);
            case "Nora":
                return new NoraLogic(u, parent, parent.tag);
            default:
                return new DoombotLogic(u, parent, parent.tag);
        }
    }*/
}
