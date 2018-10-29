using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColossalFramework;
using UnityEngine;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        // Commercial
        public static readonly GoodsTransfer CommercialGoods =
            new GoodsTransfer(Service.Commercial, TransferReason.Goods); // any SubService

        public static readonly GoodsTransfer CommercialLuxuryProducts =
            new GoodsTransfer(Service.Commercial, TransferReason.LuxuryProducts); // any SubService

        // Specialized industry
        public static readonly GoodsTransfer SpecializedOil =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOil, TransferReason.Oil);

        public static readonly GoodsTransfer SpecializedOre =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOre, TransferReason.Ore);

        public static readonly GoodsTransfer SpecializedGrain =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialFarming, TransferReason.Grain);

        public static readonly GoodsTransfer SpecializedLogs =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialForestry, TransferReason.Logs);

        // Generic industry
        public static readonly GoodsTransfer GenericPetrol =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Petrol);

        public static readonly GoodsTransfer GenericCoal =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Coal);

        public static readonly GoodsTransfer GenericFood =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Food);

        public static readonly GoodsTransfer GenericLumber =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Lumber);

        // Shelters
        public static readonly GoodsTransfer ShelterGoods =
            new GoodsTransfer(Service.Disaster, SubService.None, TransferReason.Goods);

        // Industries DLC raw
        public static readonly GoodsTransfer PlayerIndustryRawOil =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryOil, TransferReason.Oil);

        public static readonly GoodsTransfer PlayerIndustryRawOre =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryOre, TransferReason.Ore);

        public static readonly GoodsTransfer PlayerIndustryRawFarming =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryFarming, TransferReason.Grain);

        public static readonly GoodsTransfer PlayerIndustryRawForestry =
            new GoodsTransfer(Service.PlayerIndustry, SubService.PlayerIndustryForestry, TransferReason.Logs);

        // Industries DLC processed
        public static readonly GoodsTransfer ProcessedAnimalProducts =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.AnimalProducts);

        public static readonly GoodsTransfer ProcessedFlours =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Flours);

        public static readonly GoodsTransfer ProcessedPaper =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Paper);

        public static readonly GoodsTransfer ProcessedPlanedTimber =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.PlanedTimber);

        public static readonly GoodsTransfer ProcessedPetroleum =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Petroleum);

        public static readonly GoodsTransfer ProcessedPlastics =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Plastics);

        public static readonly GoodsTransfer ProcessedGlass =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Glass);

        public static readonly GoodsTransfer ProcessedMetals =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Metals);

        public static readonly GoodsTransfer ProcessedCrops =
            new GoodsTransfer(Service.PlayerIndustry, SubService.None, TransferReason.Grain, "PlayerIndustryCrops");


        public static readonly IList<GoodsTransfer> GoodsTransfers = new ReadOnlyCollection<GoodsTransfer>(
            new List<GoodsTransfer> {
                CommercialGoods, CommercialLuxuryProducts,
                SpecializedOil, SpecializedOre, SpecializedGrain, SpecializedLogs,
                GenericPetrol, GenericCoal, GenericFood, GenericLumber,
                ShelterGoods,
                PlayerIndustryRawOil, PlayerIndustryRawOre, PlayerIndustryRawFarming, PlayerIndustryRawForestry,
                ProcessedAnimalProducts, ProcessedFlours, ProcessedPaper, ProcessedPlanedTimber,
                ProcessedPetroleum, ProcessedPlastics, ProcessedGlass, ProcessedMetals, ProcessedCrops
            });


        /// <summary>
        /// The default amount of goods to transfer each time. 
        /// A relatively arbitrary number.
        /// </summary>
        private const int DefaultTransferAmount = 100000;

        public Service Service { get; }
        public SubService SubService { get; } // None == any
        public bool AnySubService { get; }
        public TransferReason Material { get; }
        public int TransferAmount { get; }
        public string Id { get; }

        private readonly BuildingManager buildingManager;

        internal GoodsTransfer(Service service, SubService subService, TransferReason material, string id = null,
            int transferAmount = DefaultTransferAmount, bool anySubService = false) {
            this.Service = service;
            this.SubService = subService;
            this.Material = material;
            this.TransferAmount = transferAmount;
            this.Id = id ?? GenerateId();
            this.buildingManager = Singleton<BuildingManager>.instance;
            this.AnySubService = anySubService;
        }

        internal GoodsTransfer(Service service, TransferReason material, string id = null, int transferAmount = DefaultTransferAmount) :
            this(service, SubService.None, material, id, transferAmount, true) { }

        public bool Matches(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null)
                return false;
            return Service.Equals(info.GetService()) &&
                   (AnySubService || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            BuildingAI ai = building.Info?.GetAI() as BuildingAI;
            if (ai == null)
                return;

            int amount = TransferAmount;
            ai.ModifyMaterialBuffer(buildingId, ref buildingManager.m_buildings.m_buffer[buildingId],
                Material, ref amount);

            if (ModIdentity.DebugMode)
                Debug.Log(
                    $"{Material} ({amount}) => \"{building.Info.name}\" {building.Info.GetService()}->{building.Info.GetSubService()} ({ai.GetType()})");
        }

        public void TransferIfMatch(ushort buildingId) {
            if (Matches(buildingId))
                Transfer(buildingId);
        }

        private string GenerateId() {
            switch (Service) {
                case Service.Commercial:
                case Service.PlayerIndustry:
                    return Enum.GetName(typeof(Service), Service) + MaterialToString();
                case Service.Industrial:
                    bool generic = SubService.Equals(SubService.IndustrialGeneric);
                    return (generic ? "Generic" : "Specialized") + MaterialToString();
                case Service.Disaster:
                    return "ShelterGoods";
            }

            throw new Exception("No valid ID for the GoodsTransfer.");
        }

        private string MaterialToString() {
            return Enum.GetName(typeof(TransferReason), Material);
        }
    }
}