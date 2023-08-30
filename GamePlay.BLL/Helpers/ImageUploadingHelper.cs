using System.Text;
using Microsoft.AspNetCore.Http;

namespace GamePlay.BLL.Helpers;

public static class ImageUploadingHelper
{
    public static async Task<string> ReuploadAndGetNewPathAsync(string directory, string defaultPath, IFormFile? image,
        string previousPath)
    {
        if (image == null) return previousPath;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "../GamePlay.UI/game-play-ui/public" + previousPath);
        if (File.Exists(path) && !previousPath.Equals(defaultPath)) File.Delete(path);

        return await UploadImageAsync(directory, defaultPath, image);
    }

    public static async Task<string> UploadImageAsync(string directory, string defaultPath, IFormFile? image)
    {
        if (image == null) return defaultPath;

        var name = GenerateCode() + Path.GetExtension(image.FileName);
        await using var fileStream =
            new FileStream(Path.Combine(Directory.GetCurrentDirectory(), $"../GamePlay.UI/game-play-ui/public/{directory}/{name}"),
                FileMode.Create);
        await image.CopyToAsync(fileStream);

        return $"/{directory}/{name}";
    }

    private static string GenerateCode()
    {
        var builder = new StringBuilder(12);
        var random = new Random();
        for (var i = 0; i < 12; i++) builder.Append(random.Next(10));

        return builder.ToString();
    }
}