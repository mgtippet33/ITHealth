using ITHealth.Web.API.Models.Question;

namespace ITHealth.Web.API.Models.Test;

public class TestResponseModel : BaseTestModel
{
    public int Id { get; set; }

    public List<QuestionResponseModel> Questions { get; set; }
}