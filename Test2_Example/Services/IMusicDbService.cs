using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Test2_Example.Services
{
    public interface IMusicDbService
    {
        public IEnumerable GetLabelInfo(int id);
        public IActionResult DeleteMusician(int id);

    }
}