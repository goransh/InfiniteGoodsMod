using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteGoodsMod.Settings {
    /// <summary>
    /// These are used as IDs in settings. Must not be renamed.
    /// </summary>
    public enum SettingId {
        CommercialGoods,
        CommercialLuxuryProducts,

        SpecializedIndustryOil,
        SpecializedIndustryOre,
        SpecializedIndustryGrain,
        SpecializedIndustryLogs,

        GenericIndustryPetrol,
        GenericIndustryCoal,
        GenericIndustryFood,
        GenericIndustryLumber,

        ShelterGoods,

        WarehouseOil,
        WarehouseOre,
        WarehouseGrain,
        WarehouseLogs,

        PloppedIndustryOil,
        PloppedIndustryOre,
        PloppedIndustryGrain,
        PloppedIndustryLogs,

        UniqueIndustryAnimalProducts,
        UniqueIndustryFlours,
        UniqueIndustryPaper,
        UniqueIndustryPlanedTimber,
        UniqueIndustryPetroleum,
        UniqueIndustryPlastics,
        UniqueIndustryGlass,
        UniqueIndustryMetals,
        UniqueIndustryGrain,

        FishingHarbor,
        FishingFarm,
        FishingMarket,
        FishingProcessing,

        PowerPlantCoal,
        PowerPlantOil,
        
        PedestrianServicePointGoods,
        PedestrianServicePointLuxuryProducts,

        CargoServicePointSpecializedIndustryOil,
        CargoServicePointSpecializedIndustryOre,
        CargoServicePointSpecializedIndustryGrain,
        CargoServicePointSpecializedIndustryLogs,
        
        CargoServicePointGenericIndustryPetrol,
        CargoServicePointGenericIndustryCoal,
        CargoServicePointGenericIndustryFood,
        CargoServicePointGenericIndustryLumber,

        // Non goods transfer settings
        Debug,
    }

    public static class SettingIdExtensions {
        /// <summary>
        ///     Cached lookup of the <see cref="SettingId" /> values. Do not mutate.
        /// </summary>
        public static readonly SettingId[] Values = Enum
            .GetValues(typeof(SettingId))
            .Cast<SettingId>()
            .ToArray();

        private static readonly Dictionary<SettingId, string> _settingNameMap = Values
            .ToDictionary(id => id, id => id.ToString("G"));

        /// <summary>
        ///     Produces the same result as <see cref="Enum.ToString()" /> with the <c>G</c> format, but is much faster.
        /// </summary>
        /// <param name="settingId">The ID to stringify.</param>
        /// <returns>The name of the setting ID.</returns>
        public static string ToStringOptimized(this SettingId settingId) => _settingNameMap[settingId];
    }
}
