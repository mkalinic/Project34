using AutoMapper;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.DtoModel;
using System.Collections.Generic;
using System.Web.Mvc;
using IGG.TenderPortal.WebService.Models;

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

        [HttpGet]
        public ActionResult GetForProject(int id)
        {
            var checkListItems = _itemService.GetCheckListItems(id);
            var checklists = Mapper.Map<IEnumerable<CheckListItem>, IEnumerable<DtoModel.Checklist>>(checkListItems);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, checklists);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var item = _itemService.GetCheckListItemById(id);
            if (item == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, item);

            _itemService.RemoveCheckListItem(item);
            _itemService.SaveCheckListItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        [HttpGet]
        public ActionResult ChangeOrder(int id, int order)
        {
            var item = _itemService.GetCheckListItemById(id);
            if (item == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, item);

            item.ItemOrder = order;
            _itemService.RemoveCheckListItem(item);
            _itemService.SaveCheckListItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }


        [HttpPost]
        public ActionResult Save(Checklist chlist)
        {
            CheckListItem checklist;
            if (chlist.ID <= 0)
                checklist = CreateUser(chlist);
            else
                checklist = UpdateUser(chlist);

            var model = Mapper.Map<CheckListItem, Checklist>(checklist);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, model);
        }

        private CheckListItem CreateUser(Checklist value)
        {
            var item = Mapper.Map<Checklist, CheckListItem>(value);
            var tender = _tenderService.GetTenderById(value.projectID);
            item.Tender = tender;
            _itemService.CreateCheckListItem(item);
            _itemService.SaveCheckListItem();

            return item;
        }

        private CheckListItem UpdateUser(Checklist value)
        {
            var item = _itemService.GetCheckListItemById(value.ID);

            Mapper.Map(value, item);

            var tender = _tenderService.GetTenderById(value.projectID);
            item.Tender = tender;

            _itemService.UpdateCheckListItem(item);
            _itemService.SaveCheckListItem();

            return item;
        }



        [HttpPost]
        public ActionResult SaveChecked(ChecklistChecked chlist)
        {
            var checkedItem = new CheckedItem();

            var item = _itemService.GetCheckListItemById(chlist.ID);
            if (item == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, item);

            var user = _userService.GetUserById(chlist.IDUser);
            if (user == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, user);

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
            if (item == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, item);

            _checkedItemService.RemoveCheckedItem(item);
            _checkedItemService.SaveCheckedItem();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }
    }
}