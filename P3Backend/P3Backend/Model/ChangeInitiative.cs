﻿using P3Backend.Model.ChangeTypes;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeInitiative {
		public int Id { get; set; }
		public string Name { get; set; }
		public String Description { get; set; }
		public DateTime StartDate {
			get { return StartDate; }
			set {
				if (value <= DateTime.Now)
					throw new ArgumentException("Start must be in the future");
				else
					_ = value;
			}
		}
		public DateTime EndDate {
			get { return EndDate; }
			set {
				if (value <= StartDate)
					throw new ArgumentException("End must be after start");
				else
					_ = value;
			}
		}

		public IUser ChangeSponsor { get; set; } // could also be another CM
		public IChangeType ChangeType { get; set; }
		public IList<RoadMapItem> RoadMap { get; set; }

		public ChangeInitiative(string name, string desc, DateTime start, DateTime end, IUser sponsor, IChangeType changeType) {
			Name = name;
			Description = desc;
			StartDate = start;
			EndDate = end;
			ChangeSponsor = sponsor;
			ChangeType = changeType;

			RoadMap = new List<RoadMapItem>();
			// TODO standaard voorbereiding item toevoegen aan roadmap
		}

		protected ChangeInitiative() {
			// EF
		}
	}
}
