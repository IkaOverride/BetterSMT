﻿using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BetterSMT.Patches;

[HarmonyPatch(typeof(ProductListing))]
public class ProductListingPatch
{
    [HarmonyPatch("ConvertFloatToTextPrice"), HarmonyPostfix]
    private static void ConvertFloatToTextPricePatch(ref string __result)
    {
        __result = BetterSMT.ReplaceCommas(__result);
    }

    [HarmonyPatch(typeof(ProductListing))]
    public class PillarPatch
    {
        [HarmonyPatch("OnStartClient"), HarmonyPostfix]
        private static void PillarRemovalPatch()
        {
            if (BetterSMT.RemovePillars.Value == true)
            {
                HashSet<string> pillarSuffixes = [" (6)", " (4)", " (7)"];
                HashSet<string> beamSuffixes = [ " (4)", " (13)", " (21)", " (24)", " (26)", " (12)", " (11)", " (2)", " (4)",
                        " (29)", " (33)", " (1)", " (3)", " (5)", " (30)", " (34)"];

                UnityEngine.Object[] allObjects = Object.FindObjectsOfType<UnityEngine.Object>();

                Object[] allPillars = allObjects.Where(pillar => pillar.name.StartsWith("Pillar_BeamCross") && pillarSuffixes.Any(suffix => pillar.name.EndsWith(suffix))).ToArray();
                Object[] allBeams = allObjects.Where(pillar => pillar.name.StartsWith("Beam") && beamSuffixes.Any(suffix => pillar.name.EndsWith(suffix))).ToArray();
                Object[] extraBeam = allObjects.Where(pillar => pillar.name.Equals("Beam")).ToArray();

                foreach (Object obj in allPillars.Concat(allBeams).Concat(extraBeam))
                {
                    Object.Destroy(obj);
                }

            }
        }
    }

