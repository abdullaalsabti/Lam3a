using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lam3a.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "SeedStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    UserAccountStatus = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Landmark = table.Column<string>(type: "text", nullable: false),
                    BuildingNumber = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    MapCoordinates_Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    MapCoordinates_Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Read = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviders", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ServiceProviders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleBrands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProviders",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProviders", x => new { x.ClientId, x.ServiceProviderId });
                    table.ForeignKey(
                        name: "FK_FavoriteProviders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProviders_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProviderServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EstimatedTime = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderServices_ServiceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderServices_ServiceProviders_UserId",
                        column: x => x.UserId,
                        principalTable: "ServiceProviders",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    PlateNumber = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false),
                    CarType = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.PlateNumber);
                    table.ForeignKey(
                        name: "FK_Vehicle_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleBrands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<TimeSpan>(type: "interval", nullable: false),
                    End = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehiclePlateNumber = table.Column<string>(type: "character varying(8)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeSlotId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_ProviderServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Vehicle_VehiclePlateNumber",
                        column: x => x.VehiclePlateNumber,
                        principalTable: "Vehicle",
                        principalColumn: "PlateNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Dry Clean" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Exterior Wash" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Interior Wash" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Full Wash" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Wax & Polish" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Interior Detailing" },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Exterior Detailing" },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Headlight Restoration" },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Leather Seat Conditioning" },
                    { new Guid("10000000-0000-0000-0000-00000000000a"), "Odor Removal / Ozone Treatment" },
                    { new Guid("10000000-0000-0000-0000-00000000000b"), "Ceramic Coating" },
                    { new Guid("10000000-0000-0000-0000-00000000000c"), "Paint Protection Film (PPF)" },
                    { new Guid("10000000-0000-0000-0000-00000000000d"), "Engine Bay Cleaning" },
                    { new Guid("10000000-0000-0000-0000-00000000000e"), "Underbody Wash" },
                    { new Guid("10000000-0000-0000-0000-00000000000f"), "Tire & Rim Polishing" }
                });

            migrationBuilder.InsertData(
                table: "VehicleBrands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Honda" },
                    { 3, "Nissan" },
                    { 4, "Hyundai" },
                    { 5, "Kia" },
                    { 6, "Ford" },
                    { 7, "Chevrolet" },
                    { 8, "Mercedes-Benz" },
                    { 9, "BMW" },
                    { 10, "Lexus" },
                    { 11, "Audi" },
                    { 12, "Volkswagen" },
                    { 13, "Porsche" },
                    { 14, "Land Rover" },
                    { 15, "Jaguar" },
                    { 16, "Mazda" },
                    { 17, "Subaru" },
                    { 18, "Mitsubishi" },
                    { 19, "GMC" },
                    { 20, "Cadillac" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Camry" },
                    { 2, 1, "Corolla" },
                    { 3, 1, "Land Cruiser" },
                    { 4, 1, "Crown" },
                    { 5, 1, "Hilux" },
                    { 6, 1, "RAV4" },
                    { 7, 1, "Highlander" },
                    { 8, 2, "Civic" },
                    { 9, 2, "Accord" },
                    { 10, 2, "CR-V" },
                    { 11, 2, "Pilot" },
                    { 12, 2, "City" },
                    { 13, 3, "Sunny" },
                    { 14, 3, "Sentra" },
                    { 15, 3, "Altima" },
                    { 16, 3, "Patrol" },
                    { 17, 3, "X-Trail" },
                    { 18, 4, "Accent" },
                    { 19, 4, "Elantra" },
                    { 20, 4, "Sonata" },
                    { 21, 4, "Tucson" },
                    { 22, 4, "Santa Fe" },
                    { 23, 5, "Rio" },
                    { 24, 5, "Cerato" },
                    { 25, 5, "K5" },
                    { 26, 5, "Sportage" },
                    { 27, 5, "Sorento" },
                    { 28, 6, "Taurus" },
                    { 29, 6, "Mustang" },
                    { 30, 6, "Explorer" },
                    { 31, 6, "F-150" },
                    { 32, 6, "Expedition" },
                    { 33, 7, "Spark" },
                    { 34, 7, "Malibu" },
                    { 35, 7, "Camaro" },
                    { 36, 7, "Tahoe" },
                    { 37, 7, "Silverado" },
                    { 38, 8, "C-Class" },
                    { 39, 8, "E-Class" },
                    { 40, 8, "S-Class" },
                    { 41, 8, "G-Class" },
                    { 42, 9, "3 Series" },
                    { 43, 9, "5 Series" },
                    { 44, 9, "7 Series" },
                    { 45, 9, "X5" },
                    { 46, 10, "IS" },
                    { 47, 10, "ES" },
                    { 48, 10, "LS" },
                    { 49, 10, "LX" },
                    { 50, 10, "RX" },
                    { 51, 11, "A3" },
                    { 52, 11, "A4" },
                    { 53, 11, "A6" },
                    { 54, 11, "Q5" },
                    { 55, 11, "Q7" },
                    { 56, 12, "Golf" },
                    { 57, 12, "Jetta" },
                    { 58, 12, "Passat" },
                    { 59, 12, "Tiguan" },
                    { 60, 12, "Touareg" },
                    { 61, 13, "911" },
                    { 62, 13, "Cayenne" },
                    { 63, 13, "Macan" },
                    { 64, 13, "Panamera" },
                    { 65, 13, "Taycan" },
                    { 66, 14, "Range Rover" },
                    { 67, 14, "Defender" },
                    { 68, 14, "Discovery" },
                    { 69, 14, "Evoque" },
                    { 70, 14, "Velar" },
                    { 71, 15, "XE" },
                    { 72, 15, "XF" },
                    { 73, 15, "F-Pace" },
                    { 74, 15, "E-Pace" },
                    { 75, 15, "F-Type" },
                    { 76, 16, "Mazda3" },
                    { 77, 16, "Mazda6" },
                    { 78, 16, "CX-5" },
                    { 79, 16, "CX-9" },
                    { 80, 16, "CX-30" },
                    { 81, 17, "Impreza" },
                    { 82, 17, "Legacy" },
                    { 83, 17, "Outback" },
                    { 84, 17, "Forester" },
                    { 85, 17, "Crosstrek" },
                    { 86, 18, "Lancer" },
                    { 87, 18, "Pajero" },
                    { 88, 18, "Outlander" },
                    { 89, 18, "ASX" },
                    { 90, 18, "Eclipse Cross" },
                    { 91, 19, "Sierra" },
                    { 92, 19, "Yukon" },
                    { 93, 19, "Terrain" },
                    { 94, 19, "Acadia" },
                    { 95, 19, "Canyon" },
                    { 96, 20, "Escalade" },
                    { 97, 20, "CT5" },
                    { 98, 20, "XT5" },
                    { 99, 20, "XT6" },
                    { 100, 20, "CT4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProviders_ServiceProviderId",
                table: "FavoriteProviders",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServices_CategoryId",
                table: "ProviderServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderServices_UserId",
                table: "ProviderServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ServiceProviderId",
                table: "Schedules",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_AddressId",
                table: "ServiceRequests",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceId",
                table: "ServiceRequests",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceProviderId",
                table: "ServiceRequests",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_TimeSlotId",
                table: "ServiceRequests",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_VehiclePlateNumber",
                table: "ServiceRequests",
                column: "VehiclePlateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId",
                table: "TimeSlots",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BrandId",
                table: "Vehicle",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ClientId",
                table: "Vehicle",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ModelId",
                table: "Vehicle",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_BrandId",
                table: "VehicleModels",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteProviders");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SeedStatus");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ProviderServices");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "ServiceProviders");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
