using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainExample
{
    public class Transaction
    {
        public static Random rnd = new Random();

        // 거래의 주 정보입니다.
        private readonly string Timestamp;
        private readonly string Sender;
        private readonly string Receiver;
        private readonly uint Amount;

        public Transaction(string sender, string receiver, uint amount)
        {
            Timestamp = DateTime.Now.ToString();
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }

        /// <summary>
        /// 거래의 정보를 담은 문자열을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Sender: {Sender}, Receiver: {Receiver}, Amount: {Amount}, CreationTimestamp: {Timestamp}";
        }

        /// <summary>
        /// 한 개의 블록으로 처리될 임의 개수의 거래를 임의로 생성합니다.
        /// </summary>
        /// <returns></returns>
        public static List<Transaction> GenerateRandomTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            // 임의의 거래자 이름을 생성합니다.
            string GetRandomName()
            {
                string returnName = string.Empty;
                for (int i = 0; i < 3; i++)
                {
                    returnName += (char)rnd.Next(65, 90 + 1);
                }
                return returnName;
            }

            // [1, 10] 개수의 범위에서 거래를 임의 생성합니다.
            int maxTransaction = rnd.Next(1, 11);
            for (int i = 1; i <= maxTransaction; i++)
            {
                var sender = GetRandomName();
                var receiver = GetRandomName();
                var amount = (uint)rnd.Next(1, 1001);
                transactions.Add(new Transaction(sender, receiver, amount));
            }

            return transactions;
        }
    }
}
