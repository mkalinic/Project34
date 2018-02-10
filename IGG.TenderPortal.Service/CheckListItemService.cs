using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;
using System.Linq;

namespace IGG.TenderPortal.Service
{
    public interface ICheckListItemService
    {
        IEnumerable<CheckListItem> GetCheckListItems(int tenderId);
        CheckListItem GetCheckListItemById(int itemId);
        void CreateCheckListItem(CheckListItem item);
        void UpdateCheckListItem(CheckListItem item);
        void RemoveCheckListItem(CheckListItem item);
        void SaveCheckListItem();
    }

    public class CheckListItemService : ICheckListItemService
    {
        private readonly ICheckListItemRepository _checkListItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckListItemService(ICheckListItemRepository checkListItemRepository, IUnitOfWork unitOfWork)
        {
            _checkListItemRepository = checkListItemRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CheckListItem> GetCheckListItems(int tenderId)
        {
            var checkListItems = _checkListItemRepository.GetItemsByTenderId(tenderId);
            return checkListItems;
        }

        public CheckListItem GetCheckListItemById(int itemId) 
        {
            var checkListItem = _checkListItemRepository.GetById(itemId);
            return checkListItem;
        }

        public void CreateCheckListItem(CheckListItem item)
        {
            _checkListItemRepository.Add(item);
        }

        public void UpdateCheckListItem(CheckListItem item)
        {
            _checkListItemRepository.Update(item);
        }

        public void RemoveCheckListItem(CheckListItem item)
        {
            _checkListItemRepository.Delete(item);
        }

        public void SaveCheckListItem()
        {
            _unitOfWork.Commit();
        }
    }
}
