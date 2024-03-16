using Microsoft.AspNetCore.Components.Forms;

namespace Rent2Own_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);

    }
}
