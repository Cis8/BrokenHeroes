using UnityEditor;
using UnityEngine;

namespace Assets.BattleAssetts.Scripts
{
    public static class CalcEngineUtil
    {
        static CalcEngine.CalcEngine calculator = new CalcEngine.CalcEngine();

        public static int Int32StatScalingCalculator(string formula, Object context)
        {
            calculator.DataContext = context;
            return System.Convert.ToInt32(calculator.Evaluate(formula));
        }
        public static float FloatStatScalingCalculator(string formula, Object context)
        {
            calculator.DataContext = context;
            return System.Convert.ToSingle(calculator.Evaluate(formula));
        }

        public static int Int32DamageCalculator(string baseFormula = "", string selfFormula = "", string targetFormula = "", Object selfContext = null, Object targetContext = null)
        {
            int result = 0;
            if(baseFormula != "")
                result += System.Convert.ToInt32(calculator.Evaluate(baseFormula));
            if (selfFormula != "")
            {
                if (selfContext == null)
                    throw new System.Exception("Self context can't be null for self formula evalutaion.");
                calculator.DataContext = selfContext;
                result += System.Convert.ToInt32(calculator.Evaluate(selfFormula));
            }
            if (targetFormula != "")
            {
                if (targetContext == null)
                    throw new System.Exception("Target context can't be null for target formula evalutaion.");
                calculator.DataContext = targetContext;
                result += System.Convert.ToInt32(calculator.Evaluate(targetFormula));
            }
            return result;
        }

        public static float FloatDamageCalculator(string baseFormula = "", string selfFormula = "", string targetFormula = "", Object selfContext = null, Object targetContext = null)
        {
            float result = 0f;
            if (baseFormula != "")
                result += System.Convert.ToSingle(calculator.Evaluate(baseFormula));
            if (selfFormula != "")
            {
                if (selfContext == null)
                    throw new System.Exception("Self context can't be null for self formula evalutaion.");
                calculator.DataContext = selfContext;
                result += System.Convert.ToSingle(calculator.Evaluate(selfFormula));
            }
            if (targetFormula != "")
            {
                if (targetContext == null)
                    throw new System.Exception("Target context can't be null for target formula evalutaion.");
                calculator.DataContext = targetContext;
                result += System.Convert.ToSingle(calculator.Evaluate(targetFormula));
            }
            return result;
        }
    }
}