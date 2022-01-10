using StockControl.API.Models;
using StockControl.API.Services.Utilities;
using StockControl.Shared.Requests;

namespace StockControl.API.Mappers
{
    public interface IPartsMapper
    {
        PartDetail Map_Part_To_PartDetail(Part part, PartDetail partDetail);
        Part Map_PartDetail_To_Part(PartDetail partDetail, Part part);
    }

    public class PartsMapper : IPartsMapper
    {
        private readonly IImageService _imageService;

        public PartsMapper(IImageService imageService)
        {
            _imageService = imageService;
        }
        public PartDetail Map_Part_To_PartDetail(Part part, PartDetail partDetail)
        {
            partDetail.Id = part.Id;
            partDetail.Name = part.Name;
            partDetail.PartCode = part.PartCode;
            partDetail.Description = part.Description;
            partDetail.Cost = part.Cost;
            partDetail.UnitType = part.UnitType;
            partDetail.StockLevel = part.StockLevel;
            partDetail.ReorderLevel = part.ReorderLevel;
            partDetail.SupplierId = part.SupplierId;

            //partDetail.ImageChar64 =
            //    part.Image == null ? String.Empty : _imageService.ConvertByteArrayToChar64(part.Image);

            return partDetail;
        }

        public Part Map_PartDetail_To_Part(PartDetail partDetail, Part part)
        {
            if (!string.IsNullOrEmpty(partDetail.Id)) part.Id = partDetail.Id;

            part.Name = partDetail.Name;
            part.PartCode = partDetail.PartCode;
            part.Description = partDetail.Description;
            part.Cost = partDetail.Cost;
            part.UnitType = partDetail.UnitType;
            part.StockLevel = partDetail.StockLevel;
            part.ReorderLevel = partDetail.ReorderLevel;
            part.SupplierId = partDetail.SupplierId;

            //if (partDetail.Image != null)
            //    part.Image = _imageService.ConvertImageToByteArray(partDetail.Image);

            return part;
        }
    }
}
