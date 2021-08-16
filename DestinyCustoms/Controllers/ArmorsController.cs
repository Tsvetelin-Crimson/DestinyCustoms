using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DestinyCustoms.Models.Armors;
using DestinyCustoms.Infrastructure;
using DestinyCustoms.Services.Armors;
using DestinyCustoms.Services.Comments;
using DestinyCustoms.Models.Comments;

namespace DestinyCustoms.Controllers
{
    using static Common.WebConstants;

    public class ArmorsController : Controller
    {
        private readonly IArmorsService armorsService;
        private readonly ICommentsService commentsService;

        public ArmorsController(
            IArmorsService armorsService,
            ICommentsService commentsService)
        {
            this.armorsService = armorsService;
            this.commentsService = commentsService;
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
            var (isClassCorrect, classEnum) = this.armorsService.IsCharacterClassNameValid(armor.Class);

            if (!isClassCorrect)
            {
                this.ModelState.AddModelError(nameof(armor.Class), "Character class does not exist!");
                return View(armor);
            }

            if (!this.ModelState.IsValid)
            {
                return View(armor);
            }

            var armorId = this.armorsService.Create(
                armor.Name, 
                armor.IntrinsicName, 
                armor.IntrinsicDescription, 
                classEnum, 
                armor.ImageUrl, 
                this.User.GetId());

            return RedirectToAction(
                nameof(ArmorsController.Details),
                nameof(ArmorsController).RemoveControllerFromString(),
                new { id = armorId });
        }

        public IActionResult Details(string id)
        {
            var armor = this.armorsService.GetById(id);

            if (armor == null)
            {
                return NotFound();
            }

            var model = new FullArmorDetailsViewModel
            {
                Armor = armor,
                CommentToBeAdded = new AddCommentFormModel
                {
                    ItemId = armor.Id,
                    AspActionString = nameof(CommentsController.AddArmorComment),
                },
                CommentClass = new CommentViewModel
                {
                    Comments = this.commentsService.GetByArmorId(id),
                    ItemId = armor.Id,
                    DeleteModel = new DeleteCommentFormModel
                    {
                        ItemId = armor.Id,
                        AspActionString = nameof(CommentsController.DeleteArmorComment),
                    },
                    ReplyToBeAdded = new AddReplyFormModel
                    {
                        ItemId = armor.Id,
                        AspActionReplyString = nameof(CommentsController.AddArmorReply),
                    },
                },
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var armor = this.armorsService.GetById(id);

            if (armor == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != armor.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
            }

            var model = new AddArmorFormModel
            {
                Name = armor.Name,
                IntrinsicName = armor.IntrinsicName,
                IntrinsicDescription = armor.IntrinsicDescription,
                ImageUrl = armor.ImageUrl,
                Class = armor.ClassName,
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, AddArmorFormModel newArmor)
        {
            var armor = this.armorsService.GetIdAndUserIdById(id);

            if (armor == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != armor.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
            }

            var (isClassCorrect, classEnum) = this.armorsService.IsCharacterClassNameValid(newArmor.Class);
            if (!isClassCorrect)
            {
                this.ModelState.AddModelError(nameof(newArmor.Class), "Character class does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return View(newArmor);
            }

            this.armorsService.Edit(
                armor.Id,
                newArmor.Name,
                newArmor.IntrinsicName,
                newArmor.IntrinsicDescription,
                classEnum,
                newArmor.ImageUrl);

            return RedirectToAction(
                nameof(ArmorsController.Details),
                nameof(ArmorsController).RemoveControllerFromString(),
                new { id = armor.Id });
        }


        [Authorize]
        public IActionResult Delete(string id)
        {
            var armor = this.armorsService.GetIdAndUserIdById(id);

            if (armor == null)
            {
                return NotFound();
            }

            if (this.User.GetId() != armor.UserId && !this.User.IsInRole(adminRoleName))
            {
                return Unauthorized();
            }

            this.armorsService.Delete(armor.Id);

            return RedirectToAction(nameof(ArmorsController.All), nameof(ArmorsController).RemoveControllerFromString());
        }

        [Authorize]
        public IActionResult MyArmors()
                => View(this.armorsService.AllUserOwned(this.User.GetId()));
    }
}
