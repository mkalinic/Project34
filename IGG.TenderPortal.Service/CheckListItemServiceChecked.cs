using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;
using System.Linq;

namespace IGG.TenderPortal.Service
{
    public interface ICheckedItemService
    {
        IEnumerable<CheckedItem> GetCheckedItems(int itemId, int userId);
        CheckedItem GetCheckedItemById(int itemId);
        void CreateCheckedItem(CheckedItem item);
        void UpdateCheckedItem(CheckedItem item);
        void RemoveCheckedItem(CheckedItem item);
        void SaveCheckedItem();
    }

    public class CheckedItemService : ICheckedItemService
    {
        private readonly ICheckedItemRepository _checkedItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckedItemService(ICheckedItemRepository checkedItemRepository, IUnitOfWork unitOfWork)
        {
            _checkedItemRepository = checkedItemRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CheckedItem> GetCheckedItems(int itemId, int userId)
        {
            var CheckedItems = _checkedItemRepository.GetAll()
                .Where(c => c.CheckListItem.CheckListItemId == itemId)
                .Where(c => c.User.UserId == userId);
            return CheckedItems;
        }

        public CheckedItem GetCheckedItemById(int itemId) 
        {
            var CheckedItem = _checkedItemRepository.GetById(itemId);
            return CheckedItem;
        }

        public void CreateCheckedItem(CheckedItem item)
        {
            _checkedItemRepository.Add(item);
        }

        public void UpdateCheckedItem(CheckedItem item)
        {
            _checkedItemRepository.Update(item);
        }

        public void RemoveCheckedItem(CheckedItem item)
        {
            _checkedItemRepository.Delete(item);
        }

        public void SaveCheckedItem()
        {
            _unitOfWork.Commit();
        }
    }
}
