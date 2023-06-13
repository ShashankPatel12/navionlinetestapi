using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Models;
using System.Collections.Generic;
using SubCategory = NichiOnlineTest.View.Models.SubCategory;

namespace NichiOnlineTest.View.Logic
{
    public class OnlineTestViewLogic
    {
        private OnlineTestCoreLogic _logic;

        public OnlineTestViewLogic()
        {
            _logic = new OnlineTestCoreLogic();
        }

        public OnlineTestViewModel GetOnlineTestQuestions(string userId)
        {
            var result = _logic.GetOnlineTestQuestions(userId);

            var viewModel = new OnlineTestViewModel();

            viewModel.CategoryId = result.CategoryId;
            viewModel.CategoryName = result.CategoryName;
            viewModel.Time = result.Time.TotalSeconds;
            viewModel.TotalQuestions = result.TotalQuestions;
            viewModel.UserId = result.UserId;
            viewModel.AttendedQuestions = result.AttendedQuestions;
            viewModel.IsTestSubmitted = result.IsTestSubmitted;
            viewModel.SubCategories = new List<SubCategory>();

            foreach (var item in result.SubCategories)
            {
                var subcategory = new SubCategory();
                subcategory.SubCategoryId = item.SubCategoryId;
                subcategory.SubCategoryName = item.SubCategoryName;
                subcategory.TotalQuestions = item.TotalQuestions;
                subcategory.AttendedQuestions = item.AttendedQuestions;
                subcategory.Questions = new List<QuestionsViewModel>();

                foreach (var question in item.Questions)
                {
                    var que = new QuestionsViewModel();
                    que.QuestionId = question.QuestionId;
                    que.IsAnswered = question.IsAnswered;
                    que.IsMultiple = question.IsMultiple;
                    que.QuestionImg = question.QuestionImg;
                    que.AnswerImg = question.AnswerImg;
                    que.QuestionText = question.QuestionText;
                    que.QuestionType = question.QuestionType;
                    que.Answers = new List<AnswerViewModel>();
                    decimal marks = 0.00M;

                    foreach (var answer in question.Answers)
                    {
                        que.Answers.Add(new AnswerViewModel
                        {
                            AnswerId = answer.AnswerId,
                            AnswerText = answer.AnswerText,
                            Marks = answer.Marks,
                            IsSelected = answer.IsSelected,
                            UserAnswer = answer.UserAnswer
                        });
                        marks += answer.Marks;
                    }

                    que.Marks = marks;
                    subcategory.Questions.Add(que);
                }

                viewModel.SubCategories.Add(subcategory);

            }

            return viewModel;

        }

        public void SubmitAnswers(OnlineTestViewModel model)
        {
            var data = new OnlineTestDataModel();

            data.CategoryId = model.CategoryId;
            data.CategoryName = model.CategoryName;
            data.IsTestSubmitted = model.IsTestSubmitted;
            data.UserId = model.UserId;
            data.SubCategories = new List<Core.Models.SubCategory>();

            foreach (var item in model.SubCategories)
            {
                var subcategory = new Core.Models.SubCategory();
                subcategory.SubCategoryId = item.SubCategoryId;
                subcategory.SubCategoryName = item.SubCategoryName;
                subcategory.TotalQuestions = item.TotalQuestions;
                subcategory.Questions = new List<Questions>();

                foreach (var question in item.Questions)
                {
                    var que = new Questions();
                    que.QuestionId = question.QuestionId;
                    que.IsMultiple = question.IsMultiple;
                    que.QuestionImg = question.QuestionImg;
                    que.AnswerImg = question.AnswerImg;
                    que.QuestionText = question.QuestionText;
                    que.QuestionType = question.QuestionType;
                    que.Answers = new List<Answers>();
                    foreach (var answer in question.Answers)
                    {
                        que.Answers.Add(new Answers
                        {
                            AnswerId = answer.AnswerId,
                            AnswerText = answer.AnswerText,
                            Marks = answer.Marks,
                            IsSelected = answer.IsSelected,
                            UserAnswer = answer.UserAnswer
                        });
                    }

                    subcategory.Questions.Add(que);
                }

                data.SubCategories.Add(subcategory);

            }

            var logic = new OnlineTestCoreLogic();
            logic.SubmitAnswers(data);
        }
    }
}
