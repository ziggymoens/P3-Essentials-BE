﻿using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.RepoInterfaces {
	public interface IAdminRepository {
		IEnumerable<Admin> GetAll();

		Admin GetBy(int id);

		void Add(Admin a);

		void Update(Admin a);

		void Delete(Admin a);

		void SaveChanges();

		Admin GetByEmail(string email);
	}
}
