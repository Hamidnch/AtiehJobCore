using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtiehJobCore.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AmountUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Branch = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TwoLetterIsoCode = table.Column<string>(maxLength: 2, nullable: true),
                    ThreeLetterIsoCode = table.Column<string>(maxLength: 3, nullable: true),
                    NumericIsoCode = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(nullable: true),
                    XmlData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrivingLicenseNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicenseNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerAddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerAddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerIntroductionMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Method = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerIntroductionMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOrganizationUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForeignLanguageNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignLanguageNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionalLetters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    From = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionalLetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDegreeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDegreeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    LogLevel = table.Column<string>(nullable: true),
                    Logger = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    StateJson = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OccupationalGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationalGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipDisciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDiseaseNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDiseaseNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 450, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 11, nullable: true),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    SsnId = table.Column<string>(maxLength: 100, nullable: true),
                    SsnSerial = table.Column<string>(maxLength: 100, nullable: true),
                    GenderType = table.Column<byte>(nullable: true),
                    PhotoFileName = table.Column<string>(maxLength: 450, nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    LastVisitedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsEmailPublic = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlCache",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 449, nullable: false),
                    Value = table.Column<byte[]>(nullable: false),
                    ExpiresAtTime = table.Column<DateTimeOffset>(nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlCache", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentGateways",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    BankCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentGateways", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentGateways_Banks_BankCode",
                        column: x => x.BankCode,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 100, nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CountryCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OccupationalTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OccupationalGroupCode = table.Column<int>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationalTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OccupationalTitles_OccupationalGroups_OccupationalGroupCode",
                        column: x => x.OccupationalGroupCode,
                        principalTable: "OccupationalGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipTendencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(nullable: false),
                    ScholarshipDisciplineCode = table.Column<int>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipTendencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScholarshipTendencies_ScholarshipDisciplines_ScholarshipDisciplineCode",
                        column: x => x.ScholarshipDisciplineCode,
                        principalTable: "ScholarshipDisciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    Size = table.Column<long>(nullable: true),
                    Extensions = table.Column<string>(maxLength: 10, nullable: true),
                    DownloadsCount = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FileNumber = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    OrganizationUnitCode = table.Column<int>(nullable: true),
                    HumanApplicantUnit = table.Column<string>(maxLength: 255, nullable: true),
                    EnrollDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrollTime = table.Column<string>(maxLength: 5, nullable: true),
                    ActivityField = table.Column<int>(nullable: true),
                    ActivityType = table.Column<string>(maxLength: 255, nullable: true),
                    UnitCode = table.Column<string>(maxLength: 50, nullable: true),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Family = table.Column<string>(maxLength: 50, nullable: true),
                    InsuranceStatus = table.Column<bool>(nullable: true),
                    InsuranceCode = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 255, nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    PropertyType = table.Column<int>(nullable: true),
                    IntroductionMethodCode = table.Column<int>(nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    BeforeDispatchCoordination = table.Column<bool>(nullable: true),
                    CurrentState = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employers_EmployerIntroductionMethods_IntroductionMethodCode",
                        column: x => x.IntroductionMethodCode,
                        principalTable: "EmployerIntroductionMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employers_EmployerOrganizationUnits_OrganizationUnitCode",
                        column: x => x.OrganizationUnitCode,
                        principalTable: "EmployerOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChargeBox",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true),
                    TotalAmount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChargeBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChargeBox_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HashedPassword = table.Column<string>(maxLength: 450, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUsedPasswords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserIp = table.Column<string>(maxLength: 15, nullable: true),
                    PaymentGatewayCode = table.Column<int>(nullable: true),
                    Amount = table.Column<long>(nullable: true),
                    AmountUnitCode = table.Column<int>(nullable: true),
                    RequestDate = table.Column<DateTimeOffset>(nullable: true),
                    RequestState = table.Column<bool>(nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: true),
                    PaymentState = table.Column<bool>(nullable: true),
                    VerifyDate = table.Column<DateTimeOffset>(nullable: true),
                    VerifyState = table.Column<bool>(nullable: true),
                    OrderId = table.Column<string>(maxLength: 12, nullable: true),
                    ReferenceId = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Token = table.Column<string>(maxLength: 100, nullable: true),
                    PaymentKind = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AmountUnits_AmountUnitCode",
                        column: x => x.AmountUnitCode,
                        principalTable: "AmountUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentGateways_PaymentGatewayCode",
                        column: x => x.PaymentGatewayCode,
                        principalTable: "PaymentGateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    TerminalId = table.Column<string>(nullable: true),
                    DepartmentCode = table.Column<string>(nullable: true),
                    UserCode = table.Column<string>(nullable: true),
                    UserPassword = table.Column<string>(nullable: true),
                    Sha1Key = table.Column<string>(nullable: true),
                    SiteIp = table.Column<string>(nullable: true),
                    CallbackUrl = table.Column<string>(nullable: true),
                    BankUrl = table.Column<string>(nullable: true),
                    PaymentGatewayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSettings_PaymentGateways_PaymentGatewayId",
                        column: x => x.PaymentGatewayId,
                        principalTable: "PaymentGateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FileNumber = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ManagerName = table.Column<string>(maxLength: 50, nullable: true),
                    ManagerNationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    ActivityType = table.Column<int>(nullable: true),
                    LicenseType = table.Column<int>(nullable: true),
                    LicenseDate = table.Column<DateTimeOffset>(nullable: true),
                    LicenseNumber = table.Column<string>(maxLength: 16, nullable: true),
                    LicenseLocation = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    WorkshopCode = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Placements_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Placements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shahrestans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ProvinceCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shahrestans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shahrestans_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    OrderId = table.Column<string>(maxLength: 12, nullable: true),
                    BaseChargeAmount = table.Column<long>(nullable: false),
                    ChargeCount = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountCharges_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountCharges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacementBankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    BankCode = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 30, nullable: true),
                    PlacementCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacementBankAccounts_Banks_BankCode",
                        column: x => x.BankCode,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacementBankAccounts_Placements_PlacementCode",
                        column: x => x.PlacementCode,
                        principalTable: "Placements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShahrestanCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Shahrestans_ShahrestanCode",
                        column: x => x.ShahrestanCode,
                        principalTable: "Shahrestans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SectionCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Side = table.Column<string>(nullable: true),
                    CityCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    AddressTypeCode = table.Column<int>(nullable: true),
                    CountryCode = table.Column<int>(nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    ShahrestanCode = table.Column<int>(nullable: true),
                    SectionCode = table.Column<int>(nullable: true),
                    CityCode = table.Column<int>(nullable: true),
                    StreetCode = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PostBox = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    EmployerCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_EmployerAddressTypes_AddressTypeCode",
                        column: x => x.AddressTypeCode,
                        principalTable: "EmployerAddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Employers_EmployerCode",
                        column: x => x.EmployerCode,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Shahrestans_ShahrestanCode",
                        column: x => x.ShahrestanCode,
                        principalTable: "Shahrestans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerAddresses_Streets_StreetCode",
                        column: x => x.StreetCode,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployerTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CountryCode = table.Column<int>(nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    ShahrestanCode = table.Column<int>(nullable: true),
                    SectionCode = table.Column<int>(nullable: true),
                    CityCode = table.Column<int>(nullable: true),
                    StreetCode = table.Column<int>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    EmployerCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Employers_EmployerCode",
                        column: x => x.EmployerCode,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Shahrestans_ShahrestanCode",
                        column: x => x.ShahrestanCode,
                        principalTable: "Shahrestans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerTransfers_Streets_StreetCode",
                        column: x => x.StreetCode,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobseekers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FileNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Family = table.Column<string>(maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(maxLength: 50, nullable: true),
                    BirthPlace = table.Column<string>(maxLength: 50, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 20, nullable: true),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    SsnId = table.Column<string>(maxLength: 50, nullable: true),
                    SsnSerial = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    EnrollDate = table.Column<DateTime>(type: "date", nullable: true),
                    EnrollTime = table.Column<string>(maxLength: 5, nullable: true),
                    Marital = table.Column<byte>(nullable: true),
                    ChildCount = table.Column<byte>(nullable: true),
                    HeadHousehold = table.Column<bool>(nullable: true),
                    DependentPersonsCount = table.Column<byte>(nullable: true),
                    ReligionId = table.Column<byte>(nullable: true),
                    ReligionName = table.Column<string>(maxLength: 50, nullable: true),
                    Nationality = table.Column<byte>(nullable: true),
                    CoveredBy = table.Column<byte>(nullable: true),
                    SoldierState = table.Column<byte>(nullable: true),
                    ExemptionCase = table.Column<string>(maxLength: 255, nullable: true),
                    CoverageType = table.Column<byte>(nullable: true),
                    Stature = table.Column<byte>(nullable: true),
                    Weight = table.Column<byte>(nullable: true),
                    CurrentActivityStatus = table.Column<byte>(nullable: true),
                    BeforeSearchState = table.Column<byte>(nullable: true),
                    RetirementPlace = table.Column<string>(maxLength: 100, nullable: true),
                    IsInstitutionalLetter = table.Column<bool>(nullable: true),
                    InstitutionalLetterCode = table.Column<int>(nullable: true),
                    CountryCode = table.Column<int>(nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    ShahrestanCode = table.Column<int>(nullable: true),
                    SectionCode = table.Column<int>(nullable: true),
                    CityCode = table.Column<int>(nullable: true),
                    StreetCode = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    Picture = table.Column<string>(maxLength: 255, nullable: true),
                    SacrificeState = table.Column<byte>(nullable: true),
                    PhysicalCondition = table.Column<byte>(nullable: true),
                    DisabledType = table.Column<byte>(nullable: true),
                    DisabledPercent = table.Column<byte>(nullable: true),
                    InsuranceHistoryState = table.Column<bool>(nullable: true),
                    InsuranceOrganization = table.Column<byte>(nullable: true),
                    InsuranceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    InsuranceHistory = table.Column<byte>(nullable: true),
                    UnemploymentInsurance = table.Column<bool>(nullable: true),
                    StartDatePension = table.Column<DateTime>(type: "date", nullable: true),
                    EndDatePension = table.Column<DateTime>(type: "date", nullable: true),
                    CurrentState = table.Column<string>(maxLength: 255, nullable: true),
                    PlacementName = table.Column<string>(nullable: true),
                    IsSpecialDisease = table.Column<bool>(nullable: true),
                    CanHistoryAbuse = table.Column<bool>(nullable: true),
                    HistoryAbuseDescription = table.Column<string>(maxLength: 255, nullable: true),
                    WorkTime = table.Column<string>(maxLength: 7, nullable: true),
                    WageType = table.Column<byte>(nullable: true),
                    WageMinimum = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobseekers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_InstitutionalLetters_InstitutionalLetterCode",
                        column: x => x.InstitutionalLetterCode,
                        principalTable: "InstitutionalLetters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Shahrestans_ShahrestanCode",
                        column: x => x.ShahrestanCode,
                        principalTable: "Shahrestans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Streets_StreetCode",
                        column: x => x.StreetCode,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobseekers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacementAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    CountryCode = table.Column<int>(nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    ShahrestanCode = table.Column<int>(nullable: true),
                    SectionCode = table.Column<int>(nullable: true),
                    CityCode = table.Column<int>(nullable: true),
                    StreetCode = table.Column<int>(nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 25, nullable: true),
                    Phone1 = table.Column<string>(maxLength: 100, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 100, nullable: true),
                    Fax = table.Column<string>(maxLength: 25, nullable: true),
                    PlacementCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Placements_PlacementCode",
                        column: x => x.PlacementCode,
                        principalTable: "Placements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Sections_SectionCode",
                        column: x => x.SectionCode,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Shahrestans_ShahrestanCode",
                        column: x => x.ShahrestanCode,
                        principalTable: "Shahrestans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementAddresses_Streets_StreetCode",
                        column: x => x.StreetCode,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOpportunities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 20, nullable: true),
                    OccupationalGroupCode = table.Column<int>(nullable: true),
                    OccupationalTitleCode = table.Column<int>(nullable: true),
                    EmployerCode = table.Column<int>(nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "date", nullable: true),
                    RepeatDate = table.Column<DateTime>(type: "date", nullable: true),
                    GenderTypes2 = table.Column<byte>(nullable: true),
                    ManCount = table.Column<byte>(nullable: true),
                    WomanCount = table.Column<byte>(nullable: true),
                    WorkTime = table.Column<int>(nullable: true),
                    StartTime = table.Column<string>(maxLength: 5, nullable: true),
                    EndTime = table.Column<string>(maxLength: 5, nullable: true),
                    OccupationalHistoryRequired = table.Column<int>(nullable: true),
                    SoldierState = table.Column<byte>(nullable: true),
                    Marital = table.Column<byte>(nullable: true),
                    StartAge = table.Column<byte>(nullable: true),
                    EndAge = table.Column<byte>(nullable: true),
                    Applicant = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantPost = table.Column<string>(maxLength: 30, nullable: true),
                    WageType = table.Column<byte>(nullable: true),
                    SalaryFrom = table.Column<long>(nullable: true),
                    SalaryUntil = table.Column<long>(nullable: true),
                    FaceQuality = table.Column<byte>(nullable: true),
                    Coverage = table.Column<byte>(nullable: true),
                    Dialect = table.Column<byte>(nullable: true),
                    PublicRelations = table.Column<byte>(nullable: true),
                    PhysicalForces = table.Column<byte>(nullable: true),
                    GeneralInfoStatus = table.Column<byte>(nullable: true),
                    InterviewAddressCode = table.Column<int>(nullable: true),
                    InterviewPhone = table.Column<string>(maxLength: 50, nullable: true),
                    IsPartTimeWork = table.Column<bool>(nullable: true),
                    LadiesSuitable = table.Column<bool>(nullable: true),
                    IsInsurance = table.Column<bool>(nullable: true),
                    RetireesSuitable = table.Column<bool>(nullable: true),
                    EmployerAddressList = table.Column<string>(maxLength: 255, nullable: true),
                    CurrentState = table.Column<string>(maxLength: 255, nullable: true),
                    PlacementName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOpportunities_Employers_EmployerCode",
                        column: x => x.EmployerCode,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunities_EmployerAddresses_InterviewAddressCode",
                        column: x => x.InterviewAddressCode,
                        principalTable: "EmployerAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunities_OccupationalGroups_OccupationalGroupCode",
                        column: x => x.OccupationalGroupCode,
                        principalTable: "OccupationalGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunities_OccupationalTitles_OccupationalTitleCode",
                        column: x => x.OccupationalTitleCode,
                        principalTable: "OccupationalTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerDontWantEmployers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    OrganizationName = table.Column<string>(maxLength: 100, nullable: true),
                    Phone1 = table.Column<string>(maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 50, nullable: true),
                    Cause = table.Column<string>(maxLength: 500, nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerDontWantEmployers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerDontWantEmployers_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerDrivingLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    DrivingLicenseNameCode = table.Column<int>(nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerDrivingLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerDrivingLicenses_DrivingLicenseNames_DrivingLicenseNameCode",
                        column: x => x.DrivingLicenseNameCode,
                        principalTable: "DrivingLicenseNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerDrivingLicenses_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    EducationLevelCode = table.Column<int>(nullable: true),
                    IsStudent = table.Column<bool>(nullable: true),
                    ScholarshipDisciplineCode = table.Column<int>(nullable: true),
                    ScholarshipTendencyCode = table.Column<int>(nullable: true),
                    ProvinceCode = table.Column<int>(nullable: true),
                    OtherProvinces = table.Column<string>(maxLength: 255, nullable: true),
                    UniversityTypeCode = table.Column<int>(nullable: true),
                    UniversityName = table.Column<string>(maxLength: 255, nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "date", nullable: true),
                    Average = table.Column<decimal>(nullable: true),
                    PassUnitCount = table.Column<byte>(nullable: true),
                    JobseekerCode = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_EducationLevels_EducationLevelCode",
                        column: x => x.EducationLevelCode,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_Jobseekers_JobseekerCode",
                        column: x => x.JobseekerCode,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_ScholarshipDisciplines_ScholarshipDisciplineCode",
                        column: x => x.ScholarshipDisciplineCode,
                        principalTable: "ScholarshipDisciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_ScholarshipTendencies_ScholarshipTendencyCode",
                        column: x => x.ScholarshipTendencyCode,
                        principalTable: "ScholarshipTendencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerEducations_UniversityTypes_UniversityTypeCode",
                        column: x => x.UniversityTypeCode,
                        principalTable: "UniversityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerEssentialPhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Family = table.Column<string>(maxLength: 50, nullable: false),
                    Relationship = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerEssentialPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerEssentialPhones_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerForeignLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    ForeignLanguageNameCode = table.Column<int>(nullable: true),
                    IsLanguageDegree = table.Column<bool>(nullable: true),
                    LanguageDegreeTypeCode = table.Column<int>(nullable: true),
                    Conversation = table.Column<byte>(nullable: true),
                    Read = table.Column<byte>(nullable: true),
                    Write = table.Column<byte>(nullable: true),
                    Translation = table.Column<byte>(nullable: true),
                    Privilege = table.Column<int>(nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerForeignLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerForeignLanguages_ForeignLanguageNames_ForeignLanguageNameCode",
                        column: x => x.ForeignLanguageNameCode,
                        principalTable: "ForeignLanguageNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobseekerForeignLanguages_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerForeignLanguages_LanguageDegreeTypes_LanguageDegreeTypeCode",
                        column: x => x.LanguageDegreeTypeCode,
                        principalTable: "LanguageDegreeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerJobDemands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    OccupationalGroupCode = table.Column<int>(nullable: false),
                    OccupationalTitleCode = table.Column<int>(nullable: false),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerJobDemands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerJobDemands_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerJobDemands_OccupationalGroups_OccupationalGroupCode",
                        column: x => x.OccupationalGroupCode,
                        principalTable: "OccupationalGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerJobDemands_OccupationalTitles_OccupationalTitleCode",
                        column: x => x.OccupationalTitleCode,
                        principalTable: "OccupationalTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerOccupationalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    OccupationalGroupCode = table.Column<int>(nullable: false),
                    OccupationalTitleCode = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 255, nullable: true),
                    ExperienceWork = table.Column<byte>(nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    LeaveWorkReason = table.Column<string>(maxLength: 255, nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerOccupationalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerOccupationalHistories_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerOccupationalHistories_OccupationalGroups_OccupationalGroupCode",
                        column: x => x.OccupationalGroupCode,
                        principalTable: "OccupationalGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerOccupationalHistories_OccupationalTitles_OccupationalTitleCode",
                        column: x => x.OccupationalTitleCode,
                        principalTable: "OccupationalTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerSkillDemands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SkillCourseCode = table.Column<int>(nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerSkillDemands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerSkillDemands_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerSkillDemands_SkillCourses_SkillCourseCode",
                        column: x => x.SkillCourseCode,
                        principalTable: "SkillCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    SkillDegree = table.Column<byte>(nullable: true),
                    LearningMethod = table.Column<int>(nullable: true),
                    PeriodTime = table.Column<byte>(nullable: true),
                    CollegeName = table.Column<string>(maxLength: 255, nullable: true),
                    EndPeriodDate = table.Column<DateTime>(type: "date", nullable: true),
                    CollegeType = table.Column<int>(nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerSkills_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerSpecialDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SpecialDiseaseNameCode = table.Column<int>(nullable: true),
                    DiseaseDescription = table.Column<string>(maxLength: 255, nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerSpecialDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerSpecialDiseases_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerSpecialDiseases_SpecialDiseaseNames_SpecialDiseaseNameCode",
                        column: x => x.SpecialDiseaseNameCode,
                        principalTable: "SpecialDiseaseNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobseekerVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    VehicleNameCode = table.Column<int>(nullable: true),
                    VehicleModel = table.Column<string>(maxLength: 100, nullable: true),
                    JobseekerId = table.Column<int>(nullable: false),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobseekerVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobseekerVehicles_Jobseekers_JobseekerId",
                        column: x => x.JobseekerId,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobseekerVehicles_VehicleNames_VehicleNameCode",
                        column: x => x.VehicleNameCode,
                        principalTable: "VehicleNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOpportunityEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EducationLevelCode = table.Column<int>(nullable: true),
                    ScholarshipDisciplineCode = table.Column<int>(nullable: true),
                    ScholarshipTendencyCode = table.Column<int>(nullable: true),
                    UniversityTypeCode = table.Column<int>(nullable: true),
                    UniversityName = table.Column<string>(maxLength: 50, nullable: true),
                    JobOpportunityCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpportunityEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOpportunityEducations_EducationLevels_EducationLevelCode",
                        column: x => x.EducationLevelCode,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunityEducations_JobOpportunities_JobOpportunityCode",
                        column: x => x.JobOpportunityCode,
                        principalTable: "JobOpportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunityEducations_ScholarshipDisciplines_ScholarshipDisciplineCode",
                        column: x => x.ScholarshipDisciplineCode,
                        principalTable: "ScholarshipDisciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunityEducations_ScholarshipTendencies_ScholarshipTendencyCode",
                        column: x => x.ScholarshipTendencyCode,
                        principalTable: "ScholarshipTendencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunityEducations_UniversityTypes_UniversityTypeCode",
                        column: x => x.UniversityTypeCode,
                        principalTable: "UniversityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOpportunityReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    JobseekerCode = table.Column<int>(nullable: false),
                    JobOpportunityCode = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpportunityReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOpportunityReservations_JobOpportunities_JobOpportunityCode",
                        column: x => x.JobOpportunityCode,
                        principalTable: "JobOpportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunityReservations_Jobseekers_JobseekerCode",
                        column: x => x.JobseekerCode,
                        principalTable: "Jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOpportunitySkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    SkillCourseCode = table.Column<int>(nullable: true),
                    JobOpportunityCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpportunitySkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOpportunitySkills_JobOpportunities_JobOpportunityCode",
                        column: x => x.JobOpportunityCode,
                        principalTable: "JobOpportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobOpportunitySkills_SkillCourses_SkillCourseCode",
                        column: x => x.SkillCourseCode,
                        principalTable: "SkillCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_SectionCode",
                table: "Cities",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_DataProtectionKeys_FriendlyName",
                table: "DataProtectionKeys",
                column: "FriendlyName",
                unique: true,
                filter: "[FriendlyName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_AddressTypeCode",
                table: "EmployerAddresses",
                column: "AddressTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_CityCode",
                table: "EmployerAddresses",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_CountryCode",
                table: "EmployerAddresses",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_EmployerCode",
                table: "EmployerAddresses",
                column: "EmployerCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_ProvinceCode",
                table: "EmployerAddresses",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_SectionCode",
                table: "EmployerAddresses",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_ShahrestanCode",
                table: "EmployerAddresses",
                column: "ShahrestanCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerAddresses_StreetCode",
                table: "EmployerAddresses",
                column: "StreetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_IntroductionMethodCode",
                table: "Employers",
                column: "IntroductionMethodCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_OrganizationUnitCode",
                table: "Employers",
                column: "OrganizationUnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_CityCode",
                table: "EmployerTransfers",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_CountryCode",
                table: "EmployerTransfers",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_EmployerCode",
                table: "EmployerTransfers",
                column: "EmployerCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_ProvinceCode",
                table: "EmployerTransfers",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_SectionCode",
                table: "EmployerTransfers",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_ShahrestanCode",
                table: "EmployerTransfers",
                column: "ShahrestanCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerTransfers_StreetCode",
                table: "EmployerTransfers",
                column: "StreetCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunities_EmployerCode",
                table: "JobOpportunities",
                column: "EmployerCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunities_InterviewAddressCode",
                table: "JobOpportunities",
                column: "InterviewAddressCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunities_OccupationalGroupCode",
                table: "JobOpportunities",
                column: "OccupationalGroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunities_OccupationalTitleCode",
                table: "JobOpportunities",
                column: "OccupationalTitleCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityEducations_EducationLevelCode",
                table: "JobOpportunityEducations",
                column: "EducationLevelCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityEducations_JobOpportunityCode",
                table: "JobOpportunityEducations",
                column: "JobOpportunityCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityEducations_ScholarshipDisciplineCode",
                table: "JobOpportunityEducations",
                column: "ScholarshipDisciplineCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityEducations_ScholarshipTendencyCode",
                table: "JobOpportunityEducations",
                column: "ScholarshipTendencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityEducations_UniversityTypeCode",
                table: "JobOpportunityEducations",
                column: "UniversityTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityReservations_JobOpportunityCode",
                table: "JobOpportunityReservations",
                column: "JobOpportunityCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunityReservations_JobseekerCode",
                table: "JobOpportunityReservations",
                column: "JobseekerCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunitySkills_JobOpportunityCode",
                table: "JobOpportunitySkills",
                column: "JobOpportunityCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpportunitySkills_SkillCourseCode",
                table: "JobOpportunitySkills",
                column: "SkillCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerDontWantEmployers_JobseekerId",
                table: "JobseekerDontWantEmployers",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerDrivingLicenses_DrivingLicenseNameCode",
                table: "JobseekerDrivingLicenses",
                column: "DrivingLicenseNameCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerDrivingLicenses_JobseekerId",
                table: "JobseekerDrivingLicenses",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_EducationLevelCode",
                table: "JobseekerEducations",
                column: "EducationLevelCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_JobseekerCode",
                table: "JobseekerEducations",
                column: "JobseekerCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_ProvinceCode",
                table: "JobseekerEducations",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_ScholarshipDisciplineCode",
                table: "JobseekerEducations",
                column: "ScholarshipDisciplineCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_ScholarshipTendencyCode",
                table: "JobseekerEducations",
                column: "ScholarshipTendencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEducations_UniversityTypeCode",
                table: "JobseekerEducations",
                column: "UniversityTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerEssentialPhones_JobseekerId",
                table: "JobseekerEssentialPhones",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerForeignLanguages_ForeignLanguageNameCode",
                table: "JobseekerForeignLanguages",
                column: "ForeignLanguageNameCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerForeignLanguages_JobseekerId",
                table: "JobseekerForeignLanguages",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerForeignLanguages_LanguageDegreeTypeCode",
                table: "JobseekerForeignLanguages",
                column: "LanguageDegreeTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerJobDemands_JobseekerId",
                table: "JobseekerJobDemands",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerJobDemands_OccupationalGroupCode",
                table: "JobseekerJobDemands",
                column: "OccupationalGroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerJobDemands_OccupationalTitleCode",
                table: "JobseekerJobDemands",
                column: "OccupationalTitleCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerOccupationalHistories_JobseekerId",
                table: "JobseekerOccupationalHistories",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerOccupationalHistories_OccupationalGroupCode",
                table: "JobseekerOccupationalHistories",
                column: "OccupationalGroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerOccupationalHistories_OccupationalTitleCode",
                table: "JobseekerOccupationalHistories",
                column: "OccupationalTitleCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_CityCode",
                table: "Jobseekers",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_CountryCode",
                table: "Jobseekers",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_FileNumber",
                table: "Jobseekers",
                column: "FileNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_InstitutionalLetterCode",
                table: "Jobseekers",
                column: "InstitutionalLetterCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_ProvinceCode",
                table: "Jobseekers",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_SectionCode",
                table: "Jobseekers",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_ShahrestanCode",
                table: "Jobseekers",
                column: "ShahrestanCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_StreetCode",
                table: "Jobseekers",
                column: "StreetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Jobseekers_UserId",
                table: "Jobseekers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerSkillDemands_JobseekerId",
                table: "JobseekerSkillDemands",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerSkillDemands_SkillCourseCode",
                table: "JobseekerSkillDemands",
                column: "SkillCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerSkills_JobseekerId",
                table: "JobseekerSkills",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerSpecialDiseases_JobseekerId",
                table: "JobseekerSpecialDiseases",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerSpecialDiseases_SpecialDiseaseNameCode",
                table: "JobseekerSpecialDiseases",
                column: "SpecialDiseaseNameCode");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerVehicles_JobseekerId",
                table: "JobseekerVehicles",
                column: "JobseekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobseekerVehicles_VehicleNameCode",
                table: "JobseekerVehicles",
                column: "VehicleNameCode");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationalTitles_OccupationalGroupCode",
                table: "OccupationalTitles",
                column: "OccupationalGroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentGateways_BankCode",
                table: "PaymentGateways",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AmountUnitCode",
                table: "Payments",
                column: "AmountUnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentGatewayCode",
                table: "Payments",
                column: "PaymentGatewayCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId1",
                table: "Payments",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSettings_PaymentGatewayId",
                table: "PaymentSettings",
                column: "PaymentGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_CityCode",
                table: "PlacementAddresses",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_CountryCode",
                table: "PlacementAddresses",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_PlacementCode",
                table: "PlacementAddresses",
                column: "PlacementCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_ProvinceCode",
                table: "PlacementAddresses",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_SectionCode",
                table: "PlacementAddresses",
                column: "SectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_ShahrestanCode",
                table: "PlacementAddresses",
                column: "ShahrestanCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementAddresses_StreetCode",
                table: "PlacementAddresses",
                column: "StreetCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementBankAccounts_BankCode",
                table: "PlacementBankAccounts",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementBankAccounts_PlacementCode",
                table: "PlacementBankAccounts",
                column: "PlacementCode");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_ProvinceCode",
                table: "Placements",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_UserId",
                table: "Placements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryCode",
                table: "Provinces",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipTendencies_ScholarshipDisciplineCode",
                table: "ScholarshipTendencies",
                column: "ScholarshipDisciplineCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ShahrestanCode",
                table: "Sections",
                column: "ShahrestanCode");

            migrationBuilder.CreateIndex(
                name: "IX_Shahrestans_ProvinceCode",
                table: "Shahrestans",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_CityCode",
                table: "Streets",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountCharges_PaymentId",
                table: "UserAccountCharges",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountCharges_UserId",
                table: "UserAccountCharges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChargeBox_UserId1",
                table: "UserChargeBox",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserUsedPasswords_UserId",
                table: "UserUsedPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "Index_ExpiresAtTime",
                schema: "dbo",
                table: "SqlCache",
                column: "ExpiresAtTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "EmployerTransfers");

            migrationBuilder.DropTable(
                name: "JobOpportunityEducations");

            migrationBuilder.DropTable(
                name: "JobOpportunityReservations");

            migrationBuilder.DropTable(
                name: "JobOpportunitySkills");

            migrationBuilder.DropTable(
                name: "JobseekerDontWantEmployers");

            migrationBuilder.DropTable(
                name: "JobseekerDrivingLicenses");

            migrationBuilder.DropTable(
                name: "JobseekerEducations");

            migrationBuilder.DropTable(
                name: "JobseekerEssentialPhones");

            migrationBuilder.DropTable(
                name: "JobseekerForeignLanguages");

            migrationBuilder.DropTable(
                name: "JobseekerJobDemands");

            migrationBuilder.DropTable(
                name: "JobseekerOccupationalHistories");

            migrationBuilder.DropTable(
                name: "JobseekerSkillDemands");

            migrationBuilder.DropTable(
                name: "JobseekerSkills");

            migrationBuilder.DropTable(
                name: "JobseekerSpecialDiseases");

            migrationBuilder.DropTable(
                name: "JobseekerVehicles");

            migrationBuilder.DropTable(
                name: "LogItems");

            migrationBuilder.DropTable(
                name: "PaymentSettings");

            migrationBuilder.DropTable(
                name: "PlacementAddresses");

            migrationBuilder.DropTable(
                name: "PlacementBankAccounts");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserAccountCharges");

            migrationBuilder.DropTable(
                name: "UserChargeBox");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserUsedPasswords");

            migrationBuilder.DropTable(
                name: "SqlCache",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobOpportunities");

            migrationBuilder.DropTable(
                name: "DrivingLicenseNames");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropTable(
                name: "ScholarshipTendencies");

            migrationBuilder.DropTable(
                name: "UniversityTypes");

            migrationBuilder.DropTable(
                name: "ForeignLanguageNames");

            migrationBuilder.DropTable(
                name: "LanguageDegreeTypes");

            migrationBuilder.DropTable(
                name: "SkillCourses");

            migrationBuilder.DropTable(
                name: "SpecialDiseaseNames");

            migrationBuilder.DropTable(
                name: "Jobseekers");

            migrationBuilder.DropTable(
                name: "VehicleNames");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "EmployerAddresses");

            migrationBuilder.DropTable(
                name: "OccupationalTitles");

            migrationBuilder.DropTable(
                name: "ScholarshipDisciplines");

            migrationBuilder.DropTable(
                name: "InstitutionalLetters");

            migrationBuilder.DropTable(
                name: "AmountUnits");

            migrationBuilder.DropTable(
                name: "PaymentGateways");

            migrationBuilder.DropTable(
                name: "EmployerAddressTypes");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "OccupationalGroups");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "EmployerIntroductionMethods");

            migrationBuilder.DropTable(
                name: "EmployerOrganizationUnits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Shahrestans");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
