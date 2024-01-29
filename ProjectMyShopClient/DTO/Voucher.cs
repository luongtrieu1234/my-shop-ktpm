using System;

namespace ProjectMyShopClient.DTO
{
    public class Voucher : Data
    {
        public int ID { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public float PercentOff { get; set; }
    }
}
