using OCRInvoice.Entities;
using OCRInvoice.Interfaces;

namespace OCRInvoice.Services
{
    public class LineItemMasterRepository : GenericRepository<LineItemMaster>, ILineItemMasterRepository
    {
        
        public LineItemMasterRepository(DataBaseContext context) : base(context)
        {
        }

    }
}
