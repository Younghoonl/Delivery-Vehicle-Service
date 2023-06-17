namespace Assignment2;
using System;
using System.IO;

//INPUT.txt로 부터 StreamReader를 통해 읽어오고, StreamWriter를 통해 OUTPUT.txt에 쓰는 클래스
public class TextFileData
{
     DeliveryVehicleManager deliveryManager;    //배달 서비스 관리자
     private int numberOfWaitPlace;             //waitPlace 개수
     

     private string readLine;
     private string writeLine = "";
     private string[] tmpReadLine;
     private string[] tmpWriteLine;
    
     private int lineCount = 0;                  //INPUT.txt 파일의 문장을 한줄씩 세는 필드       
     private string[] commandInput;              //INPUT.txt 파일의 한 문장을 Split을 통해 여러 String으로 나눠 저장하는 변수
     public TextFileData()                       //생성자에서 deliveryManager초기화
     {
          deliveryManager = new DeliveryVehicleManager();
     }
     
     public void SetTextFile()          //INPUT.txt로 부터 StreamReader를 통해 읽어오고, StreamWriter를 통해 OUTPUT.txt에 쓰기
     {
          StreamReader sr = new StreamReader(@"../INPUT.txt");    
          StreamWriter sw = new StreamWriter(@"../OUTPUT.txt");

          for (lineCount = 1; sr.Peek() >= 0; lineCount++)       //INPUT.txt 을 한줄씩 읽어오는 반복문
          {
               readLine = sr.ReadLine();
               tmpReadLine = readLine.Split(' ');      //문장 한 줄 읽어온 후 tmpReadLine에 한 단어씩 저장

               if (lineCount == 1)          //첫번째 줄 waitPlace의 수 읽어오기
               {
                    numberOfWaitPlace = Int32.Parse(readLine);        //읽어온 waitPlace수 저장
                    deliveryManager.SetWaitPlaces(numberOfWaitPlace);
               }
               else      //INPUT파일의 두번째 줄부터 각 줄 기능 수행
               {
                    if (tmpReadLine[0] =="ReadyIn")                //문장이 ReadyIn으로 시작할때 
                    {
                         commandInput = readLine.Split(' ');     //문장을 단어로 나눠 저장
                         int waitNum = Int32.Parse(commandInput[1].Substring(1, 1)) -1;   //WaitNum 저장
                         int vehicleNum = Int32.Parse(commandInput[2]);         //문장을 단어로 나눠 저장
                         string dest = commandInput[3];                //목적지 저장
                         int prior = Int32.Parse(commandInput[4].Substring(1, 1));   //우선 순위 저장
                         DeliveryVehicle vehicle = deliveryManager.CreateVehicle(vehicleNum, dest, prior);    //읽어온 정보로 Vehicle객체 생성
                         deliveryManager.AddVehicle(vehicle, waitNum);     //waitPlaceNum을 토대로 해당 대기 장소 배열에 저장

                         writeLine = $"Vehicle {vehicleNum} assigned to waitPlace #{waitNum + 1}";  //출력

                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] =="Ready")       //문장이 Ready로 시작할때 
                    {
                         commandInput = readLine.Split(' ');      //문장을 단어로 나눠 저장
                         int vehicleNum = Int32.Parse(commandInput[1]);      //문장을 단어로 나눠 저장
                         string dest = commandInput[2];                    //목적지 저장
                         int prior = Int32.Parse(commandInput[3].Substring(1, 1));   //우선 순위 저장

                         int waitNum = deliveryManager.SearchMinWait();         //WaitPlace 대기 최소 장소 찾기

                         DeliveryVehicle vehicle = deliveryManager.CreateVehicle(vehicleNum, dest, prior);    //읽어온 정보로 Vehicle객체 생성
                         deliveryManager.AddVehicle(vehicle, waitNum);          //waitPlaceNum을 토대로 해당 대기 장소 배열에 저장
                         
                         writeLine = $"Vehicle {vehicleNum} assigned to waitPlace #{waitNum + 1}";  //출력

                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] =="Deliver")                   //문장이 Deliver로 시작할때 
                    {
                         commandInput = readLine.Split(' ');
                         int waitNum = Int32.Parse(commandInput[1].Substring(1, 1)) -1;

                         writeLine = deliveryManager.DeliverFirstPriority(waitNum);      //첫번째 우선순위 차 배달 보내기 
                         
                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] =="Clear")                 //문장이 Clear로 시작할때 
                    {
                         commandInput = readLine.Split(' ');
                         int waitNum = Int32.Parse(commandInput[1].Substring(1, 1)) -1;

                         writeLine = deliveryManager.ClearDelivery(waitNum);    //해당 WaitPlace의 모든 자동차 삭제
                         
                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] =="Cancel")          //문장이 Cancel로 시작할때 
                    {
                         commandInput = readLine.Split(' ');
                         int vehicleId = Int32.Parse(commandInput[1]);
                         
                         writeLine = deliveryManager.CancelVehicleById(vehicleId);        //해당 아이디와 일치하는 자동차 삭제

                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] == "Status")              //문장이 Status로 시작할때 
                    {
                         writeLine = deliveryManager.PrintStatus();        //WaitPlace 배열 정보 출력

                         sw.WriteLine(writeLine);
                    }
                    else if (tmpReadLine[0] =="Quit")                 //문장이 Quit로 시작할때 
                    {
                         sw.Close();
                         sr.Close();
                         return;
                    }
               }
          }
     }
}