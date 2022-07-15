using HomeworkDatabase2.Models;

namespace HomeworkDatabase2.Repositories
{
    public interface IVoucherRepository
    {
        IReadOnlyList<Voucher> GetAll();
        Voucher GetById( int id );
        void Delete( Voucher voucher );
    }
}
