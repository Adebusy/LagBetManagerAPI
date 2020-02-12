using LagBetManagerAPI.Models;
using System.Threading.Tasks;

namespace LagBetManagerAPI.AppCode
{
    public interface IBetManager
    {
          ResponseMessage LogBetRequest(Transactions transactions);
    }
}
