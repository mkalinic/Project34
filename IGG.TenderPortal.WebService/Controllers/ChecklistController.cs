using AutoMapper;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly ICheckListItemService _itemService;
        private readonly ICheckedItemService _checkedItemService;
        private readonly IUserService _userService;
        private readonly ITenderService _tenderService;

        public ChecklistController(ICheckListItemService itemService, ICheckedItemService checkedItemService, IUserService userService, ITenderService tenderService)
        {
            _itemService = itemService;
            _checkedItemService = checkedItemService;
            _userService = userService;
            _tenderService = tenderService;
        }
        // GET: Checklist
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetForProject(int id)
        {
            var checkListItems = _itemService.GetCheckListItems(id);
            var checklists = Mapper.Map<IEnumerable<CheckListItem>, IEnumerable<Models.Checklist>>(checkListItems);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, checklists);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var item = _itemService.GetCheckListItemById(id);
            _itemService.RemoveCheckListItem(item);
            _itemService.SaveCheckListItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        [HttpGet]
        public ActionResult ChangeOrder(int id, int order)
        {
            var item = _itemService.GetCheckListItemById(id);
            item.ItemOrder = order;
            _itemService.RemoveCheckListItem(item);
            _itemService.SaveCheckListItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }


        [HttpPost]
        public ActionResult Save(Checklist chlist)
        {
            if (chlist.ID <= 0)
                CreateUser(chlist);
            else
                UpdateUser(chlist);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        private void CreateUser(Checklist value)
        {
            var item = Mapper.Map<Checklist, CheckListItem>(value);
            var tender = _tenderService.GetTenderById(value.projectID);
            item.Tender = tender;
            _itemService.CreateCheckListItem(item);
            _itemService.SaveCheckListItem();
        }

        private void UpdateUser(Checklist value)
        {
            var item = _itemService.GetCheckListItemById(value.ID);

            Mapper.Map(value, item);

            var tender = _tenderService.GetTenderById(value.projectID);
            item.Tender = tender;

            _itemService.UpdateCheckListItem(item);
            _itemService.SaveCheckListItem();
        }



        [HttpPost]
        public ActionResult SaveChecked(ChecklistChecked chlist)
        {
            var checkedItem = new CheckedItem();

            var item = _itemService.GetCheckListItemById(chlist.ID);
            var user = _userService.GetUserById(chlist.IDUser);
            checkedItem.CheckListItem = item;
            checkedItem.User = user;

            _checkedItemService.UpdateCheckedItem(checkedItem);
            _checkedItemService.SaveCheckedItem();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }


        [HttpPost]
        public ActionResult RemoveChecked(ChecklistChecked chlist)
        {
            var item = _checkedItemService.GetCheckedItemById(chlist.ID);
            _checkedItemService.RemoveCheckedItem(item);
            _checkedItemService.SaveCheckedItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }
    }
}