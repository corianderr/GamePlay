using System.Text;
using Microsoft.AspNetCore.Http;

namespace GamePlay.BLL.Helpers;

public static class ImageUploadingHelper {
    public static async Task ReuploadAsync(string directory, string defaultPath, IFormFile image,
        string previousPath) {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "../GamePlay.UI/game-play-ui/public" + previousPath);
        if (File.Exists(path) && !previousPath.Equals(defaultPath)) File.Delete(path);
        var name = GenerateCode() + Path.GetExtension(image.FileName);
        var photoPath = $"{directory}/{name}";

        if (photoPath.Equals(defaultPath)) {
            return;
        }

        await UploadAsync(image, photoPath);
    }

    public static async Task UploadAsync(IFormFile image, string photoPath) {
        await using var fileStream =
            new FileStream(Path.Combine(Directory.GetCurrentDirectory(), $"../GamePlay.UI/game-play-ui/public/{photoPath}"),
                FileMode.Create);
        await image.CopyToAsync(fileStream);
    }

    public static string GeneratePhotoPath(string directory, IFormFile? file, string defaultPath) {
        return file == null ? defaultPath : $"/{directory}/{GenerateCode() + Path.GetExtension(file.FileName)}";
    }

    private static string GenerateCode() {
        var builder = new StringBuilder(12);
        var random = new Random();
        for (var i = 0; i < 12; i++) builder.Append(random.Next(10));

        return builder.ToString();
    }
}