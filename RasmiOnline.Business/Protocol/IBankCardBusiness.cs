namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public interface IBankCardBusiness
    {
        /// <summary>
        /// Add BankCard With Task State in Processing
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IActionResponse<int> Insert(BankCard model);

        /// <summary>
        /// Get All BankCard
        /// </summary>
        /// <returns></returns>
        IEnumerable<BankCard> GetAll();

        IEnumerable<BankCard> GetAll(bool isEnable);
        /// <summary>
        /// Update BankCard And With Task State in Processing
        /// If model.TaskState==Success this Method Must Reject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IActionResponse<int> Update(BankCard model);

        /// <summary>
        /// Find BankCard
        /// </summary>
        /// <param name="BankCardId"></param>
        /// <returns></returns>
        IActionResponse<BankCard> Find(int BankCardId);
    }
}
