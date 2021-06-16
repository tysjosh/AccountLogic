using System;

namespace GunvorAssessment.Audit
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountNumber { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

    }


    public enum TransactionType
    {
        Deposit = 0,
        Withdraw = 1
    }
}