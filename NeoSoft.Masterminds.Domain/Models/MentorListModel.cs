using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Models
{
    public class MentorListModel
    {
        public int Id { get; set; }
        public int ProfilePhotoId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Rating { get; set; }
        public IList<ProfessionsModel> Professions { get; set; }
       
        

    }

}
