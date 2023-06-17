namespace Assignment2;

public class DeliveryVehicle        //배달 자동차 클래스
{
    int vehicleId;              //자동차 아이디
    string destination;         //자동차 목적지
    int priority;               //자동차 우선 순위

    public DeliveryVehicle()    //자동차 초기화
    {
        this.vehicleId = 0;
        this.destination = "";
        this.priority = 0;
    }
    public DeliveryVehicle(int vehicleId, string destination, int priority)     //자동차 초기화
    {
        this.vehicleId = vehicleId;
        this.destination = destination;
        this.priority = priority;
    }

    public int Priority         //자동차 우선순위 프로퍼티
    {
        get { return priority; }
        set { priority = value; }
    }

    public int VehicleId        //자동차 아이디 프로퍼티
    {
        get { return vehicleId;} 
        set {vehicleId = value;} 
    }

    public string Destination       //자동차 목적지 프로퍼티
    {
        get { return destination;}
        set { destination = value; }
    }

    
}
