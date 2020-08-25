using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.Common
{
   public interface IStorageService
    {
        string GetFileUrl(string filename);
        Task SaveFileAsync(Stream mediaBinaryStream, string filename);
        Task DeleleFileAsync(string filename);
    }
}
