namespace Assignment2;

public class DeliveryVehicleManager     //배달 서비스 관리자 클래스
{
    int numWaitingPlaces;               //자동차 대기 장소 개수
    List<DeliveryVehicle>[]? waitPlaces;    //자동차 대기 장소 배열
    int minWaitingInd;                  //가장 자동차가 적은 대기 장소의 인덱스

    string writeLine = "";

    public DeliveryVehicleManager()     //배달 서비스 관리자 초기화 
    {
        numWaitingPlaces = 0;
    }

    public void SetWaitPlaces(int waitNum)        //자동차 대기 장소 개수, 자동차 대기 장소 배열 초기화 
    {
        numWaitingPlaces = waitNum;
        waitPlaces = new List<DeliveryVehicle>[waitNum];      
        for (int i = 0; i < numWaitingPlaces; i++)
            waitPlaces[i] = new List<DeliveryVehicle>();
    }

    public DeliveryVehicle CreateVehicle(int vehicleNum, string dest, int prior)        //받아오 정보를 바탕으로 자동차 객체 생성 메소드
    {
        DeliveryVehicle vehicle = new DeliveryVehicle(vehicleNum, dest, prior);
        return vehicle;
    }

    public void AddVehicle(DeliveryVehicle vehicle, int waitNum)    //자동차 대기 장소에 자동차 객체 추가 메소드
    {                       
         waitPlaces[waitNum].Insert(0, vehicle);        //대기 장소 첫번째 인덱스에 자동차 객체 추가
         SortVehicle(waitNum);                               //배열을 훓으며 자동차를 우선순위에 따라 정렬
    }

    public void SortVehicle(int waitNum)            //0번쨰 인덱스에 입력된 자동차를 우선순위에 따라 정렬하는 메소드
    {
        DeliveryVehicle vehicle = waitPlaces[waitNum][0];       //배열에 추가된 0번째 인덱스의 자동차
        for (int i = 1; i < waitPlaces[waitNum].Count; i++)     //1번째 인덱스부터 순차적으로 자동차 우선순위 비교 
        {
            if (vehicle.Priority > waitPlaces[waitNum][i].Priority)
            {
                (vehicle, waitPlaces[waitNum][i]) = (waitPlaces[waitNum][i], vehicle);  //우선순위가 낮을 경우 스왑
            }
            else
            {
                    //다음 인덱스 자동차보다 우선 순위가 높을 경우 반복문 탈출
            }
            {
                break;
            }
        }
    }

    public int SearchMinWait()      //자동차 대기 장소들을 전부 비교해가며 최소 대기 장소 찾기 
    {
        int minVehicle = waitPlaces[0].Count;
        int minVehicleIdx = 0;
        for (int i = 1; i < numWaitingPlaces; i++)
        {
            if (minVehicle > waitPlaces[i].Count)
            {
                minVehicle = waitPlaces[i].Count;
                minVehicleIdx = i;
            }
        }
        return minVehicleIdx;       //베열에서 최소 대기 장소 인덱스 반환
    }

    public string DeliverFirstPriority(int waitNum)     //첫번째 우선순위 자동차 배달 보내기
    {
        DeliveryVehicle vehicle;
        if (waitPlaces[waitNum].Count != 0)     //첫번째 인덱스의 자동차 삭제
        {
            vehicle = waitPlaces[waitNum][0];
            waitPlaces[waitNum].RemoveAt(0);
            writeLine = $"Vehicle {vehicle.VehicleId} used to deliver";
            return writeLine;
        }
        else
        {
            writeLine = $"There is no Vehicle to deliver";
            return writeLine;
        }
    }

    public string ClearDelivery(int waitNum)        //해당 대기 장소의 자동차 모두 삭제 메소드 
    {
        waitPlaces[waitNum].Clear();
        writeLine = $"WaitPlace #{waitNum + 1} cleared";
        return writeLine;                                            
    }

    public string CancelVehicleById(int id)     //해당 아이디와 일치하는 자동차 삭제 메소드
    {
        
        for (int i = 0; i < numWaitingPlaces; i++)      //배달 장소 배열 전부 훓기
        {
            for (int j = 0; j < waitPlaces[i].Count; j++)
            {
                if (waitPlaces[i][j].VehicleId == id)       //일치 하는 아이디 찾으면 삭제
                {
                    waitPlaces[i].RemoveAt(j);
                    writeLine = $"Cancelation of Vehicle {id} completed";
                    return writeLine;
                }
            }
        }
        writeLine = $"There is no vehicle {id}";
        return writeLine;
    }

    public string PrintStatus()         //배달 관리 시스템 정보 출력
    {
        writeLine = "***********************Delivery Vehicle Info***********************\n";
        writeLine += $"Number of WaitPlaces: {numWaitingPlaces}\n";

        for (int i = 0; i < numWaitingPlaces; i++)
        {
            writeLine += $"WaitPlace #{i + 1} Number Vehicle: {waitPlaces[i].Count}\n";
            for (int j = 0; j < waitPlaces[i].Count; j++)
            {
                writeLine +=
                    $"FNUM: {waitPlaces[i][j].VehicleId} DEST: {waitPlaces[i][j].Destination} PRIO: {waitPlaces[i][j].Priority}\n";
            }
            writeLine += "----------------------------------------\n";
        }
        writeLine += "***********************End Delivery Vehicle Info************************";
        return writeLine;
    }

}

