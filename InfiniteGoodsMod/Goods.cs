using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColossalFramework;
using static ItemClass;
using static TransferManager;

namespace InfiniteGoodsMod {
    public class GoodsTransfer {
        public static readonly GoodsTransfer CommercialGoods =
            new GoodsTransfer(Service.Commercial, SubService.None, TransferReason.Goods);

        public static readonly GoodsTransfer CommercialLuxuryProducts =
            new GoodsTransfer(Service.Commercial, SubService.None, TransferReason.LuxuryProducts);

        public static readonly GoodsTransfer SpecializedOil =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOil, TransferReason.Oil);

        public static readonly GoodsTransfer SpecializedOre =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialOre, TransferReason.Ore);

        public static readonly GoodsTransfer SpecializedGrain =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialFarming, TransferReason.Grain);

        public static readonly GoodsTransfer SpecializedLogs =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialForestry, TransferReason.Logs);

        public static readonly GoodsTransfer GenericPetrol =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Petrol);

        public static readonly GoodsTransfer GenericCoal =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Coal);

        public static readonly GoodsTransfer GenericFood =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Food);

        public static readonly GoodsTransfer GenericLumber =
            new GoodsTransfer(Service.Industrial, SubService.IndustrialGeneric, TransferReason.Lumber);

        public static readonly GoodsTransfer ShelterGoods =
            new GoodsTransfer(Service.Disaster, SubService.None, TransferReason.Goods); // TODO test

        public static readonly IList<GoodsTransfer> GoodsTransfers = new ReadOnlyCollection<GoodsTransfer>(
            new List<GoodsTransfer> {
                CommercialGoods, CommercialLuxuryProducts,
                SpecializedOil, SpecializedOre, SpecializedGrain, SpecializedLogs,
                GenericPetrol, GenericCoal, GenericFood, GenericLumber,
                ShelterGoods
            });

        /// <summary>
        /// The default amount of goods to transfer each time. 
        /// A relatively arbitrary number.
        /// </summary>
        private const int DefaultTransferAmount = 100000;

        public Service Service { get; }
        public SubService SubService { get; } // None == any
        public TransferReason Reason { get; }
        public string Id { get; }

        private readonly BuildingManager buildingManager;

        internal GoodsTransfer(Service service, SubService subService, TransferReason reason) {
            this.Service = service;
            this.SubService = subService;
            this.Reason = reason;
            this.Id = GenerateId();
            this.buildingManager = Singleton<BuildingManager>.instance;
        }

        public bool Matches(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            var info = building.Info;
            if (info == null)
                return false;
            return Service.Equals(info.GetService()) &&
                   (SubService.Equals(SubService.None) || SubService.Equals(info.GetSubService()));
        }

        public void Transfer(ushort buildingId) {
            Building building = buildingManager.m_buildings.m_buffer[buildingId];
            BuildingAI ai = building.Info?.GetAI() as BuildingAI;
            if (ai == null)
                return;
            int amount = DefaultTransferAmount;
            ai.ModifyMaterialBuffer(buildingId, ref buildingManager.m_buildings.m_buffer[buildingId], Reason, ref amount);
            Utils.Log(
                $"{Reason} => {building.Info.GetService()} {building.Info.GetSubService()} ({ai.GetType()})");
        }

        public void TransferIfMatch(ushort buildingId) {
            if (Matches(buildingId))
                Transfer(buildingId);
        }

        private string GenerateId() {
            switch (Service) {
                case Service.Commercial:
                    return "Commercial" + ReasonToString();
                case Service.Industrial:
                    bool generic = SubService.Equals(SubService.IndustrialGeneric);
                    return (generic ? "Generic" : "Specialized") + ReasonToString();
                case Service.Disaster:
                    return "ShelterGoods";
            }

            throw new Exception("No valid ID for the GoodsTransfer.");
        }

        private string ReasonToString() {
            return Enum.GetName(typeof(TransferReason), Reason);
        }
    }
}