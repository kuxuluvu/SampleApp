// ***********************************************************************
// Assembly         : SampleApp.Infrastructure
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="SeedDatabase.cs" company="SampleApp.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using SampleApp.Infrastructure.Helper;
using System;
using System.Text;

namespace SampleApp.Infrastructure.Models
{
    /// <summary>
    /// Class SeedDatabase.
    /// </summary>
    public static class SeedDatabase
    {
        /// <summary>
        /// Seeds the specified module builder.
        /// </summary>
        /// <param name="moduleBuilder">The module builder.</param>
        public static void Seed(this ModelBuilder moduleBuilder)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                FirstName = "admin",
                LastName = string.Empty,
                DayOfBirth = DateTime.Now.AddYears(-20),
                Email = string.Empty,
                Phone = string.Empty,
                IsActive = true,
                IsDeleted = false,
            };
            user.Salt = SampleHelper.CreateSalt();
            user.Password = SampleHelper.GenerateSaltedHash(Encoding.ASCII.GetBytes("123456@"), Convert.FromBase64String(user.Salt));

            moduleBuilder.Entity<User>(x =>
            {
                x.HasData(user);
                x.HasIndex(y => y.Username).IsUnique();
                //x.ToTable("User");
            });

            moduleBuilder.Entity<RefreshToken>()/*.ToTable("RefreshToken")*/;
        }
    }
}