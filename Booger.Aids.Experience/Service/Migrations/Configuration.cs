namespace Service.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Infrastructure.ExperienceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Service.Infrastructure.ExperienceContext context)
        {
            //  This method will be called after migrating to the latest version.

            SeedExperience(context);
            SeedEducation(context);
            SeedSkill(context);
            SeedExperienceSkill(context);
        }

        private static void SeedExperience(Service.Infrastructure.ExperienceContext context)
        {
            context.Experiences.AddOrUpdate([
                new()
                {
                    Id = 0,
                    Title = "Cloud Research & Development Mentorship",
                    Organization = "Renaissance Computinng Institute",
                    StartDate = new DateOnly(2019, 9, 1),
                    EndDate = new DateOnly(2020, 2, 1),
                    Location = "Chapel Hill, NC, USA",
                    Description = "Mentorship in Computer Science via NC School of Science and Mathematics"
                },
                new()
                {
                    Id = 1,
                    Title = "Cloud Research & Development Intern",
                    Organization = "Renaissance Computing Institute",
                    StartDate = new DateOnly(2020, 11, 1),
                    EndDate = new DateOnly(2023, 12, 1),
                    Location = "Remote",
                    Description = "QA and automation work on FABRIC Testbed"
                },
                new()
                {
                    Id = 2,
                    Title = "Research Intern",
                    Organization = "NC State University",
                    StartDate = new DateOnly(2022, 5, 1),
                    EndDate = new DateOnly(2023, 12, 1),
                    Location = "Raleigh, NC, USA",
                    Description = "Research development on layered graph verticality and dominating set problems"
                },
                new()
                {
                    Id = 3,
                    Title = "Senior Design",
                    Organization = "NC State University",
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2025, 5, 31),
                    Location = "Raleigh, NC, USA",
                    Description = "Project lead for start-up advertising platform"
                },
                new()
                {
                    Id = 4,
                    Title = "Software Quality Engineer II (Contract)",
                    Organization = "Becton-Dickinson",
                    StartDate = new DateOnly(2024, 12, 1),
                    Location = "Durham, NC, USA",
                    Description = "Software and QA engineering on pharmaceutical interops microservices"
                }
            ]);
        }
    
        private static void SeedEducation(Service.Infrastructure.ExperienceContext context)
        {
            context.Educations.AddOrUpdate([
                new()
                {
                    Id = 0,
                    Title = "High School Diploma",
                    Organization = "North Carolina School of Science and Mathematics",
                    StartDate = new DateOnly(2018, 8, 1),
                    EndDate = new DateOnly(2020, 5, 31),
                    Location = "Durham, NC, USA",
                    Description = "Tech magnet for exceptional students in NC"
                },
                new()
                {
                    Id = 1,
                    Title = "Bachelors of Science",
                    Organization = "North Carolina State University",
                    StartDate = new DateOnly(2020, 8, 1),
                    EndDate = new DateOnly(2024, 5, 31),
                    Location = "Raleigh, NC, USA",
                    Description = "Undergraduate in Computer Science with Minor in Mathematics",
                    Field = "Computer Science"
                }
            ]);
        }

        private static void SeedSkill(Service.Infrastructure.ExperienceContext context)
        {
            context.Skills.AddOrUpdate([
                new()
                {
                    Id = 0,
                    Name = "HTML"
                },
                new()
                {
                    Id = 1,
                    Name = "CSS"
                },
                new()
                {
                    Id = 2,
                    Name = "JavaScript"
                },
                new()
                {
                    Id = 3,
                    Name = "Python"
                },
                new()
                {
                    Id = 4,
                    Name = "Java"
                },
                new()
                {
                    Id = 5,
                    Name = "Eclipse"
                },
                new()
                {
                    Id = 6,
                    Name = "C"
                },
                new()
                {
                    Id = 7,
                    Name = "Make"
                },
                new()
                {
                    Id = 8,
                    Name = "C++"
                },
                new()
                {
                    Id = 9,
                    Name = "MATLAB"
                },
                new()
                {
                    Id = 10,
                    Name = "Maven"
                },
                new()
                {
                    Id = 11,
                    Name = "SQL"
                },
                new()
                {
                    Id = 12,
                    Name = "MySQL"
                },
                new()
                {
                    Id = 13,
                    Name = "Docker"
                },
                new()
                {
                    Id = 14,
                    Name = "Apache"
                },
                new()
                {
                    Id = 15,
                    Name = "Hibernate"
                },
                new()
                {
                    Id = 16,
                    Name = "Jakarta"
                },
                new()
                {
                    Id = 17,
                    Name = "AngularJS"
                },
                new()
                {
                    Id = 18,
                    Name = "CMake"
                },
                new()
                {
                    Id = 19,
                    Name = "SQLAlchemy"
                },
                new()
                {
                    Id = 20,
                    Name = "FastAPI"
                },
                new()
                {
                    Id = 21,
                    Name = "Pydantic"
                },
                new()
                {
                    Id = 22,
                    Name = "PostgreSQL"
                },
                new()
                {
                    Id = 23,
                    Name = "C#"
                },
                new()
                {
                    Id = 24,
                    Name = "Microsoft Entity Framework Core"
                },
                new()
                {
                    Id = 25,
                    Name = "Auth0"
                },
                new()
                {
                    Id = 26,
                    Name = "Microsoft Azure"
                },
                new()
                {
                    Id = 27,
                    Name = "IntelliJ IDEA"
                },
                new()
                {
                    Id = 28,
                    Name = "Kafka"
                },
                new()
                {
                    Id = 29,
                    Name = "Amazon Web Services"
                }
            ]);
        }

        private static void SeedExperienceSkill(Service.Infrastructure.ExperienceContext context)
        {
            context.ExperienceSkills.AddOrUpdate([
                new()
                {
                    ExperienceId = 0,
                    SkillId = 4
                },
                new()
                {
                    ExperienceId = 1,
                    SkillId = 3
                },
                new()
                {
                    ExperienceId = 2,
                    SkillId = 6
                },
                new()
                {
                    ExperienceId = 2,
                    SkillId = 7
                },
                new()
                {
                    ExperienceId = 2,
                    SkillId = 8
                },
                new()
                {
                    ExperienceId = 2,
                    SkillId = 13
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 3,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 13,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 19,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 20,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 21,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 22,
                },
                new()
                {
                    ExperienceId = 3,
                    SkillId = 23,
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 4
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 10
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 13
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 23
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 24
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 25
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 26
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 27
                },
                new()
                {
                    ExperienceId = 4,
                    SkillId = 28
                }
            ]);
        }

        private static void SeedExperienceAchievement(Service.Infrastructure.ExperienceContext context)
        {
            context.ExperienceAchievements.AddOrUpdate([
                new()
                {
                    Id = new Guid("e6b42741-a5a0-44f2-a67e-c8239e5601ac"),
                    ExperienceId = 0,
                    Description = "Implemented AWS drivers for cloud controller"
                },
                new()
                {
                    Id = new Guid("3041ed4f-d697-44ff-9d57-518a643a6d98"),
                    ExperienceId = 1,
                    Description = "Performed regression testing of FABRIC Testbed SDK against multiple operating systems"
                },
                new()
                {
                    Id = new Guid("c88b9251-6ea2-4158-b3ea-d3d524422900"),
                    ExperienceId = 1,
                    Description = "Debugged DOS-specific errors in FABRIC SDK infrastructure"
                },
                new()
                {
                    Id = new Guid("36c3b6c5-5623-4af9-8a6c-b03923f46bda"),
                    ExperienceId = 1,
                    Description = "Automated documentation generation for FABRIC Python SDK using Sphinx API"
                },
                new()
                {
                    Id = new Guid("d8e90b37-ad6a-455e-abc8-e947e7b3759a"),
                    ExperienceId = 2,
                    Description = "Recorded and organized thousands of data records comparing newly implemented strategies with those existing, finding an average decreased runtime of 50\\% over next best configuration"
                },
                new()
                {
                    Id = new Guid("c8e54868-4df7-4fac-827d-27c5c1cac864"),
                    ExperienceId = 2,
                    Description = "Implemented strategies within a research software tool used to find minimum independent dominating set"
                },
                new()
                {
                    Id = new Guid("804728ff-147e-4fad-bcb6-69433dc68793"),
                    ExperienceId = 3,
                    Description = "Validated complex solution strategies by hand on small problem instances in order to find what graph characteristics cooperate well with particular algorithms"
                },
                new()
                {
                    Id = new Guid("d7e78ac6-7635-44cd-9461-cb327bad1163"),
                    ExperienceId = 3,
                    Description = "Studied modern graph algorithms and literature on the dominating set problem, an NP-hard probably in graph theory, in order to optimize techniques for set generation"
                },
                new()
                {
                    Id = new Guid("ee0dbfeb-7f55-4918-9fe5-2426dfa259db"),
                    ExperienceId = 4,
                    Description = "Implemented data control mechanism over system of microservices utilizing Auth0 and HTTP claims to enable targeted content to be delivered to end users"
                },
                new()
                {
                    Id = new Guid("25f90b8d-425d-49a3-9686-b525648b71f0"),
                    ExperienceId = 4,
                    Description = "Created system of integration/regression tests against Microsoft Azure IoT Hub deployments to ensure that remote deployment workflow does not fail"
                }
            ]);
        }
    }
}
