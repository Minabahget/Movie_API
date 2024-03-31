using Microsoft.AspNetCore.Mvc;
using Movie_Api.DTO;

namespace Movie_Api.Utilities
{
    public static class Utility
    {


        public static string DeleteImage(string imagePath)
        {
            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                    return $"Image {fileName} deleted successfully";
                }
                else
                {
                    return $"Image {fileName} not found";
                }
            }
            catch (Exception ex)
            {
                return $"Internal server error, {ex.Message}";
            }

        }
        public static async Task<string> AddImage([FromForm] MovieCreateModel movieCreate)
        {
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + movieCreate.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await movieCreate.Image.CopyToAsync(fileStream);
                }
                string path = Path.Combine("uploads", uniqueFileName);
                return (path);
            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex.Message}";
            }

        }

    }
}
