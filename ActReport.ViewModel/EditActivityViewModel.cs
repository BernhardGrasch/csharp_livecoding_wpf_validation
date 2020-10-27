using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ActReport.ViewModel
{
  public class EditActivityViewModel : BaseViewModel
  {
    public EditActivityViewModel(IController controller) : base(controller)
    {
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      return Enumerable.Empty<ValidationResult>();
    }

  }
}
