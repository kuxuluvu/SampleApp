﻿// ***********************************************************************
// Assembly         : SampleApp.Infrastructure
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="SampleContextModelSnapshot.cs" company="SampleApp.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Infrastructure;

namespace SampleApp.Infrastructure.Migrations
{
    /// <summary>
    /// Class SampleContextModelSnapshot.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.Infrastructure.ModelSnapshot" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Infrastructure.ModelSnapshot" />
    [DbContext(typeof(SampleContext))]
    partial class SampleContextModelSnapshot : ModelSnapshot
    {
        /// <summary>
        /// Builds the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleApp.Infrastructure.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Expiration");

                    b.Property<string>("Token");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("SampleApp.Infrastructure.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DayOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<string>("Phone");

                    b.Property<string>("Salt");

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = new Guid("f35b43cd-0642-422e-b7d5-b276290f119d"), DayOfBirth = new DateTime(1999, 9, 16, 11, 6, 2, 662, DateTimeKind.Local), Email = "", FirstName = "admin", IsActive = true, IsDeleted = false, LastName = "", Password = "n0cCTtcV5CxkM3IhdBs40eWNiIKrGFpuGBm722V1TYA=", Phone = "", Salt = "BxWJlr6ogxpsF5Z9Wu0hpTDrV1J71UwsytTjx1Jp7z8=", Username = "admin" }
                    );
                });

            modelBuilder.Entity("SampleApp.Infrastructure.Models.RefreshToken", b =>
                {
                    b.HasOne("SampleApp.Infrastructure.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
