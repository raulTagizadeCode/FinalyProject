using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace Project.BL.Utilites;

public static class FileExtension
{
    public static async Task<string> SaveAsync(this IFormFile file, string folder)
    {
        string uploadPath = Path.Combine(Path.GetFullPath("wwwroot"), "uploads", folder);

        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        string filename = Guid.NewGuid().ToString() + file.FileName;

        using (FileStream fs = new(Path.Combine(uploadPath, filename), FileMode.Create))
        {
            await file.CopyToAsync(fs);
        }

        return filename;
    }
 public static bool CheckType(this IFormFile file, string requiredType) => file.ContentType.Contains(requiredType);
}
