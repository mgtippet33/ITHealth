using ITHealth.Web.API.Models.Question;

namespace ITHealth.Web.API.Models.Test;

public class TestRequestModel : BaseTestModel
{
    public List<QuestionRequestModel> Questions { get; set; } 
}

public class BaseTestModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
}
