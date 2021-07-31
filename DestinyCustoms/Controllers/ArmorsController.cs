using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Armors;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Common.Enums;
using DestinyCustoms.Infrastructure;

namespace DestinyCustoms.Controllers
{
    public class ArmorsController : Controller
    {
        private readonly IArmorsService armorsService;

        public ArmorsController(
            IArmorsService armorsService)
        {
            this.armorsService = armorsService;
        }


        public IActionResult All([FromQuery]AllArmorsQueryModel query)
        {
            var armors = this.armorsService.All(
                        query.SearchTerm,
                        query.Class,
                        query.ArmorsPerPage,
                        query.CurrentPage);

            query.Armors = armors.Armors;
            query.Classes = this.armorsService.AllClassNames();
            query.AllArmorsCount = armors.AllArmorsCount;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
            => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddArmorFormModel armor)
        {
            var isClassCorrect = Enum.TryParse(armor.Class, out CharacterClass classEnum);

            if (!isClassCorrect)
            {
                this.ModelState.AddModelError(nameof(armor.Class), "Character class does not exist!");
                return View(armor);
            }

            if (!this.ModelState.IsValid)
            {
                return View(armor);
            }

            this.armorsService.Create(
                armor.Name, 
                armor.IntrinsicName, 
                armor.IntrinsicDescription, 
                classEnum, 
                armor.ImageUrl, 
                this.User.GetId());

            //TODO: Redirect to details page of the armor
            return RedirectToAction("Index", "Home");
        }
    }
}
