using System;
using System.Collections.Generic;
using ColossalFramework;
using UnityEngine;
using static ItemClass;
using static TransferManager;
using static InfiniteGoodsMod.GoodsTransfer.TransferCategory;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        public enum TransferCategory {
            Commercial,
            SpecializedIndustry,
            GenericIndustry,
            Shelter,
            PloppedIndustry,
            UniqueIndustry
        }

        public static readonly IList<GoodsTransfer> GoodsTransfers = new List<GoodsTransfer>();

        // Commercial
        public static readonly GoodsTransfer CommercialGoods =
            new GoodsTransfer(Commercial, Service.Commercial, TransferReason.Goods); // any SubService

        public static readonly GoodsTransfer CommercialLuxuryProducts =
            new GoodsTransfer(Commercial, Service.Commercial, TransferReason.LuxuryProducts); // any SubService

        // Specialized industry
        public static readonly GoodsTransfer SpecializedOil =
            new GoodsTransfer(SpecializedIndustry, Service.Industrial, SubService.IndustrialOil,
                TransferReason.Oil);

        public static readonly GoodsTransfer SpecializedOre =
            new GoodsTransfer(SpecializedIndustry, Service.Industrial, SubService.IndustrialOre,
                TransferReason.Ore);

        public static readonly GoodsTransfer SpecializedGrain =
            new GoodsTransfer(SpecializedIndustry, Service.Industrial, SubService.IndustrialFarming,
                TransferReason.Grain);

        public static readonly GoodsTransfer SpecializedLogs =
            new GoodsTransfer(SpecializedIndustry, Service.Industrial, SubService.IndustrialForestry,
                TransferReason.Logs);

        // Generic industry
        public static readonly GoodsTransfer GenericPetrol =
            new GoodsTransfer(GenericIndustry, Service.Industrial, SubService.IndustrialGeneric,
                TransferReason.Petrol);

        public static readonly GoodsTransfer GenericCoal =
            new GoodsTransfer(GenericIndustry, Service.Industrial, SubService.IndustrialGeneric,
                TransferReason.Coal);

        public static readonly GoodsTransfer GenericFood =
            new GoodsTransfer(GenericIndustry, Service.Industrial, SubService.IndustrialGeneric,
                TransferReason.Food);

        public static readonly GoodsTransfer GenericLumber =
            new GoodsTransfer(GenericIndustry, Service.Industrial, SubService.IndustrialGeneric,
                TransferReason.Lumber);

        // Shelters
        public static readonly GoodsTransfer ShelterGoods =
            new GoodsTransfer(Shelter, Service.Disaster, SubService.None, TransferReason.Goods);

        // Industries DLC raw
        public static readonly GoodsTransfer PloppedIndustryRawOil =
            new GoodsTransfer(PloppedIndustry, Service.PlayerIndustry, SubService.PlayerIndustryOil,
                TransferReason.Oil);

        public static readonly GoodsTransfer PloppedIndustryRawOre =
            new GoodsTransfer(PloppedIndustry, Service.PlayerIndustry, SubService.PlayerIndustryOre,
                TransferReason.Ore);

        public static readonly GoodsTransfer PloppedIndustryRawFarming =
            new GoodsTransfer(PloppedIndustry, Service.PlayerIndustry, SubService.PlayerIndustryFarming,
                TransferReason.Grain);

        public static readonly GoodsTransfer PloppedIndustryRawForestry =
            new GoodsTransfer(PloppedIndustry, Service.PlayerIndustry, SubService.PlayerIndustryForestry,
                TransferReason.Logs);

        // Industries DLC processed
        public static readonly GoodsTransfer ProcessedAnimalProducts =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.AnimalProducts);

        public static readonly GoodsTransfer ProcessedFlours =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Flours);

        public static readonly GoodsTransfer ProcessedPaper =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Paper);

        public static readonly GoodsTransfer ProcessedPlanedTimber =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.PlanedTimber);

        public static readonly GoodsTransfer ProcessedPetroleum =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Petroleum);

        public static readonly GoodsTransfer ProcessedPlastics =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Plastics);

        public static readonly GoodsTransfer ProcessedGlass =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Glass);

        public static readonly GoodsTransfer ProcessedMetals =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Metals);

        public static readonly GoodsTransfer ProcessedCrops =
            new GoodsTransfer(UniqueIndustry, Service.PlayerIndustry, SubService.None, TransferReason.Grain);

        private static readonly BuildingManager BuildingManager = Singleton<BuildingManager>.instance;

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
        public TransferCategory Category { get; }
        public string Id { get; }

        internal GoodsTransfer(TransferCategory category, Service service, SubService subService,
            TransferReason material,
            int transferAmount = DefaultTransferAmount, bool anySubService = false) {

            this.Service = service;
            this.SubService = subService;
            this.Material = material;
            this.TransferAmount = transferAmount;
            this.Category = category;
            this.AnySubService = anySubService;

            this.Id = Enum.GetName(typeof(TransferCategory), category) + Enum.GetName(typeof(TransferReason), material);

            GoodsTransfers.Add(this);
            Debug.Log($"{category} {service} {subService}");
        }

        internal GoodsTransfer(TransferCategory category, Service service, TransferReason material,
            int transferAmount = DefaultTransferAmount) :
            this(category, service, SubService.None, material, transferAmount, true) { }

        public bool Matches(ushort buildingId) {
            Building building = BuildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null)
                return false;
            return Service.Equals(info.GetService()) &&
                   (AnySubService || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId) {
            Building building = BuildingManager.m_buildings.m_buffer[buildingId];
            BuildingAI ai = building.Info?.GetAI() as BuildingAI;
            if (ai == null)
                return;

            int amount = TransferAmount;
            ai.ModifyMaterialBuffer(buildingId, ref BuildingManager.m_buildings.m_buffer[buildingId],
                Material, ref amount);

            if (ModIdentity.DebugMode)
                Debug.Log(
                    $"{Material} ({amount}) => \"{building.Info.name}\" {building.Info.GetService()}->{building.Info.GetSubService()} ({ai.GetType()})");
        }

        public void TransferIfMatch(ushort buildingId) {
            if (Matches(buildingId))
                Transfer(buildingId);
        }
    }
}