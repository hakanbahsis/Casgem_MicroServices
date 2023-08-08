namespace CasgemMicroservices.Discount.Dtos
{
    public class CreateCouponDto
    {
       
        public string Code { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Rate { get; set; }
    }
}
