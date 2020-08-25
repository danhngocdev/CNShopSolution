﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.Common
{
    public class FileStorageService : IStorageService
    {

        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);

        }
        public async Task DeleleFileAsync(string filename)
        {
            var filePath = Path.Combine(_userContentFolder, filename);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string filename)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{filename}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string filename)
        {
            var filePath = Path.Combine(_userContentFolder, filename);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}