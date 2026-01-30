using ERPWMS.Domain.Common;

namespace ERPWMS.Domain.Entities
{
    public class Device : BaseEntity
    {
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string Status { get; set; } = "Stopped"; // Running, Stopped, Alarm, Offline
        // 模拟 JSON 数据存储实时参数 (例如 {"Speed": 1200, "Temp": 45})
        public string RealTimeDataJson { get; set; } = "{}";
        public string LastAlarmMessage { get; set; } = string.Empty;
        public string IpAddress { get; set; } = "192.168.1.100";
        public DateTime LastActiveTime { get; set; } = DateTime.Now;
    }
}
