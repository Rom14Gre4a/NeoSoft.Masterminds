using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public static class Helper
    {
       
        public static List<ProfessionsModel> MyConvertor(List<ProfessionEntity> source)
        {
            List<ProfessionsModel> dest = new List<ProfessionsModel>();
            foreach (var sourceItem in source)
            {
                dest.Add(new ProfessionsModel
                {
                    Id = sourceItem.Id,
                    Name = sourceItem.Name
                });
            }
            return dest;
        }
 
    }
}
