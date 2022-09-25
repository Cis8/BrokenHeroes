using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Modifiers/Unique Marks/Nora/RisingHate")]
public class RisingHateData : ModifierData
{
    NoraScalings noraScalings;

    public NoraScalings NoraScalings { get => noraScalings; set => noraScalings = value; }

    public override Modifier InitializeModifier(Fighter target, Fighter appliedBy)
    {
        // This is an example of how to use reflection to load custom scalings at runtime.
        // In such a simple case id advised to have an extra field in the scriptable object
        RisingHate rh = new RisingHate(Duration, 1, this, target, appliedBy);
        /*CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
        ce.DataContext = hm.AppliedBy.GetUnit();
        hm.Scaling = System.Convert.ToSingle(ce.Evaluate(noraScalings.dataArray.Where(e => e.Name == "HateMark").FirstOrDefault().Formula));*/
        //rh.Scaling = 0.1f;
        return rh;
        //return new HateMark(Duration, 1, this, target, appliedBy);
    }

    private void OnEnable()
    {
        Addressables.LoadAssetAsync<NoraScalings>("NoraScalings").Completed += handle => NoraScalings = handle.Result;
    }
}
