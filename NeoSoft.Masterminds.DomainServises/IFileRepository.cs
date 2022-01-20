using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IFileRepository
    {
        Task<FileEntity> GetAsync(int fileId);

        Task<int> SaveAsync(FileEntity file);

        Task<FileEntity> GetAsync(string fileName);
    }
}
