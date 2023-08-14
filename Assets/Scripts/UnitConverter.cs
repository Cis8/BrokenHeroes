using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitConverter
{
    /*public static Unit Convert(UnitSpec heroSpec)
    {
        return Unit();
    }*/

    static int ConvertMaxHp(int level, ClassEnum @class)
    {
        // FIXME use excel function
        return 300 + level * 10;
    }

    static int ConvertPhysicalAtk(int level, ClassEnum @class)
    {
        return 30 + level * 2;
    }
    static int ConvertMagicalAtk(int level, ClassEnum @class)
    {
        return 30 + level * 2;
    }
    static int ConvertArmorPenetration(int level, ClassEnum @class)
    {
        return 0;
    }
    static int ConvertMagicDefPenetration(int level, ClassEnum @class)
    {
        return 0;
    }
    static int ConvertSpeed(int level, ClassEnum @class)
    {
        return 10 + level * 1;
    }
    static int ConvertArmor(int level, ClassEnum @class)
    {
        return 10 + 2 * level;
    }

    static int ConvertMagicalDef(int level, ClassEnum @class)
    {
        return 10 + 2 * level;
    }

    /* Use as a template
    static int ConvertStat(int level, ClassEnum @class)
    {
        return 10 + 2 * level;
    }*/
}
