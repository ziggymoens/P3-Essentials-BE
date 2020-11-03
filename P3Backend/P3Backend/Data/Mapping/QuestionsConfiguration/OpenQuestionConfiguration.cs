﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
    public class OpenQuestionConfiguration : IEntityTypeConfiguration<OpenQuestion> {
        public void Configure(EntityTypeBuilder<OpenQuestion> builder) {

            builder.Property(mc => mc.PossibleAnswers).HasConversion(
               d => JsonConvert.SerializeObject(d, Formatting.None),
               s => JsonConvert.DeserializeObject<Dictionary<string, int>>(s)).HasMaxLength(4000);
        }
    }
}

