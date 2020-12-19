﻿using P3Backend.Model.Questions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace P3Backend.Model {
    public abstract class IAssessment {
        public int Id { get; set; }
        public List<Question> Questions { get; set; }
        public RangedQuestion Feedback { get; set; }

        [ForeignKey("RoadMapItemId")]
        public int RoadMapItemId { get; set; }

        public RoadMapItem RoadMapItem { get; set; }

        protected IAssessment(RoadMapItem rmi) {
            RoadMapItem = rmi;

            Questions = new List<Question>();

            Feedback = new RangedQuestion("How is your mood about this change initiative?");
        }

        protected IAssessment() {
            //EF
        }

        public void SurveyTemplates(string type) {
            /*type = "personal";
            if (type.Equals("personal")) {
				Question q1 = new MultipleChoiceQuestion("How is your mood about this change initiative?");				
				((MultipleChoiceQuestion)q1).AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" });
				Questions.Add(q1);
			}*/
        }

    }
}