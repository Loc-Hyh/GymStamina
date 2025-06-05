using System.ComponentModel;

namespace GymManagementProject.Const
{
    public static class Enums
    {
        public enum StaffRole 
        { 
            Admin=99, 
            Staff= 1, 
        }
        public enum MembershipType 
        {
            [Description("Bình thường")]
            Normal = 0,
            [Description("Thân thiết")]
            Loyal = 10,
            [Description("V.I.P")]
            Vip = 20,
        }
        public enum ServiceType 
        {
            [Description("Tự tập")]
            SelfStudy = 0 ,
            [Description("Thuê PT")]
            HirePT = 1, 
        }
        public enum PaymentMethod
        {
            [Description("Tiền mặt")]
            Cash,
            [Description("Trực tuyến")]
            Online
        }

        public enum DateTrain
        {
            [Description("Ngày")]
            One = 1,
            [Description("Tuần")]
            Week = 7,
            [Description("Tháng")]
            Month = 30,
            [Description("Năm")]
            Year = 365,
        }
    }
}
