using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModule.Data
{
    public interface IModuleSeeder
    {
        Task SeedAsync();
    }
}
