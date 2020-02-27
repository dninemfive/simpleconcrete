using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace D9Concrete
{
    // hardcoded. Generally bad practice, but this is a small mod so I don't need to overgeneralize. 
    public class IngredientValueGetter_Concrete : IngredientValueGetter
    {
        private const float LimestoneMultiplier = 1.5f;

        public override float ValuePerUnitOf(ThingDef t)
        {
            if (!t.IsStuff || !IsStoneyStuff(t)) return 0f;
            if (t.defName == "ChunkLimestone") return LimestoneMultiplier * 20f;
            if (t.defName == "BlocksLimestone") return LimestoneMultiplier;
            if (t.HasModExtension<IsStoneChunk>()) return 20f;
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
    public class IsStoneChunk : DefModExtension
    {
    }
}
