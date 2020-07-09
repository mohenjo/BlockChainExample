# 블록체인 구조의 간단 구현

*C#을 이용한 블록체인 구조의 간단한 구현 예제*



## Description

### Classes, members and methods

+ `Transaction` 클래스: 거래 정보
  + 생성자: `new Transaction(string sender, string receiver, uint amount)`
  + 인스턴스 메소드:
    + `string ToString()`: 거래 정보를 문자열로 출력
  + 정적 메소드:
    + `List<Transaction> GenerateRandomTransactions()`: [1, 10] 개의 범위 내에서 임의의 거래 정보 리스트를 생성
+ `Block` 클래스: 하나 이상의 거래 정보(`Transaction`의 인스턴스)가 포함된 블록
  + 생성자: `new Block(List<Transaction> transactions, string previousHash)`
    + `Transaction` 인스턴스의 `List`와 이전 거래의 해시값(`previousHash`)을 입력값으로 받음
    + 최초 블록(Genesis Block)인 경우, `previousHash`는 빈 문자열
  + 주요 멤버 변수:
    + `uint BlockID`: 생성 순서대로 자동 부여되는 블록의 ID
    + `string PreviousHash`: 선행 블록의 해시값
    + `string CurrentHash`: 현재 블록의 해시값입
    + `string CreationTimestamp`: 현재 블록의 생성 시간(체인에 추가되지 않은)
    + `string PowTimestamp`: 현재 블록의 작업 증명 완료 시간
    + `List<Transaction> Transactions`: 현재 블록에 포함된 거래(`Transaction` 인스턴스)의 리스트
    + `uint Nonce`: 논스값
    + `uint Difficulty`: 작업 증명의 난이도
  + 인스턴스 메소드:
    + `string GetBlockHash()`: 블록 헤더의 정보로부터 블록의 해시값을 계산
    + `void POW()`: 생성된 블록에 대한 작업증명(P.O.W)를 수행
    + `string DisplayBlockInfo()`: 블록의 정보를 나타내는 문자열
  + 정적 메소드:
    + `string GetHashString(string inputString)`: 입력된 문자열에 대한 해시 문자열 반환
+ `BlockChain` 클래스: 거래 정보가 포함된 블록(`Block`의 인스턴스)들로 구성된 블록체인
  + 생성자: `new BlockChain()`
    + 최초 블록(Genesis Block)을 생성
  + 주요 멤버 변수:
    + `List<Block> blocks`: 거래 정보`(List<Transaction>`)를 담은 블록(`block` 인스턴스)들의 리스트
    + `uint Difficulty`: 블록의 작업 증명에 적용될 난이도
  + 인스턴스 메소드:
    + `void AddNewBlock(List<Transaction> transactions)`: 거래 정보(`List<Transaction>`)로부터 새로운 블록(`block`의 인스턴스)을 생성하여 체인에 추가함
      + 이전 블록의 해시값을 참조하며,
      + 블록을 생성하고 작업 증명이 완료된 후 `blocks` 멤버 변수에 저장됨
    + `bool IsValidChain()`: 현재의 블록 체인이 유효한지 검토
      + 각 블록의 해시값, 각 블록과 선행 블록의 해시값을 비교함

### Code Procedure

1. 새 블록체인(`BlockChain`) 인스턴스를 생성
2. 작업 난이도(`Difficulty`) 설정 
3. 임의 개수의 거래 내역을 랜덤 생성(`GenerateRandomTransactions()`)하여 블록체인에 추가(`AddNewBlock(...)`)함(내부적으로 작업증명 실행)
4. 현재까지 생성된 블록체인의 구조를 출력(`DisplayBlockInfo()`)
5. 블록 체인의 유효성 검토(`IsValidChain()`): OK
6. 블록 내 임의 거래를 변경 시도: 조작된 거래에 대한 `Transaction` 인스턴스를 기존 블록(`blocks`)에 저장
7. 현재까지 생성된 블록체인의 구조를 출력(`DisplayBlockInfo()`)
8. 블록 체인의 유효성 검토(`IsValidChain()`): N.G.



## Project Info

### Version

- Version 1.19

### Dev Tools

+ [C#](https://docs.microsoft.com/ko-kr/dotnet/csharp/)
+ [Microsoft Visual Studio Community Edition](https://visualstudio.microsoft.com/ko/)

### Environments

+ Test Environment

    + Microsoft Windows 10 (x64)
    + .NET Framework 4.6+

+ Dependencies / 3rd-party package(s)

    + None



## License

+ [MIT License](https://github.com/mohenjo/BlockChainExample/blob/master/LICENSE)




