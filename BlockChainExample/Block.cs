using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainExample
{
    public class Block
    {
        private static uint GlobalBlockID = 0;

        // 해당 블록의 정보를 담는 멤버
        private readonly uint BlockID; // 블록 ID
        public string PreviousHash { get; } // 선행 블록의 해시값
        private readonly string CreationTimestamp = string.Empty; // 블록 생성 시간
        private string PowTimestamp = string.Empty; // 작업 증명 완료 시간
        public List<Transaction> Transactions; // 블록에 저장될 거래 내역
        public string CurrentHash { get; private set; } = string.Empty; // 현재 블록의 해시값
        private uint Nonce = 0; // 해당 블록의 논스(nonce) 값
        public uint Difficulty { get; set; } = 3; // 작업 증명의 난이도

        /// <summary>
        /// 거래정보들로부터 새로운 블록을 생성합니다.
        /// </summary>
        /// <param name="transactions">거래정보를 담은 Transaction 인스턴스의 리스트</param>
        /// <param name="previousHash">선행 블록의 해시값</param>
        public Block(List<Transaction> transactions, string previousHash = "")
        {
            BlockID = GlobalBlockID++;
            PreviousHash = previousHash;
            CreationTimestamp = DateTime.Now.ToString();
            Transactions = transactions;
            CurrentHash = GetBlockHash();
            Console.WriteLine($"\n블록 {((BlockID == 0) ? "Genesis Block" : BlockID.ToString())}이(가) 생성되었습니다.");
        }

        /// <summary>
        /// 블록의 정보로부터 해시값을 계산합니다.
        /// </summary>
        /// <returns></returns>
        public string GetBlockHash()
        {
            /* 해시 대상이 되는 실제의 블록 헤더는 6가지의 정보(프로토콜 버전, 
             * 선행 블록 해시, 머클 트리 해시, 블록 생성 시간, 난이도 조절 수치, 
             * 논스 값)으로 구성됩니다. 
             * 아래에서는 이와 유사하게, 블록 ID, 선행 블록 해시, 블록 내 거래 정보의 해시,
             * 블록 생성시간, 난이도 조절 수치(정수), 논스 값으로 블록 헤더를 구성하였습니다.
             */
            string hashInput = $"{PreviousHash}{BlockID}{CreationTimestamp}{Difficulty}{Nonce}";
            StringBuilder sb = new StringBuilder();
            foreach (Transaction transaction in Transactions)
            {
                sb.Append(GetHashString(transaction.ToString()));
            }
            hashInput += sb.ToString();
            return GetHashString(hashInput);
        }

        /// <summary>
        /// 생성된 블록에 대한 작업증명(POW)을 수행합니다.
        /// </summary>
        public void POW()
        {
            Console.WriteLine($"블록 {BlockID}에 대한 작업 증명을 실시합니다:");
            Nonce = 0;
            while (CurrentHash.Substring(0, (int)Difficulty) != new string('0', (int)Difficulty))
            {
                Console.Write($"Nonce = {Nonce} 에 대한 해시값을 검토 중입니다.\r");
                Nonce++;
                CurrentHash = GetBlockHash();
            }
            PowTimestamp = DateTime.Now.ToString();
            Console.Write(new string(' ', Console.WindowWidth - 1) + "\r");
            Console.WriteLine("\r작업 증명이 완료되었습니다.\n");
        }

        /// <summary>
        /// 블록의 정보를 나타내는 문자열을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string DisplayBlockInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"------ 블록 [{BlockID}] 정보 ------");
            sb.AppendLine($"1. 블록 생성 타임스탬프: {CreationTimestamp}");
            sb.AppendLine($"2. 작업증명 완료 타임스탬프: {PowTimestamp}");
            sb.AppendLine($"3. 선행 블록의 해시값: {PreviousHash}");
            sb.AppendLine($"4. 블록의 해시값: {GetBlockHash()}");
            sb.AppendLine($"5. 세부거래정보(총 {Transactions.Count}건):");
            foreach (Transaction transaction in Transactions)
            {
                sb.AppendLine(transaction.ToString());
            }
            sb.AppendLine("------------");
            return sb.ToString();
        }

        /// <summary>
        /// 문자열의 값으로부터 해시 값을 계산하여 반환합니다.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static string GetHashString(string inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBuffer = Encoding.UTF8.GetBytes(inputString);
            byte[] outputBuffer = sha256.ComputeHash(inputBuffer);
            return BitConverter.ToString(outputBuffer).Replace("-", "");
        }
    }
}
