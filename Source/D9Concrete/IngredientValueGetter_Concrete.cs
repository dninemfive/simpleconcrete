using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace D9Concrete
{
    public class IngredientValueGetter_Concrete : IngredientValueGetter
    {
        public override float ValuePerUnitOf(ThingDef t)
        {
            if (!t.IsStuff || !IsStoneyStuff(t)) return 0f;
            if (t.HasModExtension<RecipeMultiplier>()) return t.GetModExtension<RecipeMultiplier>().multiplier;
            return 1f;
        }
        public override string BillRequirementsDescription(RecipeDef r, IngredientCount ing)
        {
            return "BillRequires".Translate(ing.GetBaseCount(), ing.filter.Summary);
        }
        public static bool IsStoneyStuff(ThingDef t)
        {
            foreach (StuffCategoryDef scd in t.stuffProps.categories) if (scd == StuffCategoryDefOf.Stony) return true;
            return false;
        }
    }
    public class RecipeMultiplier : DefModExtension
    {
        public float multiplier; 
    }
}
