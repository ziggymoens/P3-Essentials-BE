﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
	public class IAssesmentConfiguration : IEntityTypeConfiguration<IAssesment> {
		public void Configure(EntityTypeBuilder<IAssesment> builder) {
			builder.HasKey(s => s.Id);

			builder.Property(s => s.AmountSubmitted);

			builder.HasMany(s => s.Questions).WithOne();
			builder.HasOne(s => s.Feedback);
		}
	}
}
