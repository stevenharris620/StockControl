namespace StockControl.API.Services.Utilities;

public interface IImageService
{
    byte[] ConvertImageToByteArray(IFormFile image);
    string? ConvertByteArrayToChar64(byte[] array);
}
public class ImageService : IImageService
{
    public byte[] ConvertImageToByteArray(IFormFile image)
    {
        using var target = new MemoryStream();
        image.CopyTo(target);
        return target.ToArray();
    }

    public string? ConvertByteArrayToChar64(byte[] array)
    {
        return array == null
            ? ""
            : "data:Image/png;base64," + Convert.ToBase64String(array);
    }
}