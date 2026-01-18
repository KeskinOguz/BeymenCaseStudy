using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule.Models
{
    public record ErrorResponse(int StatusCode, string Message, string? Detail = null);
}
