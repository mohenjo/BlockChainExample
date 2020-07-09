using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainExample
{
    public class BlockChain
    {
        public List<Block> blocks = new List<Block>();
        public uint Difficulty { get; set; } = 3; // 작업증명 난이도

        public BlockChain()
        {
            CreateGenesisBlock();
        }

        /// <summary>
        /// 제네시스 블록을 생성합니다.
        /// </summary>
        private void CreateGenesisBlock()
        {
            List<Transaction> genesisTransactions = new List<Transaction>
            {
                new Transaction("Genesis Block", "Genesis Block", 0)
            };
            blocks.Add(new Block(genesisTransactions));
        }

        /// <summary>
        /// 거래정보들로부터 새로운 블록을 생성하여 작업 증명 후 체인에 추가합니다.
        /// </summary>
        /// <param name="transactions">블록에 추가할 거래 정보 리스트</param>
        public void AddNewBlock(List<Transaction> transactions)
        {
            // 임시 블록을 생성하여 작업 증명을 수행하고 
            Block tmpBlock = new Block(transactions, blocks.Last().CurrentHash);
            tmpBlock.Difficulty = this.Difficulty;
            tmpBlock.POW();
            // 블록 체인에 작업 증명이 완료된 블록을 추가
            blocks.Add(tmpBlock);
        }

        public bool IsValidChain()
        {
            bool isValid = true;
            for (int blockID = 1; blockID < blocks.Count; blockID++)
            {
                Block previousBlock = blocks[blockID - 1];
                Block currentBlock = blocks[blockID];
                // 블록 해시값의 유효성 검증
                if (currentBlock.CurrentHash != currentBlock.GetBlockHash() || 
                    currentBlock.PreviousHash != previousBlock.CurrentHash ||
                    currentBlock.PreviousHash != previousBlock.GetBlockHash())
                {
                    isValid = false;
                    Console.WriteLine($"{blockID} 블록의 해시값 또는 선행 블록과의 공유 해시값이 올바르지 않습니다.");
                }
            }
            return isValid;
        }
    }
}
