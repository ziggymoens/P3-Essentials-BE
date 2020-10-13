﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.UsersConfiguration {
	public class ChangeManagerConfiguration : IEntityTypeConfiguration<ChangeManager> {
		public void Configure(EntityTypeBuilder<ChangeManager> builder) {

			builder.HasMany(cm => cm.CreatedChangeInitiatives).WithOne();
		}
	}
}
