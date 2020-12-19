﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class RoadMapItemsController : ControllerBase {

		private readonly IRoadmapItemRepository _roadmapItemRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepo;

		public RoadMapItemsController(
			IRoadmapItemRepository roadMapItemsRepo,
			IChangeInitiativeRepository changeInitiativeRepo) {
			_roadmapItemRepository = roadMapItemsRepo;
			_changeInitiativeRepo = changeInitiativeRepo;

		}

		/// <summary>
		/// Get a road map item with a given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "EmployeeAccess")]
		public ActionResult<RoadMapItem> GetRoadMapItem(int id) {
			RoadMapItem rmi = _roadmapItemRepository.GetBy(id);

			if (rmi == null) {

				return NotFound("Item not found");
			}

			return rmi;

		}

		/// <summary>
		/// Get the RoadmapItems from a change initiative
		/// </summary>
		/// <param name="changeInitiativeId"></param>
		/// <returns></returns>
		[Route("[action]/{changeInitiativeId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "EmployeeAccess")]
		public ActionResult<IEnumerable<RoadMapItem>> GetRoadMapItemsForChangeInitiative(int changeInitiativeId) {
			ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

			if (ci == null) {
				return NotFound("Change Initiative not found");
			}
			return ci.RoadMap.ToList();
		}

		/// <summary>
		/// Add a roadmap item to a change initiative
		/// </summary>
		/// <param name="changeInitiativeId"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("{changeInitiativeId}")]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult PostRoadMapItem(int changeInitiativeId, RoadMapItemDTO dto) {
			try {

				ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

				if (ci == null) {
					return NotFound("Change Initiative not found");
				}

				RoadMapItem newRmi = new RoadMapItem(dto.Title, dto.StartDate, dto.EndDate);
				ci.RoadMap.Add(newRmi);

				_roadmapItemRepository.SaveChanges();

				return CreatedAtAction(nameof(GetRoadMapItem), new {
					id = newRmi.Id
				}, newRmi);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		[HttpGet("[action]/{roadmapItemId}")]
		public List<Employee> GetEmployeesNotFilledInSurvey(int roadmapItemId) {
			RoadMapItem roadmapItem = _roadmapItemRepository.GetBy(roadmapItemId);
			List<Question> questions = roadmapItem.Assessment.Questions;
			List<ChangeInitiative> changeInitiatives = _changeInitiativeRepo.GetAll().ToList();
			ChangeInitiative changeInitiative = changeInitiatives.Where(ci => ci.RoadMap.Contains(roadmapItem)).SingleOrDefault();
			ChangeGroup changeGroup = changeInitiative.ChangeGroup;
			List<Employee> employees = changeGroup.EmployeeChangeGroups.Select(ec => ec.Employee).ToList();
			List<int> employeeIDs = changeGroup.EmployeeChangeGroups.Select(ec => ec.EmployeeId).ToList();

			List<Employee> filledInEmployeeIDs = questions.Min(q => q.QuestionRegistered.Keys).Select(qr => employees.FirstOrDefault(e => e.Id == qr)).ToList();

			List<Employee> result = employees.Except(filledInEmployeeIDs).ToList();
			return result;
        }

		/// <summary>
		/// Update an existing road map item
		/// </summary>
		/// <param name="roadmapItemId"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPut("{roadmapItemId}")]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult PutRoadMapItem(int roadmapItemId, RoadMapItemDTO dto) {
			try {
				RoadMapItem rmi = _roadmapItemRepository.GetBy(roadmapItemId);

				if (rmi == null) {
					return NotFound("Roadmap item with this id not found");
				}

				rmi.Update(dto);

				_roadmapItemRepository.SaveChanges();

				return NoContent();

			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Delete a road map item with a given id
		/// </summary>
		/// <param name="roadmapItemId"></param>
		/// <returns></returns>
		[HttpDelete("{roadmapItemId}")]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult DeleteRoadMapItem(int roadmapItemId) {
			try {
				RoadMapItem rmi = _roadmapItemRepository.GetBy(roadmapItemId);

				if (rmi == null) {
					return NotFound("Roadmap item with this id not found");
				}

				_roadmapItemRepository.Delete(rmi);
				_roadmapItemRepository.SaveChanges();

				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}
	}
}
