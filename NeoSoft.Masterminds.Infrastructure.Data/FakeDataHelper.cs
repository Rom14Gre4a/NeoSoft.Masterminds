using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class FakeDataHelper

    {
        private static List<string> FakeProfessional = new List<string>()
        {
            "Focused",
            "Poised",
            "Respectful of others",
            "Strong communicator",
            "Effective time management",
            "Organized",
            "physical health",
            "mental health",
            "Positive thinking"
        };

        private static List<string> FakeProfessions = new List<string>()
        {
            "Psychologist",
            "Designer",
            "Marketeer",
            "Financier",
            "Economist",
            "Engeneer",
            "Developer",
            "Human Resource manager",
            "software tester",
            "Architect",
            "Analytics",
            "Pharmacist",
            "Producer",
        };
        private static readonly string[] ExistingImages = new[]
        {
            "53387f51-b2dc-4dea-8b79-74273a8145ce",
            "12b0ef8e-59bd-4334-8aa6-01af5dc065fd",
            "3d19de4c-20c0-418f-9329-0c03365a2f77",
            "5fd7e89c-319d-492b-848c-f5ca89f114a1",
            "9387a87f-9c25-4467-aa8b-ba470976fde5",
            "b345f041-d817-44b3-b5cb-4605be6f1987",
            "c289844f-a838-4c9d-a03f-5354f0a6b070",
            "e8b56e48-ac8b-4113-9372-18e5598ba662",
        };

        private readonly MastermindsDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly Faker _faker;
        private List<ReviewEntity> _reviews { get; set; }
        private List<ProfileEntity> _registredUsers { get; set; }
        private List<MentorEntity> _mentors { get; set; }

        private List<FavoritesEntity> _favorites { get; set; }

        public FakeDataHelper(MastermindsDbContext contenxt, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = contenxt;
            _registredUsers = new List<ProfileEntity>();
            _mentors = new List<MentorEntity>();
            _reviews = new List<ReviewEntity>();
            _favorites = new List<FavoritesEntity>();
           _faker = new Faker();
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task SeedFakeData()//UserManager<AppUser> userManager, RoleManager<AppRole> roleManager
        {
            var anyMentors = await _context.Mentors.AnyAsync();

            if (anyMentors)
            {
                return;
            }

            int mentorCount = 50;
            var fakeProfessions = GenerateProfessions();
            var fakeProfessional = GenerateProfessionalAspect();

            await _context.AddRangeAsync(fakeProfessions);
            await _context.AddRangeAsync(fakeProfessional);
            await _context.SaveChangesAsync();

            await GenerateMentors(fakeProfessional, fakeProfessions, mentorCount);

            await _context.SaveChangesAsync();

            await CreateProfileForUsersAsync(mentorCount);

            await _context.SaveChangesAsync();

            await GenerateReviewers(mentorCount);

           await GenerateFavorites(mentorCount);

            await CreateIdentityForMentors();

            await CreateIdentityForUsers();

            await CreateRolesIdentity();

            await CreateRoleForMentorsAsync();

            await CreatedRoleForUsersAsync();

            await _context.SaveChangesAsync();
        }

        private async Task CreateRolesIdentity()
        {
            var userRole = new AppRole { Name = "User" };
            var managerRole = new AppRole { Name = "Mentor" };

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(managerRole);

        }
        private async Task CreateIdentityForUsers()
        {
            foreach (var user in _registredUsers)
            {
                var faker = new Faker();

                await _userManager.CreateAsync(new AppUser
                {
                    Email = faker.Person.Email,
                    UserName = faker.Person.Email,
                    Profile = user
                },
                  "Asd123!1");
            }
        }
        private async Task CreateIdentityForMentors()
        {
            foreach (var mentor in _mentors)
            {
                var faker = new Faker();

                await _userManager.CreateAsync(new AppUser
                {
                    Email = faker.Person.Email,
                    UserName = faker.Person.Email,
                    Profile = mentor.Profile
                },
                  "Asd123!1");
            }
        }
        private async Task CreatedRoleForUsersAsync()
        {
            foreach (var user in _registredUsers)
            {
                var appUser = await _userManager.FindByIdAsync(user.Id.ToString());

                await _userManager.AddToRoleAsync(appUser, "User");
            }
        }
        private async Task CreateRoleForMentorsAsync()
        {
            foreach (var mentor in _mentors)
            {
                var appUser = await _userManager.FindByIdAsync(mentor.Id.ToString());
                await _userManager.AddToRoleAsync(appUser, "Mentor");
            }
        }

        private static FileEntity GetProfilePhoto(int index)
        {
            if (index > 7)
                return null;

            return new FileEntity
            {
                InitialName = $"ProfilePhoto_{index}",
                Name = ExistingImages[index],
                ContentType = index == 4 ? "image/jpeg" : "image/png",
                Extension = index == 4 ? "jpg" : "png",
                FileType = FileType.ProfilePhoto
            };

        }

        private async Task GenerateFavorites(int mentorCount)
        {
            for (int i = 0; i < mentorCount; i++)
            {
                FavoritesEntity favorite = new FavoritesEntity
                {

                    ProfileId = _faker.PickRandom(_registredUsers.Select(m => m.Id)),
                    MentorId = _faker.PickRandom(_mentors.Select(m => m.Id)),
                   
                };
                _favorites.Add(favorite);
            }
            await _context.AddRangeAsync(_favorites);
        }
        private async Task GenerateMentors(List<ProfessionalAspectEntity> profAsp, List<ProfessionEntity> profi, int mentorCount)
        {
            for (int i = 0; i < mentorCount; i++)
            {
                var mentor = new MentorEntity()
                {
                    HourlyRate = _faker.Random.Int(5, 50),
                    Description = _faker.Lorem.Text(),
                    ProfessionalAspects = profAsp.Skip(3).Take(4).ToList(),
                    Professions = profi.Skip(3).Take(4).ToList(),
                    Profile = new ProfileEntity
                    {
                        ProfileFirstName = _faker.Name.FirstName(),
                        ProfileLastName = _faker.Name.LastName(),
                        Photo = GetProfilePhoto(_faker.Random.Int(0, ExistingImages.Length - 1)),                     
                    }

                };

                _mentors.Add(mentor);
            }

            await _context.AddRangeAsync(_mentors);
        }
        private async Task GenerateReviewers(int mentorCount)
        {
            for (int i = 0; i < mentorCount; i++)
            {
                var review = new ReviewEntity
                {
                    ReviewDate = _faker.Date.Past(10),
                    Text = _faker.Lorem.Text(),
                    Rating = _faker.Random.Double(1, 5),
                    FromProfileId = _faker.PickRandom(_registredUsers.Select(u => u.Id)),
                    ToProfileId = _faker.PickRandom(_mentors.Select(m => m.Id))
                };

                _reviews.Add(review);
            }

            await _context.AddRangeAsync(_reviews);
        }
        private async Task CreateProfileForUsersAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ProfileEntity profile = new ProfileEntity
                {
                    ProfileFirstName = _faker.Name.FirstName(),
                    ProfileLastName = _faker.Name.LastName(),
                    Photo = GetProfilePhoto(_faker.Random.Int(1, 100))
                };
                _registredUsers.Add(profile);
            }

            await _context.AddRangeAsync(_registredUsers);
        }
        private static List<ProfessionalAspectEntity> GenerateProfessionalAspect()
        {
            List<ProfessionalAspectEntity> professionalAsp = new List<ProfessionalAspectEntity>();

            foreach (var professionAsp in FakeProfessional)
            {
                professionalAsp.Add(new ProfessionalAspectEntity
                {
                    Aspect = professionAsp
                });
            }
            return professionalAsp;

        }
        private static List<ProfessionEntity> GenerateProfessions()
        {
            List<ProfessionEntity> professions = new List<ProfessionEntity>();

            foreach (var profession in FakeProfessions)
            {
                professions.Add(new ProfessionEntity
                {
                    Name = profession
                });
            }
            return professions;
        }

    }
}