    //[HarmonyPatch(typeof(ProductListing))]
    //[HarmonyPatch("OnStartClient")]
    //public class Patch_ProductMaxItems
    //{
    //    private static void Postfix(ProductListing __instance)
    //    {
    //        List<string> productNames =
    //        [
    //            "0_Pasta", "1_WaterBottle", "2_HoneyCereals", "3_Rice", "4_Salt", "5_Sugar", "6_Margarine", "7_Flour", "8_AppleJuice", "9_Oil", "10_Ketchup",
    //            "11_SliceBread", "12_Pepper", "13_OrangeJuice", "14_BarbaqueSauce", "15_MustardSauce", "16_SpaghettiBox", "17_TunaPate", "18_FiberCereals", "19_FlourSupremeQuality", "20_BlackCoffee",
    //            "21_EggBox", "22_Houmous", "23_WhiteFlour", "24_CaneSugarBox", "25_Sugar", "26_Macarroni", "27_SugarEcologic", "28_BrownSugar", "29_OliveOil", "30_MashPotatoes",
    //            "31_PotatoeBag", "32_CoffeeEspresso", "33_RiceBasmati", "34_RiceLongGrain", "35_Coffee", "36_PastaSpecial", "37_CerealsChocolate", "38_WaterPremium", "39_WaterSpring", "40_SugarGlass",
    //            "41_SugarBigBox", "42_SugarBrownBigBox", "43_CheeseEmmental", "44_CheeseGruyere", "45_CheeseSkimmed", "46_YoghurtFruit", "47_YoghurtVanilla", "48_MilkBrick", "49_Butter", "50_CheeseParmigiano",
    //            "51_CheeseReggiano", "52_CheeseMozzarella", "53_YoghurtSkimmed", "54_ColaPack", "55_SodaPack", "56_SodaDecafPack", "57_SodaBottle", "58_ColaBottle", "59_SodaDecafBottle", "60_PremiumSoda",
    //            "61_PizzaBarbaque", "62_Fondue", "63_HamCrocanti", "64_CrepeHamCheese", "65_FrenchFries", "66_CrispyPotatoPos", "67_GreenBeans", "68_PizzaFourCheese", "69_PizzaFourSeasons", "70_VegetableMix",
    //            "71_ChickenMix", "72_LasagnaBolognaise", "73_LasagnaVegetable", "74_Toothpaste", "75_ToiletPaperPetus", "76_HandSoap", "77_ShampooAvocado", "78_ShampooEgg", "79_BathGelPowerFresh", "80_ToiletPaperBidet",
    //            "81_SoapMango", "82_ShampooExtraSoft", "83_ShampooHoneyJojoba", "84_ShampooOilArgan", "85_PaperTowel", "86_ToiletPaperDoudou", "87_SoapLemon", "88_BathGelPremium", "89_ShampooBabies", "90_Detergent",
    //            "91_StainRemover", "92_GlassCleaner", "93_DetergentTables", "94_Dishwasher", "95_Bleach", "96_BleachBig", "97_Softener", "98_DetergentPremium", "99_Insecticide", "100_CleaningCloths",
    //            "101_CapsulesPremium", "102_BleachPremium", "103_Ammonia", "104_CookieJar", "105_MaxiCone", "106_ChocoSpread", "107_ChocoPowder", "108_Chips", "109_SweetBonbek", "110_PeachJam",
    //            "111_IceCreamBonbek", "112_ChocolateBox", "113_BiscuitChocolate", "114_BiscuitVanillaChoco", "115_Madeleine", "116_StrawberryJam", "117_PeanutButter", "118_Chipos", "119_Marshmallow", "120_BiscuitLemon",
    //            "121_BiscuitHazelnut", "122_IceCreamPremium", "123_Honey", "124_ChocolateBoxPremium", "125_Foditos", "126_CakePremium", "127_ChoppedBeef", "128_PureBeef", "129_Veal", "130_ChickenWings",
    //            "131_Chicken", "132_ParmaHam", "133_SlicedHam", "134_PeasBig", "135_TunaBig", "136_RedBeans", "137_CatFoodMiao", "138_CatFoodPatPat", "139_DogFoodPatPat", "140_GreenTea",
    //            "141_LemonTea", "142_BlackTea", "143_Peppermint", "144_Mint", "145_Valerian", "146_SushiBig", "147_SushiSmall", "148_SmokedSalmon", "149_CrabSticks", "150_BookElectromagneticTheory",
    //            "151_BookSurprise", "152_BookABC", "153_BookMotherAndChild", "154_BookColors", "155_BookPiticha", "156_BookOnceUpon", "157_BookKrok", "158_BookAdventures", "159_BookDonnine", "160_BookVintage",
    //            "161_BookIwontShare", "162_BeerPackBK", "163_BeerPackTeochew", "164_BeerPackFess", "165_BeerBarrelDEGL", "166_BeerBarrelFess", "167_VodkaMagnat", "168_RedWine", "169_RoseWine", "170_WhiteWine",
    //            "171_BeerBarrelTeochew", "172_VodkaPremiumEay", "173_JapaneseWhisky", "174_WhiskyPremiumGrandMarnier", "175_WhiskyPremiumJackSublett", "176_HydrogenPeroxide", "177_Disinfectant", "178_Ibuprofen", "179_Paracetamol", "180_BandAids",
    //            "181_Laxative", "182_Antihistamine", "183_ZincSupplement", "184_Antioxidant", "185_FishOil", "186_AlgaePills", "187_Vitamins", "188_Melatonin", "189_Sunscreen", "190_StretchCream",
    //            "191_RedAppleTray", "192_GreenAppleTray", "193_ClementineTray", "194_OrangeTray", "195_PearTray", "196_LemonTray", "197_MangoTray", "198_AvocadoTray", "199_KiwiTray", "200_PapayaTray",
    //            "201_StrawberryTray", "202_CherryTray", "203_ArtichokeTray", "204_ZucchiniTray", "205_CarrotTray", "206_TomatoTray", "207_PotatoTray", "208_OnionTray", "209_BananaPack",
    //            "210_Melon", "211_Pineapple", "212_Pumpkin", "213_Watermelon", "214_BabyFoodVegetables", "215_BabyFoodFish", "216_BabyFoodFruit", "217_BabyFoodMeat", "218_BabyMilkLiquid", "219_BabyMilkPowder", "220_EcoDiapers",
    //            "221_BasicDiapers", "222_ToddlerDiapers", "223_PremiumDiapers", "224_CleaningWipes", "225_BathWipes", "226_BabyPowder"
    //        ];
    //
    //        foreach (GameObject prefab in __instance.productPrefabs)
    //        {
    //            if (productNames.Contains(prefab.name))
    //            {
    //                Data_Product dataProduct = prefab.GetComponent<Data_Product>();
    //                if (dataProduct != null)
    //                {
    //                    dataProduct.maxItemsPerBox *= BetterSMT.MaxBoxAmountModifier.Value;
    //                }
    //            }
    //        }
    //    }
    //}
}