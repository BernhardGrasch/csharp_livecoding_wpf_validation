using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActReport.ViewModel
{
  public class EditActivityViewModel : BaseViewModel
  {
    public EditActivityViewModel(IController controller) : base(controller)
    {
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      return new ValidationResult[0];
    }

  }
}
