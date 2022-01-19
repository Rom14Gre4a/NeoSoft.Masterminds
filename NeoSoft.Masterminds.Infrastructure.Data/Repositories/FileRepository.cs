﻿using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly MastermindsDbContext _dbContext;

        public FileRepository(MastermindsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<FileEntity> Get(int fileId)
        {
            var file = await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == fileId);
            return file;
        }
        public async Task<int> Save(FileEntity file)
        {
            await _dbContext.Files.AddAsync(file);
            await _dbContext.SaveChangesAsync();

            return file.Id;
        }
    }
}
