﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace D9Concrete
{
    // hardcoded. Generally bad practice, but this is a small mod so I don't need to overgeneralize. 
    public class IngredientValueGetter_Concrete : IngredientValueGetter
    {
        private const float limestoneMultiplier = 1.5f, chunkValue = 20f;

        public override float ValuePerUnitOf(ThingDef t)
        {
            if (!IsStoneyStuff(t) && !IsChunk(t)) return 0.0001f; // Rely on the filter to block invalid items
            if (t.defName == "ChunkLimestone") return limestoneMultiplier * chunkValue;
            if (t.defName == "BlocksLimestone") return limestoneMultiplier;
            if (IsChunk(t)) return chunkValue;
            return 1f;
        }
        public override string BillRequirementsDescription(RecipeDef r, IngredientCount ing)
        {
            return "BillRequires".Translate(ing.GetBaseCount(), ing.filter.Summary);
        }
        public static bool IsStoneyStuff(ThingDef t)
        {
            if (!t.IsStuff) return false;
            foreach (StuffCategoryDef scd in t.stuffProps?.categories) if (scd == StuffCategoryDefOf.Stony) return true;
            return false;
        }
        public static bool IsChunk(ThingDef t) => t.HasModExtension<IsStoneChunk>();
        public override string ExtraDescriptionLine(RecipeDef r)
        {
            return "D9ConcreteValueDesc".Translate(limestoneMultiplier, chunkValue);
        }
    }
    public class IsStoneChunk : DefModExtension
    {
    }
}
