using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("블록 생성 결과");
            Console.WriteLine(new string('*', Console.WindowWidth-1));

            // 새로운 블록체인 인스턴스를 생성합니다.
            BlockChain bc = new BlockChain();

            // 임의의 거래를 생성하여 몇 개의 블록체인을 추가해봅니다.
            bc.Difficulty = 3; // 작업 증명의 난이도를 설정합니다.
            for (int i = 0; i < 5; i++)
            {
                bc.AddNewBlock(Transaction.GenerateRandomTransactions());
            }
            // 생성된 블록체인과 거래 정보를 출력합니다.
            foreach (Block block in bc.blocks)
            {
                Console.WriteLine(block.DisplayBlockInfo());
            }
            // 블록 체인의 유효성을 출력합니다.
            Console.WriteLine($"블록 유효성: {bc.IsValidChain()}");
            Console.WriteLine();

            // 블록의 거래를 임의 변경해봅니다.
            int targetBlockID = 2;
            int targetTransactionID = 0;
            Console.Write("임의 블록 거래 정보 변경 시도: ");
            Console.WriteLine($"{targetBlockID} 블록의 {targetTransactionID + 1} 번째 거래를 변경 시도합니다.");
            Console.WriteLine(new string('*', Console.WindowWidth-1));
            bc.blocks[targetBlockID].Transactions[targetTransactionID] = new Transaction("아무개가", "아무개에게", 1000000);
            // 변경된 거래는 다음과 같이 저장된 것처럼 보이나,
            foreach (Block block in bc.blocks)
            {
                Console.WriteLine(block.DisplayBlockInfo());
            }
            // 블록 체인의 유효성을 출력해보면 거짓으로 나타납니다.
            // 블록의 히스토리는 체인화되어 참여 노드에 분산돼 있으므로, 
            // 다음 블록이 생성되기 이전에,
            // 임의 블록의 거래를 수정한 후 해당 블록부터 최신 노드까지 새로이 
            // 작업 증명하여 노드에 분산시킬 방법이 없습니다.
            Console.WriteLine($"블록 유효성: {bc.IsValidChain()}");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
