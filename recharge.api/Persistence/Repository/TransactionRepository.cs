using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Helpers.Functions;

namespace recharge.api.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly decimal transactionFee = 0.015m;
        private readonly IDataRepository _context;
        private readonly DataContext _repo;
        private readonly IMapper _mapper;

        public TransactionRepository(IDataRepository context, DataContext repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        public async void RecordTransaction(UserTransaction userTransaction, RechargeRequestResource rechargeRequestResource)
        {

            userTransaction.DateCreated = DateTime.Now;
            userTransaction.TransactionFee = 2 * transactionFee;
            userTransaction.UserPoint = PointFunctions.GetBonus(userTransaction.Amount);
            userTransaction.AdditionalInformation = rechargeRequestResource.AdditionalInformation();
            if(userTransaction.User.RefererId != null){
                RecordReferersTransaction(userTransaction);
            }

            userTransaction.Balance = userTransaction.UserPoint + await GetUsersPoint(userTransaction.User.Id.ToString());
            _context.Add(userTransaction);
            
            // var Transaction = new UserTransaction() {
            //     UserId = e.User.Id,
            //     RefererId = e.User.RefererId,
            //     Amount = e.Transaction.Amount,
            //     // Points = Functions.GetBonus(e.Transaction.amount),
            //     // RefererPoint = Functions.GetBonus(e.Transaction.amount, false),
            //     // PaymentType = e.Transaction.PaymentType(),
            //     AdditionalInformation = e.Transaction.AdditionalInformation(),
            //     DateCreated = DateTime.Now,
            //     TransactionFee = .015m * e.Transaction.Amount
            // };
            // _context.Add(Transaction);

            // if(!await _context.SaveAll()){
            //     throw new Exception("Failed to save transaction");
            // }

            // RecordPublicTransaction(_mapper.Map<UserTransaction, PublicPaymentTransaction>(Transaction), e.User);

        }

        private async void RecordReferersTransaction(UserTransaction userTransaction)
        {
            var referTransaction = _mapper.Map<UserTransaction>(userTransaction);
            referTransaction.TransactionFee = 1 * transactionFee;
            referTransaction.UserPoint = PointFunctions.GetBonus(userTransaction.Amount, false);
            referTransaction.Amount = 0.0m;
            referTransaction.User = userTransaction.User.Referer;
            referTransaction.Transactions = new List<AppTransaction>();
            referTransaction.AdditionalInformation = null;
            referTransaction.Balance = referTransaction.UserPoint + await GetUsersPoint(userTransaction.User.Id.ToString());
            
            _context.Add(referTransaction);
            //  _context.Add(transaction);
            // var publicTransactions = _repo.PublicPaymentTransactions.Where(u => u.UserId == user.Id);
            // var count = await publicTransactions.CountAsync();
            // if(count > maxCount + 1) {
            //     // var newPublicTransactions = publicTransactions.OrderByDescending(d => d.DateCreated).Take(maxCount);
            //     var mergePublicTransactions = publicTransactions.OrderByDescending(d => d.DateCreated).TakeLast(count - maxCount);
            //     var newPublicTransactions = new PublicPaymentTransaction(){
            //         DateCreated = DateTime.MinValue
            //     };

            //     foreach (var trans in mergePublicTransactions)
            //     {
            //         newPublicTransactions.Points += trans.Points;
            //         newPublicTransactions.RefererPoint += trans.RefererPoint;
            //         newPublicTransactions.Amount += trans.Amount;
            //     }

            //     _context.Delete(mergePublicTransactions);
            //     _context.Add(newPublicTransactions);
            // }

        }

        public IEnumerable<UserTransaction> GetUserTransaction(string userId) {
            return _repo.UsersTransactions.Where(u => u.UserId.ToString() == userId).OrderByDescending(d => d.DateCreated).Take(10).ToList();
        }

        public IEnumerable<UserTransaction> GetRefererTransaction(string userId) {
            return _repo.UsersTransactions.Where(u => u.UserId.ToString() == userId && u.AdditionalInformation == null).OrderByDescending(d => d.DateCreated).Take(10).ToList();
        }

        public async Task<Decimal> GetUsersPoint(string userId) {
            var balance = await _repo.UsersTransactions.OrderByDescending(d => d.DateCreated).FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

            return balance == null ? 0m : balance.Balance;
        }
    }
}