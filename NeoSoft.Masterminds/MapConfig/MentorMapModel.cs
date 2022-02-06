using Microsoft.AspNetCore.Http;
using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Models.Outcoming;
using System.Linq;

namespace NeoSoft.Masterminds.MapConfig
{
    public static class MentorMapModel
    {
        public static MentorView Map(MentorModel mentorModel, HttpRequest request)
        {
            return new MentorView
            {
                Id = mentorModel.Id,
                FirstName = mentorModel.FirstName,
                LastName = mentorModel.LastName,
                Description = mentorModel.Description,
                HourlyRate = mentorModel.HourlyRate,
                Rating = mentorModel.Rating,
                Professions = mentorModel.Professions,
                ProfessionalAspects = mentorModel.ProfessionalAspects,
                ReviewsTotalCount = mentorModel.ReviewsTotalCount,
                ProfilePhoto = FileCommon.GetPhotoPath(mentorModel.ProfilePhotoId, request),
                Reviews = mentorModel.Reviews.Select(x => new ReviewView
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Rating = x.Rating,
                    Text = x.Text,
                    ProfilePhoto = FileCommon.GetPhotoPath(x.ProfilePhotoId, request)
                }).ToList()
            };

        }
    }
}
